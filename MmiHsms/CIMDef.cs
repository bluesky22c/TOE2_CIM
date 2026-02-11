using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MMISoft.HSMS;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MMISoft.HSMS
{
    #region 기본 클래스 설정

    [Serializable]
    public abstract class HSMSObject
    {

        // 문자열에서 포맷을 찾는다
        static private Dictionary<string, ItemFormat> string2Format = new Dictionary<string, ItemFormat>();
        static public string[] ItemFormatString = new string[] { "L", "B", "Q", "A", "JIS", "WCHAR", "I8", "I1", "I2", "I4", "F8", "F4", "U8", "U1", "U2", "U4", "Root" };

        static private Type[] netType = { typeof(int), typeof(byte), typeof(bool), typeof(char), typeof(char), typeof(char), 
                typeof(long), typeof(sbyte), typeof(short), typeof(int), typeof(double), typeof(float), 
                typeof(ulong), typeof(byte), typeof(ushort), typeof(uint), typeof(char)};

        public HSMSObject(ItemFormat type, int length, string desc = "")
        {
            Init();
            Type = type;
            Length = length;
            if (string.IsNullOrEmpty(desc))
            {
                if (GetType().ReflectedType != null)
                {
                    desc = GetType().ReflectedType.Name;
                }
            }
            Desc = desc;

        }
        public ItemFormat Type { get; private set; }
        public int Length { get; set; }
        public object ValueCore { get; set; }

        private string desc;

        public string Desc { 
            get => desc; 
            set => desc = value; 
        }
        public int IndentCount {get; set;}
        static void Init()
        {
            if (string2Format.Count == 0)
            {
                for (int i = 0; i < ItemFormatString.Length; i++)
                {
                    string2Format.Add(ItemFormatString[i], (ItemFormat)i);
                }
            }
        }

        static public HSMSObject Parse(string objectString)
        {
            HSMSObject item = null;

            string str = objectString;

            string[] token = str.Split('\'');
            string desc = "";
            if (token.Length > 1)
            {
                desc = token[1].TrimEnd('\t', '\n', '\r', ' ', '>');
            }
            str = token[0].Split('>')[0];

            int pos = str.IndexOf(',');
            if (pos == -1)
                return null;
            string code = str.Substring(0, pos);
            string body = str.Substring(pos + 1);

            // Parameter의 개수에 대한 처리를 한다
            // A[xx]에 대한 처리를 한다.
            int countA = 0;
            Regex r;
            Match m;
            string pat = @"\[(?<N>\d+)\]";
            r = new Regex(pat, RegexOptions.IgnoreCase);
            m = r.Match(code);

            if (m.Success)
            {
                code = code[0].ToString();
                countA = int.Parse(m.Groups["N"].ToString());
            }

            string[] values = body.Split(',');

            switch (code) // header
            {
                case "L":
                    item = new ListType();
                    item.Type = ItemFormat.LIST;
                    item.Length = 1;
                    item.ValueCore = int.Parse(values[0]);
                    break;

                case "A":
                    {
                        item = new StringType();
                        item.Type = ItemFormat.ASCII;

                        int len = body.Length;
                        if (countA > 0)
                        {
                            len = countA;
                        }
                        item.Length = len;
                        item.ValueCore = body;

                    }
                    break;

                default:
                    {
                        int length = body.Split(',').Length;
                        item = CreateInstance(string2Format[code], length);
                        item.Length = body.Split(',').Length;

                        if (item.Length == 1)
                        {
                            MethodInfo mi = netType[(int)item.Type].GetMethod("Parse", new Type[] { typeof(string)});
                            try
                            {
                                item.ValueCore = mi.Invoke(netType[(int)item.Type].GetType(), new object[] { values[0] });
                            }
                            catch
                            {
                                throw new HSMSMInvalidData(string.Format("다음 라인을 해석할 수 없습니다. {0}", objectString));   
                            }
//                            MethodInfo mi = netType[(int)item.Type].GetMethod("TryParse", new Type[] { typeof(string), netType[(int)item.Type].MakeByRefType() });
//                            object outValue = Activator.CreateInstance(netType[(int)item.Type]);
//                           byte outValue = 10;
//                            bool ok = (bool)mi.Invoke(null, new object[] { values[0], outValue });
//                            byte out2 = outValue;
                        }
                        else
                        {
                            MethodInfo mi = netType[(int)item.Type].GetMethod("Parse", new Type[] { typeof(string) });
                            try
                            {
                                Array value = Array.CreateInstance(netType[(int)item.Type], item.Length);
                                for (int i = 0; i < item.Length; i++)
                                {
                                    object tmp = mi.Invoke(netType[(int)item.Type].GetType(), new object[] { values[i] });
                                    value.SetValue(tmp, i);
                                }
                                item.ValueCore = value;
                            }
                            catch
                            {
                                throw new HSMSMInvalidData(string.Format("다음 라인을 해석할 수 없습니다. {0}", objectString));
                            }
                        }
                    }
                    break;
            }
            item.Desc = desc;
            return item;
        }

        static HSMSObject CreateInstance(ItemFormat format, int length)
        {
            ItemFormat[] formats = { ItemFormat.ASCII, ItemFormat.Binaray, ItemFormat.Double, ItemFormat.Float, 
                                       ItemFormat.Int16, ItemFormat.Int32, ItemFormat.Int64, ItemFormat.Int8,
                                       ItemFormat.LIST, ItemFormat.UInt16, ItemFormat.UInt32, ItemFormat.UInt64, ItemFormat.UInt8 };
            Type[] Types = { typeof(StringType), typeof(BinaryType), typeof(DoubleType), typeof(FloatType),
                               typeof(I2Type), typeof(I4Type), typeof(I8Type), typeof(I1Type),
                               typeof(ListType), typeof(U2Type), typeof(U4Type), typeof(U8Type), typeof(U1Type)};
            for (int i = 0; i < formats.Length; i++)
            {
                if (formats[i] == format)
                {
                    return (HSMSObject)Activator.CreateInstance(Types[i]);
                }
            }
            return null;

        }

        /// <summary>
        /// Class 이름을 이용해서 로그에 사용할 주석을 만든다.
        /// </summary>
        void MakeDesc()
        {
            //PropertyInfo piDesc = GetType().GetProperty("Desc");
            //if (piDesc != null)
            //{
            //    var desc = (string)piDesc.GetValue(this);
            //    if(string.IsNullOrWhiteSpace(desc))
            //        piDesc.SetValue(this, piDesc.ReflectedType.Name, null);
            //}
        }


        public string FormatString(bool includeIndent = true, int indentSize = 4)
        {

            string formatString = "<A, >";
            string dataString = "";
            if (Type == ItemFormat.LIST)
            {
                dataString = ValueCore.ToString();
            }
            else if (Type != ItemFormat.ASCII)
            {
                if (Length == 1)
                {
                    dataString = ValueCore.ToString();
                }
                else
                {
                    object[] array = new object[Length];
                    Array.Copy((Array)ValueCore, array, Length);
                    for (int i = 0; i < Length; i++)
                    {
                        dataString += array[i].ToString() + ",";
                    }
                    dataString = dataString.TrimEnd(',');
                }
            }
            string indent = includeIndent ? "".PadRight(indentSize * IndentCount) : string.Empty;

            switch (Type)
            {
                case ItemFormat.ASCII:
                    formatString = string.Format("<A[{1}],{0}>", ValueCore.ToString().PadRight(Length), Length);
                    break;

                case ItemFormat.Binaray:
                    formatString = string.Format("<B,{0}>", dataString);
                    break;

                case ItemFormat.UInt8:
                    formatString = string.Format("<U1,{0}>", dataString);
                    break;

                case ItemFormat.UInt16:
                    formatString = string.Format("<U2,{0}>", dataString);
                    break;

                case ItemFormat.UInt32:
                    formatString = string.Format("<U4,{0}>", dataString);
                    break;

                case ItemFormat.Int8:
                    formatString = string.Format("<I1,{0}>", dataString);
                    break;

                case ItemFormat.Int16:
                    formatString = string.Format("<I2,{0}>", dataString);
                    break;

                case ItemFormat.Int32:
                    formatString = string.Format("<I4,{0}>", dataString);
                    break;

                case ItemFormat.Double:
                    formatString = string.Format("<F8,{0}>", dataString);
                    break;

                case ItemFormat.Float:
                    formatString = string.Format("<F4,{0}>", dataString);
                    break;

                case ItemFormat.LIST:
                    formatString = string.Format("<L,{0}>", dataString);
                    break;
            }
            MakeDesc();
            if (Desc != null && Desc != string.Empty)
            {
                formatString += "'" + Desc;
            }
            return indent + formatString;
        }


        public override string ToString()
        {
            return FormatString(false, 0);
        }
    }

    /// <summary>
    /// Type A[]인 경우에 대한 기본 클래스
    /// </summary>
    [Serializable]
    public class StringType : HSMSObject 
    {

        public StringType(string desc = "")
        : base(ItemFormat.ASCII, 0, desc)
        {
            Value = "";
        }

        public StringType(string value, int length, string desc = "")
            : base(ItemFormat.ASCII, length, desc) 
        {
            if (value == null)
            {
                value = "";
            }
            this.Value = value;
        }

        public StringType(int length, string desc = "")
            : base(ItemFormat.ASCII, length, desc)
        {
            Value = "";
        }

        static public implicit operator string(StringType str)
        {
            return str.Value;
        }

        public string Value 
        {
            get
            {
                if (base.ValueCore == null)
                {
                    return "NULL";
                }
                return ((string) base.ValueCore).TrimEnd(' ');
            }
            set
            {
                if (value == null)
                {
                    base.ValueCore = "NULL";
                }
                if (value.Length > Length)
                {
                    value = value.Substring(0, Length);
                }
                base.ValueCore = value.TrimEnd(' ');
            }
        }
    }

    /// <summary>
    /// Type U1, U2, U4, B, I1, I2, I4 인경우 기본 클래스
    /// </summary>
    [Serializable]
    public abstract class UType : HSMSObject
    {
        public UType(ItemFormat type, int len = 1)
            : base(type, len)
        {
            base.ValueCore = 0;

        }

        public int Value
        {
            get
            {
                return Convert.ToInt32(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
    }

    //[Serializable]
    //public class FloatType : HSMSObject
    //{
    //    public FloatType(int len = 1)
    //        : base(ItemFormat.Float, len)
    //    {
    //        base.ValueCore = (float)0;
    //    }

    //    public float Value
    //    {
    //        get
    //        {
    //            return Convert.ToSingle(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = (float)value;
    //        }
    //    }
    //}

    //[Serializable]
    //public class DoubleType : HSMSObject
    //{
    //    public DoubleType(int len = 1)
    //        : base(ItemFormat.Double, len)
    //    {
    //        base.ValueCore = (double)0;
    //    }

    //    public double Value
    //    {
    //        get
    //        {
    //            return Convert.ToDouble(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }
    //}

    [Serializable]
    public class ListType : HSMSObject
    {
        public ListType()
            : base(ItemFormat.LIST, 1)
        {
            base.ValueCore = 0;
        }

        public ListType(int value)
            : base(ItemFormat.LIST, 1)
        {
            base.ValueCore = (int)value;
        }

    }

    ///// <summary>
    ///// Type U1
    ///// </summary>
    //[Serializable]
    //public class U1Type : HSMSObject
    //{
    //    public U1Type() : this(0)
    //    {

    //    }
    //    public U1Type(int value, string desc = "")
    //        : base(ItemFormat.UInt8, 1, desc)
    //    {
    //        Value = (byte)value;
    //    }

    //    public U1Type(int[] value, int len = 1, string desc = "")
    //        : base(ItemFormat.UInt8, len, desc)
    //    {
    //        Values = ConvertEx.ToUInt8Array(value);
    //    }

    //    public byte Value
    //    {
    //        get
    //        {
    //            return Convert.ToByte(ValueCore);

    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }
    //    public byte[] Values
    //    {
    //        get
    //        {
    //            return ConvertEx.ToUInt8Array(ValueCore);

    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }
    //}

    ///// <summary>
    ///// Type U2
    ///// </summary>
    //[Serializable]
    //public class U2Type : HSMSObject
    //{
    //    public U2Type() : this(0)
    //    {

    //    }
    //    public U2Type(int value, int len = 1, string desc = "")
    //        : base(ItemFormat.UInt16, len, desc)
    //    {
    //        Value = (ushort)value;
    //    }

    //    public U2Type(int[] value, int len = 1, string desc = "") : base(ItemFormat.UInt16, len, desc)
    //    {
    //        Values = ConvertEx.ToUInt16Array(value);
    //    }

    //    public U2Type(ushort[] value, int len = 1, string desc = "") : base(ItemFormat.UInt16, len, desc)
    //    {
    //        Values = ConvertEx.ToUInt16Array(value);
    //    }

    //    public ushort Value
    //    {
    //        get
    //        {
    //            return Convert.ToUInt16(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //    public ushort[] Values
    //    {
    //        get
    //        {
    //            return ConvertEx.ToUInt16Array(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //}

    ///// <summary>
    ///// Type U4
    ///// </summary>
    //[Serializable]
    //public class U4Type : HSMSObject
    //{
    //    public U4Type() : this(0)
    //    {

    //    }
    //    public U4Type(int value, string desc = "" )
    //        : base(ItemFormat.UInt32, 1, desc)
    //    {
    //        Value = (uint)value;
    //    }

    //    public U4Type(int[] value, int len = 1, string desc = "")
    //        : base(ItemFormat.UInt32, len, desc)
    //    {
    //        Values = ConvertEx.ToUInt32Array(value);
    //    }

    //    public uint Value
    //    {
    //        get
    //        {
    //            return Convert.ToUInt32(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //    public uint[] Values
    //    {
    //        get
    //        {
    //            return ConvertEx.ToUInt32Array(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //}

    //[Serializable]
    //public class U8Type : HSMSObject
    //{
    //    public U8Type() : this(0)
    //    {

    //    }
    //    public U8Type(ulong value, int len = 1, string desc = "")
    //        : base(ItemFormat.Int64, len, desc)
    //    {
    //        Value = (ulong)value;
    //    }
    //    public ulong Value
    //    {
    //        get
    //        {
    //            if (ValueCore.GetType().IsArray)
    //                return ConvertEx.ToUInt64Array(ValueCore)[0];
    //            else
    //                return Convert.ToUInt64(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //}

    //[Serializable]
    //public class I1Type : HSMSObject
    //{
    //    public I1Type() : this(0)
    //    {

    //    }
    //    public I1Type(int value, int len = 1, string desc = "") : base(ItemFormat.Int8, len, desc)
    //    {
    //        Value = (sbyte)value;
    //    }

    //    public sbyte Value
    //    {
    //        get
    //        {
    //            return Convert.ToSByte(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }


    //}

    //[Serializable]
    //public class I2Type : HSMSObject
    //{
    //    public I2Type() : this(0)
    //    {

    //    }
    //    public I2Type(int value, int len = 1, string desc = "") : base(ItemFormat.Int16, len, desc)
    //    {
    //        Value = (short)value;
    //    }
    //    public short Value
    //    {
    //        get
    //        {
    //            return Convert.ToInt16(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //}

    //[Serializable]
    //public class I4Type : HSMSObject
    //{
    //    public I4Type() : this(0)
    //    {

    //    }
    //    public I4Type(int value, int len = 1, string desc = "") : base(ItemFormat.Int32, len, desc)
    //    {
    //        Value = (int)value;
    //    }
    //    public int Value
    //    {
    //        get
    //        {
    //            return Convert.ToInt32(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //}


    //[Serializable]
    //public class I8Type : HSMSObject
    //{
    //    public I8Type() : this(0)
    //    {

    //    }
    //    public I8Type(long value, int len = 1, string desc = "") : base(ItemFormat.Int64, len, desc)
    //    {
    //        Value = (long)value;
    //    }
    //    public long Value
    //    {
    //        get
    //        {
    //            return Convert.ToInt64(ValueCore);
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }

    //}

    /// <summary>
    /// U1[] 과 같은 경우 기본 클래스
    /// </summary>
    [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
    [Serializable]
    public class UArrayType : HSMSObject
    {
        public UArrayType()
            : base(ItemFormat.UInt16, 2)
        {
            base.ValueCore = new int[2];
        }

        public UArrayType(ItemFormat type, int length)
            : base(type, length)
        {
            base.ValueCore = new int[length];
        }

        public int[] Value
        {
            get
            {
                return (int[])base.ValueCore;
            }
            set
            {
                base.ValueCore = value;
            }
        }
    }

    /// <summary>
    /// Float[] 과 같은 경우 기본 클래스
    /// </summary>
    [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
    [Serializable]
    public class FloatArrayType : HSMSObject
    {
        public FloatArrayType(ItemFormat type, int length)
            : base(type, length)
        {
            base.ValueCore = new float[length];
        }

        public float[] Value
        {
            get
            {
                //return (float[])base.ValueCore;
                Array array = (Array)base.ValueCore;
                var f = new float[array.Length];
                for (int i = 0; i < array.Length; i++)
                    f[i] = Convert.ToSingle(array.GetValue(i));
                return f;
            }
            set
            {
                base.ValueCore = value;
            }
        }
    }

    /// <summary>
    /// Float[] 과 같은 경우 기본 클래스
    /// </summary>
    [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
    [Serializable]
    public class DoubleArrayType : HSMSObject
    {
        public DoubleArrayType(ItemFormat type, int length)
            : base(type, length)
        {
            base.ValueCore = new double[length];
        }

        public double[] Value
        {
            get
            {
                //return (double[])base.ValueCore;
                Array array = (Array)base.ValueCore;
                var f = new double[array.Length];
                for (int i = 0; i < array.Length; i++)
                    f[i] = Convert.ToDouble(array.GetValue(i));
                return f;
            }
            set
            {
                base.ValueCore = value;
            }
        }
    }



    //[Serializable]
    //public class BinaryType : HSMSObject
    //{
    //    public BinaryType() : this(0)
    //    {

    //    }
    //    public BinaryType(int value, int len = 1, string desc = "") : base(ItemFormat.Binaray, len, desc)
    //    {
    //        Value = (byte)value;
    //    }

    //    public byte Value
    //    {
    //        get
    //        {
    //            return byte.Parse(base.ValueCore.ToString());
    //        }
    //        set
    //        {
    //            base.ValueCore = value;
    //        }
    //    }
    //}
    #endregion

    public static class ConvertEx
    {
        public static double[] ToDoubleArray(object value)
        {
            Array array = (Array)value;
            var f = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToDouble(array.GetValue(i));
            return f;
        }

        public static float[] ToSingleArray(object value)
        {
            Array array = (Array)value;
            var f = new float[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToSingle(array.GetValue(i));
            return f;
        }

        public static sbyte[] ToSByteArray(object value)
        {
            Array array = (Array)value;
            var f = new sbyte[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToSByte(array.GetValue(i));
            return f;
        }
        public static short[] ToInt16Array(object value)
        {
            Array array = (Array)value;
            var f = new short[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToInt16(array.GetValue(i));
            return f;
        }
        public static int[] ToInt32Array(object value)
        {
            Array array = (Array)value;
            var f = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToInt32(array.GetValue(i));
            return f;
        }
        public static long[] ToInt64Array(object value)
        {
            Array array = (Array)value;
            var f = new long[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToInt64(array.GetValue(i));
            return f;
        }

        public static byte[] ToByteArray(object value)
        {
            Array array = (Array)value;
            var f = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToByte(array.GetValue(i));
            return f;
        }
        public static ushort[] ToUInt16Array(object value)
        {
            Array array = (Array)value;
            var f = new ushort[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToUInt16(array.GetValue(i));
            return f;
        }
        public static uint[] ToUInt32Array(object value)
        {
            Array array = (Array)value;
            var f = new uint[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToUInt32(array.GetValue(i));
            return f;
        }
        public static ulong[] ToUInt64Array(object value)
        {
            Array array = (Array)value;
            var f = new ulong[array.Length];
            for (int i = 0; i < array.Length; i++)
                f[i] = Convert.ToUInt64(array.GetValue(i));
            return f;
        }

    }

}
