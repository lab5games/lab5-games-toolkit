using System;
using System.Text;

namespace Lab5Games
{
    public static class BinaryExtension
    {
        public static string ToBinaryString(this int value)
        {
            char[] buff = new char[32];

            for(int i=0; i<buff.Length; i++)
            {
                buff[i] = GetBit(value, i) > 0 ? '1' : '0';
            }

            Array.Reverse(buff);
            
            int cut = 8;
            StringBuilder sb = new StringBuilder();

            foreach(char c in buff)
            {
                if(cut == 0)
                {
                    cut = 8;
                    sb.Append(" ");
                }

                sb.Append(c);
                cut--;
            }

            return sb.ToString();
        }

        public static int GetBit(this int value, int index)
        {
            if (index < 0 || index > 31)
            {
                throw new IndexOutOfRangeException("index");
            }

            return (value & (1 << index)) != 0 ? 1 : 0;
        }

        public static int SetBit(this int value, int index, bool flag)
        {
            if (index < 0 || index > 31)
            {
                throw new IndexOutOfRangeException("index");
            }

            return ((value & ~(1 << index)) | ((flag ? 1 : 0) << index));
        }

        public static int Reverse(this int value)
        {
            return ~value;
        }
    }
}