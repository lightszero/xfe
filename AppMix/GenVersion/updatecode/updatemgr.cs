using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net;


namespace update
{
    public class MyWebClient:WebClient
    {
        public float Timeout
        {
            get;
            set;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request= base.GetWebRequest(address);
            request.Timeout = (int)(this.Timeout*1000.0f);
            return request;
        }
    }
    public class updatemgr
    {
        public updatemgr()
        {
            wc=new MyWebClient();
            wc.Timeout=1;
            wc.Encoding = System.Text.Encoding.UTF8;
            sha1 = SHA1.Create();
        }
        public MyWebClient wc
        {
            get;
            private set;
        }
        SHA1 sha1;
        //public ServerInfo GetServerInfo(string url)
        //{
        //   return ServerInfo.LoadXMLStr(wc.DownloadString(url));

        //}
        public void DownLoad(string localpath,string firsturl)
        {
            System.Threading.WaitCallback call=(s)=>
            {
                fileinfo verinfo = null;
                //string firsturl ="";
                //foreach(var url in verhfs)
                {
                    //firsturl=url;
                    try
                    {
                        var verstr=wc.DownloadString(firsturl+"ver.txt");
                        verinfo = fileinfo.Read(verstr);
                        totalsize = verinfo.flen;
                        //break;
                    }
                    catch
                    {
                        firsturl = "";
                    }
                }
                if (string.IsNullOrEmpty(firsturl))
                {//下载错误
                    if (onUpdateError != null)
                        onUpdateError("ver.txt 下载错误");
                    return;
                }

                //check filelist
                try
                {//如果本地下载
                    string str = System.IO.File.ReadAllText(System.IO.Path.Combine(localpath, "finishver.txt"));
                    var localinfo=     fileinfo.Read(str);
                    if (verinfo.Equals(localinfo))
                    {//和本地情况符合，直接跳过更新
                        if (onUpdateDone != null)
                            onUpdateDone();
                        return;
                    }
                }
                catch
                {
                }
                //down filelist
                List<fileinfo> files = new List<fileinfo>();
                try
                {
                    var filelistdata = wc.DownloadData(firsturl + verinfo.filename);
                    var hash=sha1.ComputeHash(filelistdata);
                    if (verinfo.TestHash(hash) == false)
                    {
                        if (onUpdateError != null)
                            onUpdateError(verinfo.filename + " 匹配错误");
                        return;
                    }
                    string outfilename=System.IO.Path.Combine(localpath, verinfo.filename);
                    if (System.IO.Directory.Exists(localpath) == false) System.IO.Directory.CreateDirectory(localpath);
                    using (System.IO.Stream fs = System.IO.File.Create(outfilename))
                    {
                        fs.Write(filelistdata, 0, filelistdata.Length);
                    }
                   var lines = System.IO.File.ReadAllLines(outfilename);
                   foreach (var line in lines)
                   {
                       files.Add(fileinfo.Read(line));
                   }
                   filecount = files.Count;
                }
                catch
                {
                    if (onUpdateError != null)
                        onUpdateError(verinfo.filename + " 下载错误");
                    return;
                }

                //开始比对文件
                finishfilecount = 0;
                finishfilesize = 0;

                {
                    if (onUpdatePrepare != null)
                        onUpdatePrepare();
                }

                List<fileinfo> rdown = new List<fileinfo>();
                foreach (var f in files)
                {
                    string fname = System.IO.Path.Combine(localpath, f.filename);
                    string dict = System.IO.Path.GetDirectoryName(fname);
                    if (System.IO.Directory.Exists(dict) == false) System.IO.Directory.CreateDirectory(dict);

                    if (System.IO.File.Exists(fname))
                    {
                        byte[] hash = null;
                        using (System.IO.Stream fs = System.IO.File.OpenRead(fname))
                        {
                           hash =sha1.ComputeHash(fs);
                        }
                        if (f.TestHash(hash) == false)
                        {
                            rdown.Add(f);
                            System.IO.File.Delete(fname);
                        }
                    }
                    else
                    {
                        rdown.Add(f);
                    }
                }
                int _size = 0;
                foreach (var f in rdown)
                {
                    _size += f.flen;
                }
                totalsize = _size;
                filecount = rdown.Count;
                if (onUpdateState != null)
                    onUpdateState();

                //准备下载
                List<fileinfo> errors = new List<fileinfo>();
                foreach (var f in rdown)
                {
                    try
                    {
                        string fname = System.IO.Path.Combine(localpath, f.filename);

                        string path = System.IO.Path.GetDirectoryName(f.filename);
                        string file = System.IO.Path.GetFileName(f.filename);
                        file = Uri.EscapeDataString(file);
                        file = System.IO.Path.Combine(path, file);
                        var uri = firsturl + file.Replace('\\', '/');

                        var bs = wc.DownloadData(uri);
                        var hash = sha1.ComputeHash(bs);

                        using (System.IO.Stream fs = System.IO.File.Create(fname))
                        {
                            fs.Write(bs, 0, bs.Length);
                        }
                        if (f.TestHash(hash))
                        {
                            finishfilecount = finishfilecount + 1;
                            finishfilesize = finishfilesize + f.flen;
                            if (onUpdateState != null)
                                onUpdateState();
                        }
                        else
                        {
                            errors.Add(f);
                        }
                    }
                    catch
                    {
                        errors.Add(f);
                    }
                }

                if (errors.Count == 0)
                {
                    string path = System.IO.Path.Combine(localpath, "finishver.txt");
                    System.IO.File.WriteAllText(path, verinfo.ToString());
                    if (onUpdateDone != null)
                        onUpdateDone();

                }
                else
                {
                    if (onUpdateError != null)
                        onUpdateError("更新未完成");
                }
            };
            System.Threading.ThreadPool.QueueUserWorkItem(call);
        }
        public int filecount
        {
            get;
            private set;
        }
        public int totalsize
        {
            get;
            private set;
        }
        public int finishfilecount
        {
            get;
            set;
        }
        public int finishfilesize
        {
            get;
            set;
        }
        public delegate void deleUpdateError(string info);
        public event Action  onUpdatePrepare;
        public event Action  onUpdateState;
        public event Action  onUpdateDone;
        public event deleUpdateError  onUpdateError;
    }
}

