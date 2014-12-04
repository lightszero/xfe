using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppMix
{

    public class App:Application
    {
        public App(Dictionary<string, object> _startparam)
        {
            this.MainPage = GetMainPage(_startparam);
        }
        public class Logger : CSLE.ICLS_Logger
        {

            public void Log(string str)
            {
                Debug.WriteLine(str);
            }

            public void Log_Warn(string str)
            {
                Debug.WriteLine("<W>" + str);
            }

            public void Log_Error(string str)
            {
                Debug.WriteLine("<E>" + str);
            }
        }
        public static CSLE.CLS_Environment envScript = null;
        public static Dictionary<string, object> startparams;
        public static update.updatemgr updatemgr = new update.updatemgr();
        static string savepath;
        public static int width;
        public static int height;
        public static Page GetMainPage(Dictionary<string, object> _startparam)
        {
            startparams = _startparam;
            savepath = startparams["savepath"] as string;
            width = (int)App.startparams["width"];
            height = (int)App.startparams["height"];
            FirstPage fp = new FirstPage();
            fp.InitPage();
            NavigationPage npage = new NavigationPage(fp.page);

            return npage;
        }

        public static void GetTxtLocal(string filename, Action<string, Exception> ongot)
        {
            string str = null;
            try
            {
                str = System.IO.File.ReadAllText(System.IO.Path.Combine(savepath, filename));

            }
            catch (Exception err)
            {
                ongot(null, err);
            }
            ongot(str, null);
        }
        public static void GetTxtRemote(string url, Action<string, Exception> ongot)
        {

            System.Threading.WaitCallback call = (obj) =>
          {
              string str = null;
              try
              {

                  update.MyWebClient wc = new update.MyWebClient();
                  str = wc.DownloadString(url);

              }
              catch (Exception err)
              {
                  Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                          {
                              ongot(null, err);
                          });
              }
              Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
              {
                  ongot(str, null);
              });

          };
            System.Threading.ThreadPool.QueueUserWorkItem(call, null);
        }
        public static ImageSource GetImgLocal(string filename)
        {
            return ImageSource.FromFile(System.IO.Path.Combine(savepath, filename));
        }
        public static ImageSource GetImgRemote(string url)
        {
            return ImageSource.FromUri(new Uri(url));
        }

        public static void InitScript(string indexfile, Action<Exception> ongot)
        {
            try
            {
                string str = System.IO.File.ReadAllText(System.IO.Path.Combine(savepath, indexfile));
                string[] lines = str.Split('\n');
                Dictionary<string, IList<CSLE.Token>> tokens = new Dictionary<string, IList<CSLE.Token>>();
                foreach (var l in lines)
                {
                    if (string.IsNullOrEmpty(l)) continue;
                    string file = System.IO.Path.Combine(savepath, l.Replace('\\', '/'));

                    using (System.IO.Stream s = System.IO.File.OpenRead(file))
                    {
                        tokens[file] = envScript.tokenParser.ReadTokenList(s);
                    }


                }
                envScript.Project_Compiler(tokens, true);

            }
            catch (Exception err)
            {
                ongot(err);
            }
            ongot(null);
        }

        public static void Download(string url, Action prepare, Action state, Action done, Action<string> error)
        {
            string path = startparams["savepath"] as string;
            updatemgr = new update.updatemgr();
            updatemgr.onUpdatePrepare += () =>
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                        {
                            prepare();
                        });
                };
            updatemgr.onUpdateState += () =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    state();
                });
            };
            updatemgr.onUpdateDone += () =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    done();
                });
            };
            updatemgr.onUpdateError += (txt) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    error(txt);
                });
            };
            updatemgr.DownLoad(path, url);
        }
        static System.Net.WebRequest req;
        public static Page GetMainPage2(Dictionary<string, object> startparam)
        {
            startparams = startparam;
            ContentPage page = new ContentPage();
            StackLayout sl = new StackLayout();
            page.Content = sl;

            string path = startparam["savepath"] as string;
            int height = (int)startparam["height"];
            //System.Net.WebClient wc = new System.Net.WebClient();
            //bitmap = wc.DownloadData("http://images.takungpao.com/2013/0327/20130327123103850.jpg");
            //var imgsrc = ImageSource.FromStream(() =>
            //          {
            //              return new System.IO.MemoryStream(bitmap);
            //          });

            {
                var imgsrc = UriImageSource.FromUri(new Uri("http://images.takungpao.com/2013/0327/20130327123103850.jpg"));
                sl.Children.Add(new Button());
                sl.Children.Add(new Label() { Text = "uri remote" });
                Image i1 = new Image() { Source = imgsrc };
                i1.HeightRequest = 50;
                sl.Children.Add(i1);
            }


            string file = System.IO.Path.Combine(path, "cool.jpg");
            if (System.IO.File.Exists(file))
            {
                try
                {

                    sl.Children.Add(new Label() { Text = "loadlocal:" + file });
                    var img2 = ImageSource.FromFile(file);
                    Image i2 = new Image() { Source = img2 };
                    i2.HeightRequest = height / 4;
                    sl.Children.Add(i2);
                }
                catch (Exception err)
                {
                    Debug.WriteLine("load fail.");
                }
            }
            else
            {
                sl.Children.Add(new Label() { Text = "download-1" });
                try
                {
                    req = System.Net.WebRequest.CreateHttp("http://images.takungpao.com/2013/0327/20130327123103850.jpg");
                    req.BeginGetResponse(
                        (ar) =>
                        {
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                          {
                              sl.Children.Add(new Label() { Text = "download0" });
                          }
                          );
                            var response = req.EndGetResponse(ar);
                            byte[] bs = new byte[response.ContentLength];
                            response.GetResponseStream().Read(bs, 0, bs.Length);



                            //. FromUri(new Uri("file://"+file));
                            //(() =>
                            //{
                            //    return new System.IO.MemoryStream(bs);
                            //});
                            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                            {
                                try
                                {
                                    sl.Children.Add(new Label() { Text = "download1" });
                                    System.IO.File.WriteAllBytes(file, bs);
                                    var img2 = ImageSource.FromFile(file);

                                    sl.Children.Add(new Label() { Text = "download2" });

                                    Image i2 = new Image() { Source = img2 };
                                    i2.HeightRequest = height / 4;
                                    sl.Children.Add(i2);
                                }
                                catch
                                {

                                }
                            }
                            );
                        }, null
                    );
                }
                catch
                {
                    Debug.WriteLine("Down fail.");
                }
            }
            return page;
        }
    }
}
