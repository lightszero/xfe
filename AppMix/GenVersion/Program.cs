using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GenVersion
{
    class Program
    {


        class Logger : CSLE.ICLS_Logger
        {

            public void Log(string str)
            {

                Console.ResetColor();
                Console.WriteLine(str);
            }

            public void Log_Warn(string str)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(str);
                Console.ResetColor();
            }

            public void Log_Error(string str)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(str);
                Console.ResetColor();
            }
        }
        static Logger logger;
        static CSLE.CLS_Environment scriptEnv;
        static void Main(string[] args)
        {

            logger = new Logger();
            scriptEnv = new CSLE.CLS_Environment(logger);


            sha1 = SHA1.Create();
            srcpath = System.IO.Path.GetFullPath("../");
            destpath = System.IO.Path.GetFullPath("../out");



            //检查源文件


            try
            {
                RegTest();

                List<string> files = new List<string>();
                CheckVersion(files);
                ParseVersion(files);
            }
            catch
            {

            }
            logger.Log("按回车键退出");
            Console.ReadLine();
        }

        private static void RegTest()
        {
            string[] reginfo = System.IO.File.ReadAllLines(System.IO.Path.Combine(srcpath, "txts\\regtype.txt"));
            foreach (var r in reginfo)
            {
                string[] s = r.Split(',');

                System.Reflection.Assembly asmb = null;
                try
                {
                    asmb = System.Reflection.Assembly.LoadFrom(s[1]);
                }
                catch
                {

                }

                try
                {
                    Type t = null;
                    if (asmb == null)
                    {
                        t = Type.GetType(s[0]);

                    }
                    else
                    {
                        t = asmb.GetType(s[0]);
                    }
                    if (t != null)
                    {

                        scriptEnv.RegType(new CSLE.RegHelper_Type(t, s[2]));
                        logger.Log_Warn("注册:" + s[2] + "  from" + s[0]);
                    }
                    else
                    {
                        logger.Log_Error("错误注册:" + s[2] + "  from" + s[0]);
                    }
                }
                catch
                {
                    logger.Log_Error("错误注册:" + s[2] + "  from" + s[0]);
                }

            }
        }
        static string srcpath;
        static string destpath;
        static SHA1 sha1 = null;
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
        }
        static void CheckVersion(List<string> files)
        {
            try
            {
                string[] srcdirs = System.IO.Directory.GetDirectories(srcpath);
                foreach (var sdir in srcdirs)
                {
                    string spath = System.IO.Path.GetFileName(sdir);
                    if (spath == "codes")//处理代码
                    {
                        logger.Log_Warn("================编译开始");
                        var fs = System.IO.Directory.GetFiles(sdir, "*.cs", System.IO.SearchOption.AllDirectories);
                        Dictionary<string, IList<CSLE.Token>> projs = new Dictionary<string, IList<CSLE.Token>>();
                        foreach (var f in fs)
                        {
                            //build
                            var token = scriptEnv.ParserToken(System.IO.File.ReadAllText(f));
                            projs[f.Replace(".cs", ".bytes")] = token;
                            //addbuildresult to fs
                        }
                        scriptEnv.Project_Compiler(projs, true);
                        logger.Log_Warn("================编译结束");
                        foreach (var f in projs)
                        {
                            using (System.IO.Stream s = System.IO.File.Create(f.Key))
                            {
                                scriptEnv.tokenParser.SaveTokenList(f.Value, s);
                                files.Add(f.Key);
                            }
                        }
                    }
                    else if (

                        spath == "imgs"
                        ||
                        spath == "txts"

                        )
                    {

                        var fs = System.IO.Directory.GetFiles(sdir, "*.*", System.IO.SearchOption.AllDirectories);
                        foreach (var f in fs)
                        {

                            //    var nf = f.Substring(info.srcpath.Length);
                            //    if (nf[0] == '\\') nf = nf.Substring(1);
                            files.Add(f.ToLower());
                        }

                    }
                    //files.AddRange(fs);
                }
            }
            catch
            {
                Console.WriteLine("检查源路径出错");
                throw new Exception("检查源路径出错");
                //return;
            }

            Console.WriteLine("文件数" + files.Count);
        }

        static void ParseVersion(List<string> files)
        {
            List<fileinfo> outfiles = new List<fileinfo>();
            try
            {
                int p = 0;
                foreach (var f in files)
                {
                    var nf = f.Substring(srcpath.Length);
                    if (nf[0] == '\\') nf = nf.Substring(1);
                    var dest = System.IO.Path.Combine(destpath, nf);
                    var destp = System.IO.Path.GetDirectoryName(dest);
                    if (System.IO.Directory.Exists(destp) == false)
                    {
                        System.IO.Directory.CreateDirectory(destp);
                    }
                    using (System.IO.Stream dsrc = System.IO.File.OpenRead(f))
                    {
                        byte[] bs = new byte[dsrc.Length];
                        dsrc.Read(bs, 0, bs.Length);
                        using (System.IO.Stream ddest = System.IO.File.Create(dest))
                        {
                            ddest.Write(bs, 0, bs.Length);
                        }
                        var finfo = new fileinfo();
                        finfo.filename = nf;
                        finfo.flen = bs.Length;
                        finfo.hash = sha1.ComputeHash(bs);
                        outfiles.Add(finfo);
                    }
                    //System.IO.File.Copy(f, dest);

                    p++;
                    Console.WriteLine("处理文件(" + p + "/" + files.Count + ")" + nf);
                }
            }
            catch
            {
                Console.WriteLine("处理文件出错");
                throw new Exception("处理文件出错");
            }

            //保存文件列表
            try
            {
                int totallen = 0;
                List<string> outdata = new List<string>();
                foreach (var f in outfiles)
                {
                    outdata.Add(f.ToString());
                    totallen += f.flen;
                }
                string filelistfile = System.IO.Path.Combine(destpath, "filelist.txt");
                System.IO.File.WriteAllLines(filelistfile, outdata);

                string infofile = System.IO.Path.Combine(destpath, "ver.txt");
                using (System.IO.Stream s = System.IO.File.OpenRead(filelistfile))
                {
                    byte[] bs = new byte[s.Length];
                    s.Read(bs, 0, bs.Length);
                    fileinfo _info = new fileinfo();
                    _info.filename = "filelist.txt";
                    _info.flen = totallen;
                    _info.hash = sha1.ComputeHash(bs);
                    System.IO.File.WriteAllText(infofile, _info.ToString());
                    Console.WriteLine("生成列表文件:" + infofile);
                }
            }
            catch
            {
                Console.WriteLine("生成列表文件出错");
                throw new Exception("生成列表文件出错");
            }

        }
    }
}
