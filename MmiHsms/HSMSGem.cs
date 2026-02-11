using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace MMISoft.HSMS
{

    public class GemEventArgs : EventArgs
    {
        public GemEventArgs()
        {

        }
        public bool CancelSending;
        public HSMSMessage gemMessage;
        public CimBasicClass cimClass;
    }

    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public abstract class HSMSGem
    {
        public event EventHandler<GemEventArgs> OnGemEvent = delegate { };
        public event EventHandler OnSelectEvent = delegate { };
        public event EventHandler OnDisconnectEvent = delegate { };
        public event EventHandler OnConnectEvent = delegate { };
        public event EventHandler<GemEventArgs> OnSentEvent = delegate { };

        /// <summary>
        /// 포맷을 확인하고, Log를 만들기 위한 작업을 한다.
        /// </summary>
        public delegate void CheckFormatDelegate(HSMSMessage msg);
        public CheckFormatDelegate CheckFormat;

        public HSMSSocket socket = new HSMSSocket();
        public HSMSSocket.Mode ConnectMode => socket.ConnectMode;
        public HSMSConfig Config
        {
            get { return socket.Config; }
            set { socket.Config = value; }
        }
        public HSMSGem()
        {
            //Auth.AuthCheck() check removed
        }

        public void ShowConfig(HSMSConfig config)
        {
            socket.Config = config;
            socket.ShowConfig();
        }

        public void Init()
        {
            socket.CheckFormat = SocketCheckFormat;
            socket.HSMSEvent += new EventHandler<HSMSEventArgs>(socket_HSMSEvent);
            socket.OnAccepted += new EventHandler<HSMSEventArgs>(socket_OnAccepted);
            socket.OnDisconnected += new EventHandler<HSMSEventArgs>(socket_OnDisconnected);
            socket.SentEvent += new EventHandler<HSMSEventArgs>(socket_SentEvent);
            socket.OnSelected += new EventHandler<HSMSEventArgs>(socket_OnSelected);

            socket.CheckStream = new HSMSSocket.CheckStreamDelegate(CheckStream);
            socket.CheckStreamFunction = new HSMSSocket.CheckStreamFunctionDelegate(CheckStreamFunction);

            socket.Start();
        }

        void socket_OnSelected(object sender, HSMSEventArgs e)
        {
            OnSelectEvent(this, new EventArgs());
        }

        void socket_SentEvent(object sender, HSMSEventArgs e)
        {
            OnSentEvent(this, new GemEventArgs() { gemMessage = e.Msg });
        }

        public void Close()
        {
            socket.Close();
        }

        void socket_OnDisconnected(object sender, HSMSEventArgs e)
        {
            OnDisconnectEvent(this, new EventArgs());
        }

        void socket_OnAccepted(object sender, HSMSEventArgs e)
        {
            OnConnectEvent(this, new EventArgs());
        }

        void socket_HSMSEvent(object sender, HSMSEventArgs e)
        {
            if (e.Msg.Header.SType == MMISoft.HSMS.MessageHeader.STypes.DataMessage)
            {
                HSMSReceive(e.Msg);
            }
            OnGemEvent(this, new GemEventArgs() { gemMessage = e.Msg });
        }

        static public Type FindClass(string className, Type refType)
        {
            Assembly.Load(refType.Assembly.FullName);
            Type t2 = refType.Assembly.GetType(refType.Namespace + "." + className);

            return t2;

        }

        public bool HSMSSend(CimBasicClass cimClass, string note = "Send Data")
        {

            HSMSMessage msg = new HSMSMessage();
            try
            {
                msg.SetCimClass(cimClass);
                msg.Title = cimClass.Title;
            }
            catch
            {
                socket.log.WriteLine(Log.SendReceive.Error, 0, "", false, string.Format("Send Fail -> S{0}F{1}\r\n", cimClass.Stream, cimClass.Function) + msg.ToString());
                throw;
            }
            return socket.Send(msg, note);
        }

        public bool HSMSSend(HSMSMessage msg)
        {
            return socket.Send(msg);
        }

        public CimBasicClass GetCimClass(HSMSMessage message)
        {
            // 매칭되는 class이름이 존재하는지 확인한다
            string className = "";
            if (message.SubItem == -1)
            {
                className = string.Format("CIMS{0}F{1}", message.Stream, message.Function);
            }
            else
            {
                className = string.Format("CIMS{0}F{1}ITEM{2}", message.Stream, message.Function, message.SubItem);
            }

            CimBasicClass cimClass = (CimBasicClass)Activator.CreateInstance(HSMSGem.FindClass(className, GetType()));

            if (cimClass != null)
            {
                cimClass.SetHSMS(message, 0);
            }
            else
            {
                MessageBox.Show(string.Format("Class ({0})가 없습니다.", className));
            }

            return cimClass;
        }

        public bool CheckStream(int stream)
        {
            string cimClassName = string.Format("CIMS{0}F0", stream);

            if (FindClass(cimClassName, GetType()) == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckStreamFunction(int stream, int function)
        {
            Type cimClassType = null;
            string cimClassName = string.Format("CIMS{0}F{1}", stream, function);
            cimClassType = FindClass(cimClassName, GetType());
            if (cimClassType == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 데이터 포맷을 체크하고 문제가 있으면 S9계열 에러를 전송한다.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SocketCheckFormat(HSMSMessage msg)
        {
            if (msg.Header.Stream == 9)
                return false;
            // stream이 있는지 확인한다.
            string cimClassName = string.Format("CIMS{0}F0", msg.Header.Stream);
            Type cimClassType = null;

            // 해당 스트림이 없다
            if (FindClass(cimClassName, GetType()) == null)
            {
                socket.SendMHEAD(9, 3, msg.Header);
                return false;
            }

            // Function이 있는지 확인한다.
            cimClassName = string.Format("CIMS{0}F{1}", msg.Header.Stream, msg.Header.Function);
            cimClassType = FindClass(cimClassName, GetType());
            if (cimClassType == null)
            {
                socket.SendMHEAD(9, 5, msg.Header);
                return false;
            }


            // 데이터를 만든다.
            CimBasicClass cimClass = null;
            cimClass = (CimBasicClass)Activator.CreateInstance(cimClassType);
            if (cimClass == null)
            {
                return false;
            }
            msg.Title = cimClass.Title;
            cimClass.SetHSMS(msg, 0);
            if (cimClass.error.Count != 0)
            {
                foreach (var err in cimClass.error)
                {
                    socket.log.WriteLine(Log.SendReceive.Error, 0, "", false, err.message);
                    // socket.log.WriteLine(err.message);
                }
                socket.SendMHEAD(9, 7, msg.Header);
                return false;
            }
            if (CheckFormat != null)
            {
                CheckFormat(msg);
            }
            return true;
        }

        void HSMSReceive(HSMSMessage msg)
        {
            if (msg.Header.Stream == 9)
                return;
            // stream이 있는지 확인한다.
            string cimClassName = string.Format("CIMS{0}F0", msg.Header.Stream);
            Type cimClassType = null;

            // 해당 스트림이 없다
            if (FindClass(cimClassName, GetType()) == null)
            {
                return;
            }

            // Function이 있는지 확인한다.
            cimClassName = string.Format("CIMS{0}F{1}", msg.Header.Stream, msg.Header.Function);
            cimClassType = FindClass(cimClassName, GetType());
            if (cimClassType == null)
            {
                return;
            }


            // 데이터를 만든다.
            CimBasicClass cimClass = null;
            cimClass = (CimBasicClass)Activator.CreateInstance(cimClassType);
            if (cimClass == null)
            {
                return;
            }
            cimClass.SetHSMS(msg, 0);
            //if (cimClass.error.Count != 0)
            //{
            //    foreach (var err in cimClass.error)
            //    {
            //        socket.log.WriteLine(Log.SendReceive.Error, 0, "", false, err.message);
            //        // socket.log.WriteLine(err.message);
            //    }
            //    socket.SendMHEAD(9, 7, msg.Header);
            //    return;
            //}

            GemEventArgs args = new GemEventArgs();
            args.gemMessage = msg;
            args.cimClass = cimClass;


            // 함수를 호출한다.
            string methodName = string.Format("OnS{0}F{1}Recv", msg.Header.Stream, msg.Header.Function);
            MethodInfo mi = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (mi != null)
            {
                mi.Invoke(this, new object[] { args });
            }
            else
            {
//                MessageBox.Show("Method가 정의되지 않았습니다. " + methodName);
            }
        }

        public void AddToLog(string message)
        {
            socket.log?.WriteLine(message);
        }

        public void AddToLogDesc(string message)
        {
            socket.log?.WriteDesc(message);
        }

    }
}
