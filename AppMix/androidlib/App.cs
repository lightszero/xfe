using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppMix
{
    public class App
    {
        static System.Net.WebRequest req;
        static byte[] bitmap;
        public static Page GetMainPage(Dictionary<string,object> startparam)
        {
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
