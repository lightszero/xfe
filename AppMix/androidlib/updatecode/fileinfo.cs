using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace update
{
    class fileinfo
    {
        public string filename;
        public int flen;
        public byte[] hash;
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(filename + "|" + flen.ToString() + "|");
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        public static fileinfo Read(string str)
        {
            fileinfo f = new fileinfo();
            string[] ss = str.Split('|');
            f.filename = ss[0];
            if(System.IO.Path.DirectorySeparatorChar=='/')
            {
                f.filename=f.filename.Replace('\\','/');
            }
            else
            {
                f.filename=f.filename.Replace('/','\\');
            }
            f.flen = int.Parse(ss[1]);
            f.hash = new byte[ss[2].Length / 2];
            for (int i = 0; i < ss[2].Length / 2; i++)
            {
                string hex = ss[2].Substring(i * 2, 2);
                f.hash[i] = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            }
            return f;
        }
        public bool TestHash(byte[] bs)
        {
            if (bs.Length != hash.Length) return false;
            for (int i = 0; i < hash.Length; i++)
            {
                if (bs[i] != hash[i]) return false;
            }
            return true;
        }
        public override bool Equals(object obj)
        {
            fileinfo i = obj as fileinfo;
            if (i == null) return false;
            if (this.filename != i.filename) return false;
            if (this.flen != i.flen) return false;
            return TestHash(i.hash);
        }
    }
}
