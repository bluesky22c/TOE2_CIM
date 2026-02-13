using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MMISoft.HSMS
{
    [Serializable]
    public class CimError
    {
        static public int typeMissMatch = 1;
        static public int lengthMissMath = 2;
        public CimError()
        {

        }
        public int code;
        public string message = ""; 
    
    }
    /// <summary>
    /// CIM 데이터 클래스를 정의하기 위한 기본 클래스
    /// </summary>
    [Serializable]
    public abstract class CimBasicClass
    {
        public List<CimError> error = new List<CimError>();

        public CimBasicClass()
        {
            SubItem = -1;
        }
        public string Title { get; set; } = "No Title";

        public int Stream { get; set; }
        public int Function { get; set; }
        public bool Wait { get; set; }
        public int SubItem { get; set; }
        public int SystemByte { get; set; }



        /// <summary>
        /// 속성들에 대한 Instance를 자동으로 생성한다
        /// String Type이나 Array Type은 자동생성이 가능하지며 이 경우에는 Length 비교을 하지 않는다
        /// </summary>
        public void AutoCreateInstance()
        {
            PropertyInfo[] pi = GetType().GetProperties();
            for (int i = 0; i < pi.Length; i++)
            {
                object objectValue = pi[i].GetValue(this, null);
                if (objectValue == null)
                {
                    // 변수를 생성한다.
                    try
                    {
                        object tmp = Activator.CreateInstance(pi[i].PropertyType);

                        PropertyInfo piDesc = tmp.GetType().GetProperty("Desc");
                        if (piDesc != null)
                        {
                            string descValue = piDesc.GetValue(tmp) as string;
                            if (descValue == null)
                            {
                                piDesc.SetValue(tmp, pi[i].Name, null);
                            }
                        }
                        pi[i].SetValue(this, tmp, null);
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("변수 자동 생성에 실패했습니다. 변수없는 생성자가 있는 지 확인하세요. Class: {0}, 속성: {1}", GetType().ToString(), pi[i].Name));
                    }
                }
            }
        }

        public void SetHSMS(HSMSMessage msg, int startLine = 0, int length = 0)
        {
            ParseHsms(msg, this, startLine, length);
        }

        public void SetHSMS(int stream, int function, int subItem, bool wait, string message, int startLine, int length = 0)
        {
            HSMSMessage msg = new HSMSMessage();
            msg.SetHSMS(stream, function, subItem, wait, message);
            ParseHsms(msg, this, startLine, length);

        }

        /// <summary>
        /// HSMS 메시지를 파싱해서 myClass에 데이터를 채운다
        /// </summary>
        /// <param name="msg">파싱할 HSMSMessage</param>
        /// <param name="myClass">파싱할 데이터를 채울 Class</param>
        /// <param name="startLine">파싱을 시작할 첫번째 항목</param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int ParseHsms(HSMSMessage msg, CimBasicClass myClass, int startLine = 0, int length = 0)
        {
            if (myClass == null)
                throw new Exception("ParsingHsmsError: Parsing 대상 class가 null일 수 없습니다.");
            myClass.Stream = msg.Header.Stream;
            myClass.Function = msg.Header.Function;
            myClass.Wait = msg.Header.Wait;
            myClass.SystemByte = msg.Header.SystemBytes;

            PropertyInfo[] pi = myClass.GetType().GetProperties();
            int pNo = 0;
            int i = startLine;
            while (true)
            {
                if (pNo >= pi.Length || i >= msg.Items.Count)
                {
//                    Console.WriteLine(string.Format("Return Line {0}", i));
                    return i;
                }
                object classProperty = pi[pNo].GetValue(myClass, null);
                PropertyInfo info = pi[pNo];
                pNo++;

                HSMSObject item = msg.Items[i];
                object itemValue = msg.Items[i].ValueCore;
                // 우선 리스트 타입인지 확인한다
//                if (classProperty.GetType().IsGenericType)
                if ((classProperty as IEnumerable<CimBasicClass>) != null)
                {
                    if (item.Type != ItemFormat.LIST)
                    {
                        throw new Exception(string.Format("Line {0} must be List!!", i));
                    }
                    int hsmsListCount = (int)item.ValueCore;

                    i++;

                    // List의 Type을 읽어 온다
                    Type T = classProperty.GetType().GetGenericArguments()[0];

                    // List를 지우고 새로 만든다
                    MethodInfo miClear = classProperty.GetType().GetMethod("Clear");
                    MethodInfo miAdd = classProperty.GetType().GetMethod("Add");

                    miClear.Invoke(classProperty, null);
                    for (int k = 0; k < hsmsListCount; k++)
                    {
                        miAdd.Invoke(classProperty, new object[] { Activator.CreateInstance(T) });
                    }

                    IEnumerable<CimBasicClass> list = classProperty as IEnumerable<CimBasicClass>;

                    int lastLine = i;

                    foreach (CimBasicClass b in list)
                    {
                        lastLine = ParseHsms(msg, b, lastLine, 100);
                    }

                    i = lastLine;
                }

                if ((classProperty as IEnumerable<HSMSObject>) != null)
                {
                    if (item.Type != ItemFormat.LIST)
                    {
                        throw new Exception(string.Format("Line {0} must be List!!", i));
                    }
                    int hsmsListCount = (int)item.ValueCore;

                    if (hsmsListCount > 0)
                    {
                        i++;
                        item = msg[i];

                        // List의 Type을 읽어 온다
                        Type T = classProperty.GetType().GetGenericArguments()[0];

                        // List를 지우고 새로 만든다
                        MethodInfo miClear = classProperty.GetType().GetMethod("Clear");
                        MethodInfo miAdd = classProperty.GetType().GetMethod("Add");

                        miClear.Invoke(classProperty, null);
                        for (int k = 0; k < hsmsListCount; k++)
                        {
                            HSMSObject value = (HSMSObject)Activator.CreateInstance(T);

                            item = msg[i];

                            if (value.Type != ItemFormat.Root && value.Type != item.Type)
                            {
                                CimError err = new CimError();
                                err.code = CimError.typeMissMatch;
                                err.message = string.Format("LineNo: {3}, Type Miss Match {0}, Required Type: {1}, Input Type: {2}", info.Name, value.Type, item.Type, i + 1);
                                myClass.error.Add(err);
                            }
                            if (value.Length != 0 && value.Length != item.Length)
                            {
                                CimError err = new CimError();
                                err.code = CimError.typeMissMatch;
                                err.message = string.Format("LineNo: {3}, Length Miss Match {0}, Required Length: {1}, Input Length: {2}", info.Name, value.Length, item.Length, i + 1);
                                myClass.error.Add(err);
                            }
                            value.ValueCore = item.ValueCore;
                            value.Length = item.Length;
                            item.Desc = value.Desc;
                            i++;

                            miAdd.Invoke(classProperty, new object[] { value });
                        }
                    }

                }
                if ((classProperty as CimBasicClass) != null)
                {
                    CimBasicClass value = (CimBasicClass)classProperty;
                    i = ParseHsms(msg, value, i, 100);
                }
                else if ((classProperty as HSMSObject) != null)
                {
                    HSMSObject value = (HSMSObject)classProperty;
                    if (value.Type != ItemFormat.Root && value.Type != item.Type)
                    {
                     
                        CimError err = new CimError();
                        err.code = CimError.typeMissMatch;
                        err.message = string.Format("LineNo: {3}, Type Miss Match {0}, Required Type: {1}, Input Type: {2}", info.Name, value.Type, item.Type, i + 1);
                        myClass.error.Add(err);
                    }
                    if (value.Length != 0 && value.Length != item.Length)
                    {
                        CimError err = new CimError();
                        err.code = CimError.typeMissMatch;
                        err.message = string.Format("LineNo: {3}, Length Miss Match {0}, Required Length: {1}, Input Length: {2}", info.Name, value.Length, item.Length, i + 1);
                        myClass.error.Add(err);
                    }
                    value.ValueCore = itemValue;
                    value.Length = item.Length;
                    item.Desc = value.Desc;
                    info.SetValue(myClass, value, null); 
//                    Console.WriteLine(string.Format("Parse Line {0}: {1}", i, value.Type));
                    i++;

                }
                else
                {
                }
            }
        }

        /// <summary>
        /// 현재 클래스에 해당하는 string 형테의 HSMS 메시지를 가지고 온다
        /// </summary>
        /// <returns></returns>
        public string BodyString()
        {
            string bodyString = "";
            PropertyInfo[] pi = GetType().GetProperties();

            for (int i = 0; i < pi.Length; i++)
            {
                object objectValue = pi[i].GetValue(this, null);
                Type type = pi[i].PropertyType;

                if (objectValue == null)
                {
                    throw new HSMSMakeDataException(GetType().Name, pi[i].Name);
                }

                else if ((objectValue as CimBasicClass) != null)
                {
                    CimBasicClass value = (CimBasicClass)objectValue;
                    bodyString += value.BodyString();
                }
                
                else if ((objectValue as HSMSObject) != null)
                {
                    HSMSObject value = (HSMSObject)objectValue;
                    bodyString += value.FormatString(true);
                }

                else if (objectValue is IEnumerable<CimBasicClass>)
                {
                    IEnumerable<CimBasicClass> list = objectValue as IEnumerable<CimBasicClass>;
                    bodyString += string.Format("<L,{0}>", list.Count());
                    foreach (CimBasicClass b in list)
                    {
                        bodyString += b.BodyString();
                    }
                }
                else if (objectValue is IEnumerable<HSMSObject>)
                {
                    IEnumerable<HSMSObject> list = objectValue as IEnumerable<HSMSObject>;
                    bodyString += string.Format("<L,{0}>", list.Count());
                    foreach (HSMSObject b in list)
                    {
                        bodyString += b.FormatString(true);
                    }
                }
            }

            return bodyString;
        }
    }

}
