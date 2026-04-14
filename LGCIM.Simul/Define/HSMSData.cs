using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMISoft.HSMS;
using System.Xml.Serialization;

namespace LGCIMServer
{
    public class HSMSData
    {
        public HSMSData()
        {
        }

        public HSMSData(CimBasicClass cimClass)
        {
            CimClass = cimClass;
            Message = new HSMSMessage(cimClass);
        }

        public HSMSData(int s, int f, bool wait, List<HSMSObject> items, int subItem = -1)
        {
            Message.Header = new MessageHeader();
            Message.Header.Stream = s;
            Message.Header.Function = f;
            Message.Header.Wait = wait;
            Message.SubItem = subItem;
            Message.Items = items;

            CimClass.SetHSMS(Message);
        }

        public string SFString
        {
            get 
            {
                if (Message.SubItem == -1)
                {
                    return string.Format("S{0}F{1}", Message.Header.Stream, Message.Header.Function);
                }
                else
                {
                    return string.Format("S{0}F{1}Item{2}", Message.Header.Stream, Message.Header.Function, Message.SubItem);
                }
            }
        }

        [XmlIgnore]
        public CimBasicClass CimClass { get; set; }
        public HSMSMessage Message { get; set; }
    }
}
