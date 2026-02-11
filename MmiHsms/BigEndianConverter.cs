using System;
using System.Collections.Generic;
using System.Text;

namespace MMISoft.HSMS
{
    static class BigEndianConverter
    {
        static byte[] Reverse(byte[] buf, int startIndex, int count)
        {
            byte[] tmp = new byte[count];
            for (int i = 0; i < count; i++)
            {
                tmp[i] = buf[startIndex + count - i - 1];
            }
            return tmp;
        }

        static public int ToInt32(byte[] buf, int startIndex, int len)
        {
            byte[] intbuf = new byte[4];
            for (int i = 0; i < len; i++)
            {
                intbuf[i] = buf[startIndex + len - i - 1];
            }
            return BitConverter.ToInt32(intbuf, 0);
        }

        static public long ToInt64(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 8);
            return BitConverter.ToInt64(b, 0);
        }

        static public ulong ToUInt64(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 8);
            return BitConverter.ToUInt64(b, 0);
        }

        static public int ToInt32(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 4);
            return BitConverter.ToInt32(b, 0);
        }

        static public uint ToUInt32(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 4);
            return BitConverter.ToUInt32(b, 0);
        }

        static public short ToInt16(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 2);
            return BitConverter.ToInt16(b, 0);
        }

        static public ushort ToUInt16(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 2);
            return BitConverter.ToUInt16(b, 0);
        }

        static public double ToDouble(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 8);
            return BitConverter.ToDouble(b, 0);
        }

        static public double ToFloat(byte[] buf, int startIndex)
        {
            byte[] b = Reverse(buf, startIndex, 4);
            return BitConverter.ToSingle(b, 0);
        }

        static public byte[] GetBytes(int n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }

        static public byte[] GetBytes(uint n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }

        static public byte[] GetBytes(ushort n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }

        static public byte[] GetBytes(short n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }
        static public byte[] GetBytes(double n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }
        static public byte[] GetBytes(float n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }
        static public byte[] GetBytes(long n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }
        static public byte[] GetBytes(ulong n)
        {
            byte[] str = BitConverter.GetBytes(n);
            str = Reverse(str, 0, str.Length);
            return str;
        }
    
    }
}
