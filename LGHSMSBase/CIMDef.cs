  

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


    /// <summary>
    /// Type FloatType
    /// </summary>
    [Serializable]
    public class FloatType : HSMSObject
    {
        public FloatType() : this((float)0)
        {

        }
        public FloatType(float value, int len = 1, string desc = "")
            : base(ItemFormat.Float, len, desc)
        {
            Value = (float)value;
        }

        public FloatType(float[] value, int len = 1, string desc = "")
            : base(ItemFormat.Float, len, desc)
        {
            if (value == null)
                value = new float[len];
            Values = ConvertEx.ToSingleArray(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public FloatType(int value, string desc = "")
            : base(ItemFormat.Float, 1, desc)
        {
            Value = (float)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public FloatType(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Float, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToSingleArray(value);
        }

        public float Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToSingleArray(ValueCore)[0];
                else
                    return Convert.ToSingle(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public float[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToSingleArray(ValueCore);
                else
                    return new float[] { Convert.ToSingle(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new float[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type DoubleType
    /// </summary>
    [Serializable]
    public class DoubleType : HSMSObject
    {
        public DoubleType() : this((double)0)
        {

        }
        public DoubleType(double value, int len = 1, string desc = "")
            : base(ItemFormat.Double, len, desc)
        {
            Value = (double)value;
        }

        public DoubleType(double[] value, int len = 1, string desc = "")
            : base(ItemFormat.Double, len, desc)
        {
            if (value == null)
                value = new double[len];
            Values = ConvertEx.ToDoubleArray(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public DoubleType(int value, string desc = "")
            : base(ItemFormat.Double, 1, desc)
        {
            Value = (double)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public DoubleType(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Double, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToDoubleArray(value);
        }

        public double Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToDoubleArray(ValueCore)[0];
                else
                    return Convert.ToDouble(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public double[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToDoubleArray(ValueCore);
                else
                    return new double[] { Convert.ToDouble(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new double[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type I1Type
    /// </summary>
    [Serializable]
    public class I1Type : HSMSObject
    {
        public I1Type() : this((sbyte)0)
        {

        }
        public I1Type(sbyte value, int len = 1, string desc = "")
            : base(ItemFormat.Int8, len, desc)
        {
            Value = (sbyte)value;
        }

        public I1Type(sbyte[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int8, len, desc)
        {
            if (value == null)
                value = new sbyte[len];
            Values = ConvertEx.ToSByteArray(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public I1Type(int value, string desc = "")
            : base(ItemFormat.Int8, 1, desc)
        {
            Value = (sbyte)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public I1Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int8, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToSByteArray(value);
        }

        public sbyte Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToSByteArray(ValueCore)[0];
                else
                    return Convert.ToSByte(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public sbyte[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToSByteArray(ValueCore);
                else
                    return new sbyte[] { Convert.ToSByte(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new sbyte[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type I2Type
    /// </summary>
    [Serializable]
    public class I2Type : HSMSObject
    {
        public I2Type() : this((short)0)
        {

        }
        public I2Type(short value, int len = 1, string desc = "")
            : base(ItemFormat.Int16, len, desc)
        {
            Value = (short)value;
        }

        public I2Type(short[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int16, len, desc)
        {
            if (value == null)
                value = new short[len];
            Values = ConvertEx.ToInt16Array(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public I2Type(int value, string desc = "")
            : base(ItemFormat.Int16, 1, desc)
        {
            Value = (short)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public I2Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int16, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToInt16Array(value);
        }

        public short Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToInt16Array(ValueCore)[0];
                else
                    return Convert.ToInt16(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public short[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToInt16Array(ValueCore);
                else
                    return new short[] { Convert.ToInt16(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new short[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type I4Type
    /// </summary>
    [Serializable]
    public class I4Type : HSMSObject
    {
        public I4Type() : this((int)0)
        {

        }
        public I4Type(int value, int len = 1, string desc = "")
            : base(ItemFormat.Int32, len, desc)
        {
            Value = (int)value;
        }

        public I4Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int32, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToInt32Array(value);
        }


        public int Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToInt32Array(ValueCore)[0];
                else
                    return Convert.ToInt32(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public int[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToInt32Array(ValueCore);
                else
                    return new int[] { Convert.ToInt32(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new int[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type I8Type
    /// </summary>
    [Serializable]
    public class I8Type : HSMSObject
    {
        public I8Type() : this((long)0)
        {

        }
        public I8Type(long value, int len = 1, string desc = "")
            : base(ItemFormat.Int64, len, desc)
        {
            Value = (long)value;
        }

        public I8Type(long[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int64, len, desc)
        {
            if (value == null)
                value = new long[len];
            Values = ConvertEx.ToInt64Array(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public I8Type(int value, string desc = "")
            : base(ItemFormat.Int64, 1, desc)
        {
            Value = (long)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public I8Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Int64, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToInt64Array(value);
        }

        public long Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToInt64Array(ValueCore)[0];
                else
                    return Convert.ToInt64(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public long[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToInt64Array(ValueCore);
                else
                    return new long[] { Convert.ToInt64(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new long[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type U1Type
    /// </summary>
    [Serializable]
    public class U1Type : HSMSObject
    {
        public U1Type() : this((byte)0)
        {

        }
        public U1Type(byte value, int len = 1, string desc = "")
            : base(ItemFormat.UInt8, len, desc)
        {
            Value = (byte)value;
        }

        public U1Type(byte[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt8, len, desc)
        {
            if (value == null)
                value = new byte[len];
            Values = ConvertEx.ToByteArray(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U1Type(int value, string desc = "")
            : base(ItemFormat.UInt8, 1, desc)
        {
            Value = (byte)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U1Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt8, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToByteArray(value);
        }

        public byte Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToByteArray(ValueCore)[0];
                else
                    return Convert.ToByte(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public byte[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToByteArray(ValueCore);
                else
                    return new byte[] { Convert.ToByte(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new byte[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type U2Type
    /// </summary>
    [Serializable]
    public class U2Type : HSMSObject
    {
        public U2Type() : this((ushort)0)
        {

        }
        public U2Type(ushort value, int len = 1, string desc = "")
            : base(ItemFormat.UInt16, len, desc)
        {
            Value = (ushort)value;
        }

        public U2Type(ushort[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt16, len, desc)
        {
            if (value == null)
                value = new ushort[len];
            Values = ConvertEx.ToUInt16Array(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U2Type(int value, string desc = "")
            : base(ItemFormat.UInt16, 1, desc)
        {
            Value = (ushort)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U2Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt16, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToUInt16Array(value);
        }

        public ushort Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToUInt16Array(ValueCore)[0];
                else
                    return Convert.ToUInt16(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public ushort[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToUInt16Array(ValueCore);
                else
                    return new ushort[] { Convert.ToUInt16(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new ushort[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type U4Type
    /// </summary>
    [Serializable]
    public class U4Type : HSMSObject
    {
        public U4Type() : this((uint)0)
        {

        }
        public U4Type(uint value, int len = 1, string desc = "")
            : base(ItemFormat.UInt32, len, desc)
        {
            Value = (uint)value;
        }

        public U4Type(uint[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt32, len, desc)
        {
            if (value == null)
                value = new uint[len];
            Values = ConvertEx.ToUInt32Array(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U4Type(int value, string desc = "")
            : base(ItemFormat.UInt32, 1, desc)
        {
            Value = (uint)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U4Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt32, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToUInt32Array(value);
        }

        public uint Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToUInt32Array(ValueCore)[0];
                else
                    return Convert.ToUInt32(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public uint[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToUInt32Array(ValueCore);
                else
                    return new uint[] { Convert.ToUInt32(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new uint[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type U8Type
    /// </summary>
    [Serializable]
    public class U8Type : HSMSObject
    {
        public U8Type() : this((ulong)0)
        {

        }
        public U8Type(ulong value, int len = 1, string desc = "")
            : base(ItemFormat.UInt64, len, desc)
        {
            Value = (ulong)value;
        }

        public U8Type(ulong[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt64, len, desc)
        {
            if (value == null)
                value = new ulong[len];
            Values = ConvertEx.ToUInt64Array(value);
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U8Type(int value, string desc = "")
            : base(ItemFormat.UInt64, 1, desc)
        {
            Value = (ulong)value;
        }

//        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public U8Type(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.UInt64, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToUInt64Array(value);
        }

        public ulong Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToUInt64Array(ValueCore)[0];
                else
                    return Convert.ToUInt64(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public ulong[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToUInt64Array(ValueCore);
                else
                    return new ulong[] { Convert.ToUInt64(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new ulong[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }


    /// <summary>
    /// Type BinaryType
    /// </summary>
    [Serializable]
    public class BinaryType : HSMSObject
    {
        public BinaryType() : this((byte)0)
        {

        }
        public BinaryType(byte value, int len = 1, string desc = "")
            : base(ItemFormat.Binaray, len, desc)
        {
            Value = (byte)value;
        }

        public BinaryType(byte[] value, int len = 1, string desc = "")
            : base(ItemFormat.Binaray, len, desc)
        {
            if (value == null)
                value = new byte[len];
            Values = ConvertEx.ToByteArray(value);
        }
        /*
        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public BinaryType(int value, string desc = "")
            : base(ItemFormat.Binaray, 1, desc)
        {
            Value = (byte)value;
        }

        [Obsolete("사용하지 말것, 기존 호환성을 위해 존재")]
        public BinaryType(int[] value, int len = 1, string desc = "")
            : base(ItemFormat.Binaray, len, desc)
        {
            if (value == null)
                value = new int[len];
            Values = ConvertEx.ToByteArray(value);
        }
        */
        public byte Value
        {
            get
            {
                if (ValueCore.GetType().IsArray)   
                    return ConvertEx.ToByteArray(ValueCore)[0];
                else
                    return Convert.ToByte(ValueCore);
            }
            set
            {
                base.ValueCore = value;
            }
        }
        public byte[] Values
        {
            get
            {
                if (ValueCore.GetType().IsArray)
                    return ConvertEx.ToByteArray(ValueCore);
                else
                    return new byte[] { Convert.ToByte(ValueCore) };
            }
            set
            {
                /// ARRAY가 아니면 이 로직에 들어올수가 없기 때문에 IsArray 필요 없음
                var val = new byte[Length];
                Array.Copy(value, val, Math.Min(Length, value.Length));
                base.ValueCore = val;
            }
        }
    }

    #endregion
}
