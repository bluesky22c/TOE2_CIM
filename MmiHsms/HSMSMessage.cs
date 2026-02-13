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
using System.Xml.Serialization;

namespace MMISoft.HSMS
{
    public enum ItemFormat { LIST, Binaray, Boolean, ASCII, JIS, WChar, Int64, Int8, Int16, Int32, Double, Float, UInt64, UInt8, UInt16, UInt32, Root }

    [Serializable]
    public class HSMSMessage
    {
        static public Encoding Encoding { get; set; } = Encoding.Default;
        const int maxIndent = 100;
        static byte[] fmtBit = {0x00, 0x20, 0x21, 0x40, 0x44, 
                                0x48, 0x60, 0x64, 0x68, 0x70, 
                                0x80, 0x90, 0xA0, 0xA4, 0xA8,
                                0xB0};


        public byte[] BinaryBuf { get; set; }

        public string Title { get; set; } = "No Title";
        public List<HSMSObject> Items { get; set; }

        public HSMSObject this[int no]
        {
            get { return Items[no]; }
            set { Items[no] = value; }
        }

        [XmlIgnore]
        public MessageHeader Header
        {
            get; set;
        }

        public int Stream
        {
            get { return Header.Stream; }
            set { Header.Stream = value; }
        }
        public int Function
        {
            get { return Header.Function; }
            set { Header.Function = value; }
        }
        public bool Wait
        {
            get { return Header.Wait; }
            set { Header.Wait = value; }
        }
   
        public int SubItem { get; set; }

        /// <summary>
        /// MessageItem들로부터 HSMS 텍스트를 얻는다
        /// </summary>
        /// <param name="items"></param>
        /// <param name="padString">ASCII의 경우 문자열의 뒷부분을 채울지 여부를 선택한다</param>
        /// <param name="includeIndent"></param>
        /// <param name="indentSize"></param>
        /// <returns></returns>
        public string ToString(bool padString = false, bool includeIndent = true, int indentSize = 4)
        {
            string str = Title + "\r\n";
            foreach (var item in Items)
            {
                str += item.FormatString(includeIndent, indentSize) + "\r\n";
            }
            return str;
        }

        /// <summary>
        /// 2013-1-17
        /// 주어진 구문을 해석해서 MessageItem[] 형태로 리턴한다.
        /// 클래스 내부에서는 사용되지 않으며 스트링 분석을 위해서 사용한다.
        /// </summary>
        /// <param name="text">입력문자열</param>
        /// <returns></returns>
        static void  MakeIndent(HSMSMessage msg)
        {
            List<HSMSObject> Items = msg.Items;
            int indent = 0;
            // Indent는 최대 100개 까지
            int[] listCount = new int[maxIndent];
            int[] ItemCount = new int[maxIndent];

            if (Items.Count > 0)
            {
                listCount[0] = 1;
            }
            try
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    HSMSObject item = Items[i];

                    while (ItemCount[indent] >= listCount[indent])
                    {
                        if (indent > 0)
                        {
                            indent--;
                        }
                        else
                        {
                            break;
                        }
                    }
                    item.IndentCount = indent;
                    switch (item.Type) // header
                    {
                        case ItemFormat.LIST:
                            ItemCount[indent]++;
                            indent++;
                            listCount[indent] = (int)item.ValueCore;
                            ItemCount[indent] = 0;
                            break;

                        default:
                            ItemCount[indent]++;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
                
            }
            for (int i = 0; i < maxIndent; i++)
            {
                if (listCount[i] != ItemCount[i])
                {
                    throw new HSMSMessageCountMissMatchException();
                }
            }
        }

        public bool SetHSMS(int stream, int function, int subItem, bool wait, string hsmsText)
        {
            Stream = stream;
            Function = function;
            Wait = wait;
            this.SubItem = subItem;

            Items.Clear();
            if (hsmsText == null)
            {
                return true;
            }
            else
            {
                string[] tokens = hsmsText.Split('<');
                for (int i = 1; i < tokens.Length; i++)
                {
                    Items.Add(HSMSObject.Parse(tokens[i]));
                }
            }
            MakeIndent(this);
            return true;
        }

        public bool SetHSMSContent(string hsmsText)
        {
            Items.Clear();
            if (hsmsText == null)
            {
                return true;
            }
            else
            {
                string[] tokens = hsmsText.Split('<');
                for (int i = 1; i < tokens.Length; i++)
                {
                    Items.Add(HSMSObject.Parse(tokens[i]));
                }
            }
            MakeIndent(this);
            return true;
        }


        byte[] GetBinaryBody()
        {
            byte[] data = new byte[100000];
            byte[] tmpData = null;
            int dataLen = 0;

            foreach (var item in Items)
            {
                string[] bodies = item.ToString().Split(',');
                string body = string.Join(",", bodies, 1, bodies.Length - 1).Split('>')[0];
                switch (item.Type) // header
                {
                    case ItemFormat.LIST:
                        tmpData = GetBytes(ItemFormat.LIST, body);
                        for (int j = 0; j < tmpData.Length; j++)
                        {
                            data[dataLen++] = tmpData[j];
                        }
                        break;

                    case ItemFormat.ASCII:
                        {
                            int len = body.Length;
                            body = body.PadRight(len, ' ');
                            tmpData = GetBytes(ItemFormat.ASCII, body);
                            for (int j = 0; j < tmpData.Length; j++)
                            {
                                data[dataLen++] = tmpData[j];
                            }
                        }
                        break;

                    default:
                        tmpData = GetBytes(item.Type, body);
                        for (int j = 0; j < tmpData.Length; j++)
                        {
                            data[dataLen++] = tmpData[j];
                        }
                        break;

                }
            }

            tmpData = data;
            data = new byte[dataLen];
            for (int i = 0; i < dataLen; i++)
            {
                data[i] = tmpData[i];
            }
            return data;
        }

        public HSMSMessage()
            :this(0)
        {
        }

        public HSMSMessage(int systemByte)
        {
            Items = new List<HSMSObject>();
            Header = new MessageHeader(systemByte);
        }

        public HSMSMessage(CimBasicClass cimClass)
            : this(0)
        {
            SetCimClass(cimClass);
        }

        public byte[] GetBytes()
        {
            byte[] data;
            int len = 10;
            byte[] binaryMessage = GetBinaryBody();
            if (binaryMessage != null)
            {
                data = new byte[binaryMessage.Length + 14];
                len = binaryMessage.Length + 10;
            }
            else
            {
                len = 10;
                data = new byte[14];
            }
            int indexBuffer = 0;

            // Message Length
            byte[] byteData = BigEndianConverter.GetBytes(len);
            for (int i = 0; i < byteData.Length; i++)
            {
                data[indexBuffer++] = byteData[i];
            }

            // Message Header
            byteData = Header.GetBytes();
            for (int i = 0; i < byteData.Length; i++)
            {
                data[indexBuffer++] = byteData[i];
            }

            // Message
            if (binaryMessage != null)
            {
                for (int i = 0; i < binaryMessage.Length; i++)
                {
                    data[indexBuffer++] = binaryMessage[i];
                }
            }
            return data;
        }

        static public HSMSMessage FromBinary(byte[] data)
        {
            HSMSMessage msg = new HSMSMessage();
            MessageHeader header = msg.Header;
            List<HSMSObject> items = msg.Items;

            msg.BinaryBuf = data;
            header.SetData(data, 4);

            if (header.SType != MessageHeader.STypes.DataMessage)
                return msg;
            int indent = 0;
            // Indent는 최대 100개 까지
            int[] listCount = new int[maxIndent];
            int[] ItemCount = new int[maxIndent];

            if (data.Length > 14)
            {
                listCount[0] = 1;
            }

            int index = 14;
            string localStr;
            // 데이터 길이가 차지하고 있는 바이트 수
            int len;
            byte[] lenStr = new byte[4];
            ItemFormat itemFormat;
            int count;
            try
            {
                for (int i = 0; ; i++)
                {
                    if (index >= data.Length)
                        break;

                    Format(data[index], out len, out itemFormat);

                    while (ItemCount[indent] >= listCount[indent])
                    {
                        if (indent > 0)
                        {
                            indent--;
                        }
                        else
                        {
                            break;
                        }
                    }

                    HSMSObject item = null;
                    switch (itemFormat)
                    {
                        case ItemFormat.LIST:
                            item = new ListType();
                            ItemCount[indent]++;
                            // count = (int)data[index + 1];
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            item.ValueCore = count;
                            item.Length = 1;
                            items.Add(item);
                            indent++;
                            listCount[indent] = count;
                            ItemCount[indent] = 0;

                            break;

                        case ItemFormat.ASCII:
                            item = new StringType();
                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            item.ValueCore = Encoding.GetString(data, index, count);
                            item.Length = count;
                            items.Add(item);
                            index += count;
                            break;

                        case ItemFormat.Binaray:
                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("B,{0}", data[index]);
                            for (int j = 1; j < count; j++)
                            {
                                localStr += string.Format(", {0}", data[index + j]);
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.Int64:

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("I8,{0}", BigEndianConverter.ToInt64(data, index));
                            for (int j = 1; j < count / 8; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToInt64(data, index + j * 8));
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.UInt64:

                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("U8,{0}", BigEndianConverter.ToUInt64(data, index));
                            for (int j = 1; j < count / 8; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToUInt64(data, index + j * 8));
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.Int32:

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("I4,{0}", BigEndianConverter.ToInt32(data, index));
                            for (int j = 1; j < count / 4; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToInt32(data, index + j * 4));
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.UInt32:

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("U4,{0}", BigEndianConverter.ToUInt32(data, index));
                            for (int j = 1; j < count / 4; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToUInt32(data, index + j * 4));
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.Int16:                            

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("I2,{0}", BigEndianConverter.ToInt16(data, index));
                            for (int j = 1; j < count / 2; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToInt16(data, index + j * 2));
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.UInt16:

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("U2,{0}", BigEndianConverter.ToUInt16(data, index));
                            for (int j = 1; j < count / 2; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToUInt16(data, index + j * 2));
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.Int8:

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("I1,{0}", (sbyte)data[index]);
                            for (int j = 1; j < count; j++)
                            {
                                localStr += string.Format(", {0}", (sbyte)data[index + j]);
                            }
                            index += count;
                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.UInt8:

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("U1,{0}", data[index]);
                            for (int j = 1; j < count; j++)
                            {
                                localStr += string.Format(", {0}", data[index + j]);
                            }
                            index += count;

                            items.Add(HSMSObject.Parse(localStr));
                            break;

                        case ItemFormat.Double:
                            item = new DoubleType();

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("F8,{0}", BigEndianConverter.ToDouble(data, index));
                            for (int j = 1; j < count / 8; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToDouble(data, index + j * 8));
                            }
                            index += count;
                            items.Add(item);
                            break;

                        case ItemFormat.Float:
                            item = new FloatType();

                            ItemCount[indent]++;
                            count = BigEndianConverter.ToInt32(data, index + 1, len);
                            index += len + 1;
                            localStr = string.Format("F4,{0}", BigEndianConverter.ToFloat(data, index));
                            for (int j = 1; j < count / 4; j++)
                            {
                                localStr += string.Format(", {0}", BigEndianConverter.ToFloat(data, index + j * 4));
                            }
                            index += count;
                            items.Add(item);
                            break;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            for (int i = 0; i < maxIndent; i++)
            {
                if (listCount[i] != ItemCount[i])
                {
                    throw new HSMSMessageCountMissMatchException();
                }
            }
            MakeIndent(msg);
            return msg;
        }

        static void Format(byte b, out int len, out ItemFormat format)
        {
            len = b & 0x03;
            format = ItemFormat.ASCII;
            bool find = false;
            for (int i = 0; i < fmtBit.Length; i++)
            {
                if ((b & 0xFC) == fmtBit[i])
                {
                    format = (ItemFormat)i;
                    find = true;
                    break;
                }
            }
            if (find == false)
            {
                throw new HSMSInvalidFormatExcepton();
            }
        }

        static byte[] GetBytes(ItemFormat format, string value)
        {
            string[] strArray;
            int intValue;
            byte[] dataLength;
            int dataLen = 1;
            byte[] data = null;
            switch (format)
            {
                case ItemFormat.LIST:
                    intValue = int.Parse(value);
                    if (intValue < 256)
                    {
                        data = new byte[2];
                        data[0] = (byte)(fmtBit[(byte)format] + 1);
                        data[1] = (byte)intValue;
                    }
                    else 
                    {
                        data = new byte[3];
                        data[0] = (byte)(fmtBit[(byte)format] + 2);
                        data[1] = (byte)(intValue / 256);
                        data[2] = (byte)(intValue % 256);
                    }
                    break;

                case ItemFormat.ASCII:
                    dataLen = 1;
                    if (value.Length >= 0x10000)
                    {
                        dataLen = 3;
                    }
                    else if (value.Length >= 0x100)
                    {
                        dataLen = 2;
                    }
                    dataLength = BigEndianConverter.GetBytes(value.Length);
                    data = new byte[1 + dataLen + value.Length];
                    // space 로 초기화
                    for (int i = 0; i < data.Length; i++)
                        data[i] = 0x20;
                    data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                    for (int i = 0; i < dataLen; i++)
                    {
                        data[1 + i] = dataLength[4 - dataLen + i];
                    }
                    //                    data[1] = (byte)strValue.Length;
                    var encodeBuf = Encoding.GetBytes(value);
                    Buffer.BlockCopy(encodeBuf, 0, data, 1 + dataLen, Math.Min(value.Length, encodeBuf.Length));
                    //Encoding.GetBytes(value, 0, value.Length, data, 1 + dataLen);
                    break;

                case ItemFormat.Binaray:
                    strArray = value.Split(',');

                    dataLen = 1;
                    if (strArray.Length >= 0x10000)
                    {
                        dataLen = 3;
                    }
                    else if (strArray.Length >= 0x100)
                    {
                        dataLen = 2;
                    }
                    dataLength = BigEndianConverter.GetBytes(strArray.Length);
                    data = new byte[1 + dataLen + strArray.Length];
                    data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                    for (int i = 0; i < dataLen; i++)
                    {
                        data[1 + i] = dataLength[4 - dataLen + i];
                    }

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        bool parse = byte.TryParse(strArray[i], out data[1 + i + dataLen]);
                        if (parse == false)
                        {
                            MessageBox.Show(string.Format("Binary Syntax Error: {0}", strArray[i]));
                        }
                    }
                    break;

                case ItemFormat.Int8:
                    strArray = value.Split(',');

                    dataLen = 1;
                    if (strArray.Length >= 0x10000)
                    {
                        dataLen = 3;
                    }
                    else if (strArray.Length >= 0x100)
                    {
                        dataLen = 2;
                    }
                    dataLength = BigEndianConverter.GetBytes(strArray.Length);
                    data = new byte[1 + dataLen + strArray.Length];
                    data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                    for (int i = 0; i < dataLen; i++)
                    {
                        data[1 + i] = dataLength[4 - dataLen + i];
                    }

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        sbyte sb;
                        bool parse = sbyte.TryParse(strArray[i], out sb);
                        if (parse == false)
                        {
                            MessageBox.Show(string.Format("Int8 Syntax Error: {0}", strArray[i]));
                        }
                        data[1 + i + dataLen] = (byte)sb;
                    }
                    break;

                case ItemFormat.UInt8:
                    strArray = value.Split(',');

                    dataLen = 1;
                    if (strArray.Length >= 0x10000)
                    {
                        dataLen = 3;
                    }
                    else if (strArray.Length >= 0x100)
                    {
                        dataLen = 2;
                    }
                    dataLength = BigEndianConverter.GetBytes(strArray.Length);
                    data = new byte[1 + dataLen + strArray.Length];
                    data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                    for (int i = 0; i < dataLen; i++)
                    {
                        data[1 + i] = dataLength[4 - dataLen + i];
                    }

                    for (int i = 0; i < strArray.Length; i++)
                    {
                        byte sb;
                        bool parse = byte.TryParse(strArray[i], out sb);
                        if (parse == false)
                        {
                            MessageBox.Show(string.Format("UInt8 Syntax Error: {0}", strArray[i]));
                        }
                        data[1 + i + dataLen] = (byte)sb;
                    }
                    break;

                case ItemFormat.Int16:
                    {
                        const int typeBytes = 2;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            short n;
                            bool parse = short.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("Int16 Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.UInt16:
                    {
                        // 
                        const int typeBytes = 2;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            ushort n;
                            bool parse = ushort.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("UInt16 Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.Int32:
                    {
                        // 
                        const int typeBytes = 4;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            int n;
                            bool parse = int.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("Int32 Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.UInt32:
                    {
                        // 
                        const int typeBytes = 4;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            uint n;
                            bool parse = uint.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("UInt32 Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.Int64:
                    {
                        // 
                        const int typeBytes = 8;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            long n;
                            bool parse = long.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("Int64 Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.UInt64:
                    {
                        // 
                        const int typeBytes = 8;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            ulong n;
                            bool parse = ulong.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("UInt64 Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.Double:
                    {
                        // 
                        const int typeBytes = 8;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            double n;
                            bool parse = double.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("Double Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;

                case ItemFormat.Float:
                    {
                        // 
                        const int typeBytes = 4;
                        strArray = value.Split(',');

                        dataLen = 1;
                        if (strArray.Length * typeBytes >= 0x10000)
                        {
                            dataLen = 3;
                        }
                        else if (strArray.Length * typeBytes >= 0x100)
                        {
                            dataLen = 2;
                        }
                        dataLength = BigEndianConverter.GetBytes(strArray.Length * typeBytes);
                        data = new byte[1 + dataLen + strArray.Length * typeBytes];
                        data[0] = (byte)(fmtBit[(byte)format] + dataLen);
                        for (int i = 0; i < dataLen; i++)
                        {
                            data[1 + i] = dataLength[4 - dataLen + i];
                        }

                        for (int i = 0; i < strArray.Length; i++)
                        {
                            float n;
                            bool parse = float.TryParse(strArray[i], out n);
                            if (parse == false)
                            {
                                MessageBox.Show(string.Format("Binary Syntax Error: {0}", strArray[i]));
                            }
                            else
                            {
                                byte[] numBytes = BigEndianConverter.GetBytes(n);
                                for (int j = 0; j < numBytes.Length; j++)
                                {
                                    data[1 + i * typeBytes + dataLen + j] = numBytes[j];
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;

            }
            return data;
        }

        

        void GetCimClass(string className, CimBasicClass cimClass)
        {
            cimClass.SetHSMS(this);
        }

        public void SetCimClass(CimBasicClass cimClass)
        {
            SetHSMS(cimClass.Stream, cimClass.Function, cimClass.SubItem, cimClass.Wait, cimClass.BodyString());
        }

        public override string ToString()
        {
            return ToString();
        }
    }

}