using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using MMISoft;
using System.IO;
using System.Linq;

namespace MMISoft.HSMS
{

    public class HSMSEventArgs : EventArgs
    {
        public HSMSEventArgs ()
	    {

	    }
        public HSMSEventArgs(HSMSMessage msg)
        {
            this.msg = msg;
        }
        private HSMSMessage msg;

        public HSMSMessage Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        public bool CancelSend { get; set; } = false;
    }

    public class SentHeader
    {
        public SentHeader()
        {

        }
        public SentHeader(DateTime time, MessageHeader header)
        {
            this.SentTime = time;
            this.Header = header;
        }
        public DateTime SentTime { get; set; }
        public MessageHeader Header { get; set; }
    }


    //[System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class HSMSSocket 
    {
        //private NotifyIcon notify = new NotifyIcon();

        public enum Mode { NOT_CONNECTED, CONNECTED, SELECTED } 
        public HSMSConfig Config { get; set;}
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        /// <summary>
        /// 이건 에러가 걸러진 Event
        /// </summary>
        public event EventHandler<HSMSEventArgs> HSMSEvent = delegate { };
        /// <summary>
        /// 이건 실제로 받은 Raw데이터를 확인하기 위핸 Event
        /// </summary>
        public event EventHandler<HSMSEventArgs> OnReceiveEvent = delegate { };
        public event EventHandler<HSMSEventArgs> OnConnected = delegate { };
        public event EventHandler<HSMSEventArgs> OnDisconnected = delegate { };
        public event EventHandler<HSMSEventArgs> OnAccepted = delegate { };
        public event EventHandler<HSMSEventArgs> OnSelected = delegate { };
        /// <summary>
        /// 보낸 데이터 정보를 가지고 있다.
        /// </summary>
        public event EventHandler<HSMSEventArgs> SentEvent = delegate { };
        /// <summary>
        /// 데이터를 보내기 전에 확인 한다.
        /// </summary>
        public event EventHandler<HSMSEventArgs> OnSendingEvent = delegate { };

        /// <summary>
        /// 스트림과 펑션이 유효한지를 판단하기 위한 delegate.
        /// 2016-07-05
        /// ReceiverProce에서 이 함수를 호출하여 Stream과 Function이 사용중인지 여부를 판단한다.
        /// 메시지 형식 자체가 HSMS에 맞지 않더라도 사용중이 아닌 S/F 가 오면 무조건 S9F3, S9F5를 리턴하기 위해서 만듬.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="function"></param>
        internal delegate bool CheckStreamFunctionDelegate(int stream, int function);
        internal CheckStreamFunctionDelegate CheckStreamFunction = null;

        /// <summary>
        /// 스트림과 펑션이 유효한지를 판단하기 위한 delegate.
        /// 2016-07-05
        /// ReceiverProce에서 이 함수를 호출하여 Stream과 Function이 사용중인지 여부를 판단한다.
        /// 메시지 형식 자체가 HSMS에 맞지 않더라도 사용중이 아닌 S/F 가 오면 무조건 S9F3, S9F5를 리턴하기 위해서 만듬.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal delegate bool CheckStreamDelegate(int stream);
        internal CheckStreamDelegate CheckStream = null;


        internal delegate bool CheckFormatDelegate(HSMSMessage msg);
        internal CheckFormatDelegate CheckFormat = null;


        public HSMSMessage SentMsg { get; private set; }
        public HSMSMessage RecMsg { get; private set; }

        List<Thread> threadList = new List<Thread>();
        BackgroundWorker listenWorker = new BackgroundWorker();
        BackgroundWorker connectWorker = new BackgroundWorker();

        /// <summary>
        /// Timeout을 확인하기 위해서 전송한 리스트를 관리한다
        /// </summary>
        List<SentHeader> sentList = new List<SentHeader>();

        /// <summary>
        /// 응답시 시스템 바이트를 맞추기 위해서 받은 리스트를 관리한다.
        /// </summary>
        List<SentHeader> receiveList = new List<SentHeader>();

        object l = new object();
        public Log log;

        Mode connectMode = Mode.NOT_CONNECTED;

        public Mode ConnectMode => connectMode;
        System.Windows.Forms.Timer timer = new();
        System.Windows.Forms.Timer linkTestTimer = new();
        bool linktest = true;


        private System.Drawing.Icon runIcon;
        private System.Drawing.Icon selectIcon;

        public HSMSSocket()
        {
            Config = new HSMSConfig();
            listenWorker.DoWork += new DoWorkEventHandler(listenWorker_DoWork);
            connectWorker.DoWork += new DoWorkEventHandler(connectWorker_DoWork);

            timer.Tick += new EventHandler(timer_Tick);
            linkTestTimer.Tick += new EventHandler(linkTestTimer_Tick);


            try
            {
                runIcon = new System.Drawing.Icon(typeof(HSMSSocket), "Normal.ico");
                selectIcon = new System.Drawing.Icon(typeof(HSMSSocket), "Select.ico");
                //notify.Icon = runIcon;
                //notify.Visible = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ShowConfig()
        {
            ConfigForm form = new ConfigForm();
            form.Config = Config;
            form.ShowDialog();
        }

        void linkTestTimer_Tick(object sender, EventArgs e)
        {
            if (connectMode == Mode.SELECTED)
            {
                if (linktest == false)
                {
                    linktest = true;
                    log.WriteLine(Log.SendReceive.Error, 0, "", false, "Linktest Fail");
                    connectMode = Mode.NOT_CONNECTED;
                    Close();
                }
                else
                {
                    linktest = false;
                    HSMSMessage msg = new HSMSMessage();
                    msg.Header.SType = MessageHeader.STypes.Linktest_Req;
                    msg.Header.DeivceID = 0xffff;
                    msg.Header.byte2 = 0;
                    msg.Header.byte3 = 0;
                    msg.Header.pType = 0;
                    Send(msg);
                    log.WriteLine(Log.SendReceive.Etc, msg.Header.SystemBytes, "", false, "Send Linktest_Req", msg.Header.DeivceID);
                }
            }
        }

        ~HSMSSocket()
        {
            Close();
        }

        public void Close()
        {
            try
            {
                //if (notify != null)
                //{
                //    notify.Visible = false;
                ////                notify.Icon.Dispose();
                //    notify.Dispose();
                //    notify = null;
                //}
                socket.Close();
            }
            catch
            {
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            SentHeader timeoutHeader = null;
            lock (l)
            {
                for (int i = sentList.Count - 1; i >= 0 ; i--)
                {
                    SentHeader h = sentList[i];
                    TimeSpan ts = now - h.SentTime;
                    if (ts > TimeSpan.FromSeconds(Config.T3))
                    {
                        timeoutHeader = h;
                        break;
                    }
                }
                if (timeoutHeader != null)
                {
                    sentList.Remove(timeoutHeader);
                    SendMHEAD(9, 9, timeoutHeader.Header);
                }

            }
        }

        void connectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                bool connected = false;
                while (connected == false)
                {
                    try
                    {
                        socket.Connect(Config.IP, Config.Port);
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    break;

                }

                connectMode = Mode.CONNECTED;
                log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Connected");

                socket.SendBufferSize = 8192;
                socket.ReceiveBufferSize = 8192;
                socket.ReceiveTimeout = 2000;
                socket.NoDelay = true;

                if (OnConnected != null)
                {
                    OnConnected(this, new HSMSEventArgs());
                }
                var hdr = new MessageHeader { DeivceID = (ushort)Config.Device };
//                log.WriteLine(Log.SendReceive.Send, hdr.SystemBytes, "", false, "Send SelectReq", hdr.DeivceID);
                SendHeader(MessageHeader.STypes.SelectReq, hdr);
                Thread t = new Thread(new ThreadStart(ReceiveProc));
                threadList.Add(t);
                t.IsBackground = true;
                t.Start();

            }
            catch (Exception except)
            {
                MessageBox.Show(except.StackTrace);
            }

        }

        void listenWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    Socket server = null;
                    try
                    {
                        //            IPAddress hostIP = (Dns.Resolve(IPAddress.Any.ToString())).AddressList[0];
                        //            IPAddress hostIP = (Dns.GetHostEntry(IPAddress.Any)).AddressList[0];
                        server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IPEndPoint ep = new IPEndPoint(IPAddress.Any, Config.Port);
                        server.Bind(ep);
                        server.Listen(5);

                        while (true)
                        {
                            socket = server.Accept();

                            socket.SendBufferSize = 8192;
                            socket.ReceiveBufferSize = 8192;
                            socket.ReceiveTimeout = 2000;
                            socket.NoDelay = true;

                            connectMode = Mode.CONNECTED;
                            log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Accecpted");
                            if (OnAccepted != null)
                            {
                                OnAccepted(this, new HSMSEventArgs());
                            }
                            if (Config.NoSendSelectReqPassiveMode == false)
                            {
                                var hdr = new MessageHeader { DeivceID = (ushort)Config.Device };
                                SendHeader(MessageHeader.STypes.SelectReq, hdr);
                                //                        log.WriteLine(Log.SendReceive.Send, hdr.SystemBytes, "", false, "Send->SelectReq", hdr.DeivceID);
                            }
                            Thread t = new Thread(new ThreadStart(ReceiveProc));
                            threadList.Add(t);
                            t.IsBackground = true;
                            t.Start();
                        }
                        //server.Close();
                    }
                    catch (Exception execept)
                    {
                        if (server != null)
                        {
                            server.Close();
                            server = null;
                        }

                        log.WriteLine(Log.SendReceive.Etc, 0, "", false,
                            "Listen Error:" + execept.Message + execept.StackTrace);
                        
                        // 포트 바인딩 실패 등으로 인한 무한 루프 방지를 위해 5초 대기
                        Thread.Sleep(5000); 
                    }
                }
            }
            catch (Exception execept)
            {
                MessageBox.Show("Listen Error:" + execept.Message + execept.StackTrace);
            }
        }

        public void Start()
        {
            try
            {
                var encoding = Encoding.GetEncoding(Config.HSMSEncoding);
                //var encodings = Encoding.GetEncodings();
                //var names = from e in encodings select e.Name;
            }
            catch
            {
                Config.HSMSEncoding = "utf-8";
            }

            try
            {
                var encoding = Encoding.GetEncoding(Config.LogEncoding);
                //var encodings = Encoding.GetEncodings();
                //var names = from e in encodings select e.Name;
            }
            catch
            {
                Config.LogEncoding = "utf-8";
            }

            if (log == null)
            {
                log = new Log(Config.LogFileName, Config.LogFileSize, Config.LogPath, Encoding.GetEncoding(Config.LogEncoding));
            }
            HSMSMessage.Encoding = Encoding.GetEncoding(Config.HSMSEncoding);
            log.WriteLine("///////////////////////////////////////////////");
            log.WriteLine("// This library is HSMS library for Engion System provided by mmisoft.");
            log.WriteLine($"// Current Encoding is {Config.HSMSEncoding}");
            log.WriteLine("///////////////////////////////////////////////");
            log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Started");
            log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Config");

            timer.Interval = 1000;
            timer.Start();

            linkTestTimer.Interval = Config.LinkTestTime * 1000;
            linkTestTimer.Start();

            if (Config.Mode == HSMSConfig.ModeEnum.ACTIVE)
            {
                Connect();
            }
            else
            {
                Listen();
            }
        }

        void Listen()
        {
            if (listenWorker.IsBusy == false)
            {
                listenWorker.RunWorkerAsync();
            }

        }

        void Connect()
        {
            if (connectWorker.IsBusy == false)
            {
                connectWorker.RunWorkerAsync();
            }
        }


        public bool SendMHEAD(int stream, int function, MessageHeader h)
        {
            HSMSMessage msg = new HSMSMessage();
            byte []shead = h.GetBytes();

	        string strSend = string.Format(
		        "<B,{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}>>", stream, function, shead[0],
                            shead[1],shead[2],shead[3],shead[4],
                            shead[5],shead[6],shead[7],shead[8],shead[9]);

            return Send(stream, function, false, strSend);
        }

        
        public bool Send(HSMSMessage msg, string note = "Send Data")
        {
            if (socket.Connected == false)
            {
                connectMode = Mode.NOT_CONNECTED;
                return false;
            }
            MessageHeader header = msg.Header;
            if (msg.Header.SType != MessageHeader.STypes.Linktest_Req && msg.Header.SType != MessageHeader.STypes.Linktest_Rsp)
                header.DeivceID = (ushort)Config.Device;
            try
            {
                // Reply 메시지의 경우 자동으로 SystemByte를 설정한다.
                if (Config.AutoMakeSystemByte)
                {
                    SentHeader findHeader = receiveList.Find(h => h.Header.Stream == header.Stream && h.Header.Function == header.Function - 1);
                    if (findHeader != null)
                    {
                        header.SystemBytes = findHeader.Header.SystemBytes;
                        lock (l)
                        {
                            receiveList.Remove(findHeader);
                        }
                    }
                }

                byte[] sndMsg = msg.GetBytes();
                if (header.Wait == true)
                {
                    lock (l)
                    {
                        sentList.Add(new SentHeader(DateTime.Now, header));
                    }
                }
                Send(sndMsg);
            }
            catch (ObjectDisposedException)
            {
                connectMode = Mode.NOT_CONNECTED;
                msg.Header.SType = MessageHeader.STypes.Disconnect;
                HSMSEvent(this, new HSMSEventArgs(msg));
                return false;
            }
            catch (SocketException)
            {
                connectMode = Mode.NOT_CONNECTED;
                msg.Header.SType = MessageHeader.STypes.Disconnect;
                HSMSEvent(this, new HSMSEventArgs(msg));
                return false;
            }
            if (header.SType == MessageHeader.STypes.DataMessage)
            {
                log.WriteLine(Log.SendReceive.Send, header.SystemBytes, $"S{header.Stream}F{header.Function}", header.Wait, msg.ToString(), header.DeivceID);
                //log.WriteLine(note + string.Format("; Send[{3}]({2}) -> S{0}F{1}\r\n", 
                //    header.Stream, 
                //    header.Function, 
                //    header.SystemBytes, 
                //    header.DeivceID) + msg.ToString());
            }
            if (SentEvent != null)
            {
                SentEvent(this, new HSMSEventArgs(msg));
            }

            return true;
        }

        public bool Send(int stream, int function, bool reply, string body, int systemByte = 0)
        {
            HSMSMessage msg = new HSMSMessage();
            MessageHeader header = msg.Header;

            msg.SetHSMS(stream, function, -1, reply, body);

            msg.Header.SType = MessageHeader.STypes.DataMessage;
            header.Stream = stream;
            header.Function = function;
            header.Wait = reply;

            byte[] sndMsg = msg.GetBytes();

            return Send(msg);
        }

        bool Receive(byte[] buf, int offset, int length)
        {
            int readCount = 0;
            try
            {
                while (readCount != length)
                {
                    //for (int i = 0; i < 100; i++)
                    //{
                    //    if (socket.Available == 0)
                    //    {
                    //        Thread.Sleep(10);
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}
                    //if (socket.Available == 0)
                    //{
                    //    dataReceived = false;
                    //    break;
                    //}
                    readCount += socket.Receive(buf, readCount + offset, length - readCount, SocketFlags.None);
                    if (readCount >= length)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}, Lenght: {length}, Read: {readCount}");
                return false;
            }
            return true;

        }

        void Error(string errorString)
        {
            if (Config.ShowErrorMessageBox == true)
            {
                MessageBox.Show(errorString);
            }
            log.WriteLine(Log.SendReceive.Error, 0, "", false, errorString);

        }

        public void ReceiveProc() 
        {
            // 데이터 구조.
            // 0~3(4) : 데이터 길이
            // 4~13 (10) : 헤더.
            // 데이터
            while (true)
            {
                try
                {
                    HSMSMessage msg = new HSMSMessage();
                    int length = 0;
                    byte[] hBuf = new byte[14];
                    byte[] buf = null;
                    socket.ReceiveTimeout = -1;
                    int readLen = socket.Receive(hBuf, 14, SocketFlags.None);
                    if (readLen == 0)
                    {
                        //if (notify == null)
                        //    notify = new NotifyIcon();
                        //notify.Icon = runIcon;

                        log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Receive Thread Done");
                        lock (l)
                        {
                            sentList.Clear();
                            receiveList.Clear();
                        }
                        msg.Header.SType = MessageHeader.STypes.Disconnect;
                        HSMSEvent(this, new HSMSEventArgs(msg));

                        break;
                    }
                    length = BigEndianConverter.ToInt32(hBuf, 0) - 10;

                    MessageHeader header = new MessageHeader();
                    header.SetData(hBuf, 4);

                    if (length < 0 || length > 1024 * 1024 * 10)
                    {
                        SendMHEAD(9, 11, header);
                        Error("Data Too Long");
                        break;
                    }

                    buf = new byte[length + 14];
                    Array.Copy(hBuf, buf, 14);

                    if (length > 0)
                    {
                        socket.ReceiveTimeout = 2000;
                        if (Receive(buf, 14, length) == false)
                        {
                            //notify.Icon = runIcon;

                            log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Receive Thread Done - few Data");
//                            log.WriteLine("Receive Thread Done");
                            lock (l)
                            {
                                sentList.Clear();
                                receiveList.Clear();
                            }
                            msg.Header.SType = MessageHeader.STypes.Disconnect;
                            HSMSEvent(this, new HSMSEventArgs(msg));
                            socket.Close();
                            break;
                        }
                    }

                    msg = HSMSMessage.FromBinary(buf);
                    header = msg.Header;
                    OnReceiveEvent(this, new HSMSEventArgs(msg));


                    // 2016-7-5 S/F를 확인 하기 위할 루틴.
                    if (header.SType == MessageHeader.STypes.DataMessage && header.Stream != 9)
                    {
                        if (CheckStream != null)
                        {
                            if (CheckStream(header.Stream) == false)
                            {
                                log.WriteLine(Log.SendReceive.Error, header.SystemBytes, $"S{header.Stream}F{header.Function}", header.Wait, "Invalid Stream Receive", header.DeivceID);
                                //log.WriteLine(string.Format("Invalid Stream Receive[{3}]({2}) -> S{0}F{1}\r\n",
                                //    header.Stream, header.Function, header.SystemBytes, header.DeivceID));
                                SendMHEAD(9, 3, header);
                                continue;
                            }
                        }

                        if (CheckStreamFunction != null)
                        {
                            if (CheckStreamFunction(header.Stream, header.Function) == false)
                            {
                                log.WriteLine(Log.SendReceive.Error, header.SystemBytes, $"S{header.Stream}F{header.Function}", header.Wait, "Invalid Stream/Function Receive", header.DeivceID);
                                //log.WriteLine(string.Format("Invalid Stream/Function Receive[{3}]({2}) -> S{0}F{1}\r\n",
                                //    header.Stream, header.Function, header.SystemBytes, header.DeivceID));
                                SendMHEAD(9, 5, header);
                                continue;
                            }
                        }

                        if (CheckFormat != null)
                        {
                            if (CheckFormat(msg) == false)
                                continue;
                        }
                    }


                    SentHeader replyHeader = null;
                    lock (l)
                    {
                        for (int i = sentList.Count - 1; i >= 0; i--)
                        {
                            SentHeader h = sentList[i];
                            if (h.Header.SystemBytes == header.SystemBytes)
                            {
                                replyHeader = h;
                                sentList.Remove(replyHeader);
                                break;
                            }
                        }
                    }
                    //                    log.WriteLine(string.Format("receive: {0}", header.SystemBytes));

                    linktest = true;
                    switch (header.SType)
                    {
                        case MessageHeader.STypes.DataMessage:
                            {
                                log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, $"S{header.Stream}F{header.Function}", header.Wait, msg.ToString(), header.DeivceID);
                                if (header.DeivceID != Config.Device && header.Stream != 9)
                                {
                                    SendMHEAD(9, 1, header);
                                    Error("Device 번호가 다릅니다");
                                }
                                else
                                {
                                    if (msg.Header.Wait == true)
                                    {
                                        lock (l)
                                        {
                                            receiveList.Add(new SentHeader(DateTime.Now, msg.Header));
                                        }
                                    }
                                }
                                break;
                            }

                        case MessageHeader.STypes.SelectReq:
                            lock (l)
                            {
//                                sentList.Clear();
                                receiveList.Clear();
                            }
                            log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, "", false, "Receive->SelectReq", header.DeivceID);
                            //                            log.WriteLine("Receive -> SelectReq");

                            lock (l)
                            {
                                if (connectMode == Mode.CONNECTED)
                                {
                                    connectMode = Mode.SELECTED;
                                    OnSelected(this, new HSMSEventArgs());
                                }
                            }
                            SendHeader(MessageHeader.STypes.SelectRsp, header);
                            break;

                        case MessageHeader.STypes.SelectRsp:
                            log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, "", false, "Receive->SelectRsp", header.DeivceID);
                            lock (l)
                            {
                                if (connectMode == Mode.CONNECTED)
                                {
                                    connectMode = Mode.SELECTED;
                                    OnSelected(this, new HSMSEventArgs());
                                }
                            }
                            //notify.Icon = selectIcon;
                            break;

                        case MessageHeader.STypes.DeselectReq:
                            log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, "", false, "Receive->DeselectReq", header.DeivceID);
                            SendHeader(MessageHeader.STypes.DeselectRsp, header);
                            log.WriteLine(Log.SendReceive.Send, header.SystemBytes, "", false, "Send->DeselectRsp", header.DeivceID);
                            //notify.Icon = runIcon;
                            connectMode = Mode.CONNECTED;
                            break;

                        case MessageHeader.STypes.DeselectRsp:
                            log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, "", false, "Receive -> DeselectRsp", header.DeivceID);
                            break;

                        case MessageHeader.STypes.Linktest_Req:
                            log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, "", false, "Receive -> Linktest_Req", header.DeivceID);
                            SendHeader(MessageHeader.STypes.Linktest_Rsp, header);
                            break;
                        case MessageHeader.STypes.Linktest_Rsp:
                            log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, "", false, "Receive -> Linktest_Rsp", header.DeivceID);
                            break;
                    }
                    HSMSEvent(this, new HSMSEventArgs(msg));
                    // HSMS Event 한 후 로그를 만든다.
                    //if (header.SType == MessageHeader.STypes.DataMessage)
                    //    log.WriteLine(Log.SendReceive.Receive, header.SystemBytes, $"S{header.Stream}F{header.Function}", header.Wait, msg.ToString(), header.DeivceID);

                }
                catch (SocketException es)
                {
                    //if (notify != null)
                    //{
                    //    notify.Icon = runIcon;
                    //}
                    log.WriteLine(es.Message);
                    //MessageBox.Show(es.Message + es.StackTrace);
                    break;
                }
                catch (Exception e)
                {
                    log.WriteLine(e.Message);
                    //MessageBox.Show(e.Message + e.StackTrace);
                }
            }
            connectMode = Mode.NOT_CONNECTED;

            if (OnDisconnected != null)
            {
                OnDisconnected(this, new HSMSEventArgs());
            }
            Thread.Sleep(2000);
            Start();
        }

        public int Send(byte[] buf, int offset, int size, SocketFlags socketFlags)
        {
            var args = new HSMSEventArgs();
            args.Msg = HSMSMessage.FromBinary(buf);
            int sendBytes = 0;
            OnSendingEvent(this, args);

            if (args.CancelSend == false)
                sendBytes = socket.Send(buf, offset, size, socketFlags);

            args.Msg = HSMSMessage.FromBinary(args.Msg.BinaryBuf);
            SentEvent(this, args);

            return sendBytes;
        }

        public int Send(byte[] buf)
        {
            return Send(buf, 0, buf.Length, SocketFlags.None);
        }

        public void Select()
        {
            HSMSMessage msg = new HSMSMessage();
            msg.Header.DeivceID = (ushort)0xffff;
            msg.Header.SType = MessageHeader.STypes.SelectReq;
            byte[] sndMsg = msg.GetBytes();
            Send(sndMsg);
        }

        public bool SendHeader(MessageHeader.STypes stype, MessageHeader rcvHdr)
        {

            try
            {
                HSMSMessage msg = new HSMSMessage();

                msg.Header = rcvHdr;
                msg.Header.SType = stype;
                byte[] sndMsg = msg.GetBytes();
                if (Send(sndMsg) == -1)
                {
                    log.WriteLine(string.Format("Send Header Fail({2}) -> S{0}F{1}\r\n", rcvHdr.Stream, rcvHdr.Function, rcvHdr.SystemBytes) + msg.ToString());
                    return false;
                }
                else
                {
                    log.WriteLine(Log.SendReceive.Send, msg.Header.SystemBytes, "", false, $"Send -> {msg.Header.SType}", msg.Header.DeivceID);
                    return true;
                }
            }
            catch (Exception execept)
            {
                log.WriteLine(Log.SendReceive.Etc, 0, "", false, "Send Header Error");
                log.WriteLine(Log.SendReceive.Etc, 0, "", false, execept.Message + execept.StackTrace);

            }
            return true;
        }

    }

    [Serializable]
    public class MessageHeader
    {
        static int genCount = 1;
        public enum STypes : byte
        {
            DataMessage, SelectReq, SelectRsp, DeselectReq, DeselectRsp,
            Linktest_Req, Linktest_Rsp, RejectReq, NotUse, SeparateReq, NotUse2, Disconnect
        }
        private ushort deivceID;

        public ushort DeivceID
        {
            get { return deivceID; }
            set { deivceID = value; }
        }
        public byte byte2;
        public byte byte3;
        public byte pType;
        private STypes sType;

        public int BSType
        {
            get { return (int)sType; }
            set { sType = (STypes)value; }
        }

        public STypes SType
        {
            get { return sType; }
            set { sType = value; }
        }
        private int systemBytes;

        public int SystemBytes
        {
            get { return systemBytes; }
            set { systemBytes = value; }
        }

        public int Stream
        {
            get { return byte2 & 0x7f; }
            set { byte2 = (byte)(byte2 & 0x80 | value); }
        }
        public int Function
        {
            get { return byte3; }
            set { byte3 = (byte)value; }
        }

        public bool Wait
        {
            get { return ((byte2 & 0x80) != 0 ? true: false);}
            set { byte2 = value ? (byte)(byte2 | 0x80) : (byte)(byte2 & 0x7f); }
        }

        public MessageHeader(int systemByte  = 0)
        {
            if (systemByte == 0)
            {
                if (genCount == 0)
                    genCount = new Random().Next(100);
                systemBytes = genCount;
                genCount++;
                if (genCount > int.MaxValue - 100)
                    genCount = new Random().Next(100);
            }
            else
            {
                this.systemBytes = systemByte;
            }
        }

        public void SetData(byte[] buf, int start)
        {
            deivceID = BigEndianConverter.ToUInt16(buf, start);
            start += 2;
            byte2 = buf[start++];
            byte3 = buf[start++];
            pType = buf[start++];
            sType = (STypes)buf[start++];
            systemBytes = BigEndianConverter.ToInt32(buf, start);
        }

        public byte[] GetBytes()
        {
            byte[] data = new byte[10];
            byte[] byteData = BigEndianConverter.GetBytes(deivceID);
            for (int i = 0; i < byteData.Length; i++)
            {
                data[i] = byteData[i];
            }
            data[2] = byte2;
            data[3] = byte3;
            data[4] = pType;
            data[5] = (byte)sType;
            byteData = BigEndianConverter.GetBytes(systemBytes);
            for (int i = 0; i < byteData.Length; i++)
            {
                data[6 + i] = byteData[i];
            }
            return data;
        }
    }

}
