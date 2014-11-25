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
        //脚本Logger对象
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
        public static Page GetMainPage()
        {
            //ContentPage       单页面，页面中有一个View
            //MasterDetailPage  两个子页面切换，在Wp上外观奇怪，在android上是往右边划开的效果

            //CarouselPage      多个子页面，横向滚动的多页面 WP枢纽视图有bug，子页面高度没自动算对
            //TabbedPage        多个子页面，Tab多页面
            //NavigationPage    导航页面，多个子页面，一次呈现一个子页面.用导航器可以向前向后

            //{
            //    ContentPage cccp = new ContentPage();
            //    Button btn = new Button();
            //    cccp.Content = btn;
            //    btn.Clicked += (s, e) =>
            //        {
            //            ContentPage page02 = new ContentPage();
            //            page02.Title = "page02";
            //            cccp.Navigation.PushAsync(page02);
            //        };
            //    cccp.Title = "cccp";
            //    NavigationPage page = new NavigationPage(cccp);
            //    cccp.SetValue(NavigationPage.HasNavigationBarProperty, true);
            //    page.SetValue(NavigationPage.HasNavigationBarProperty, true);
            //    page.Title = "page";

            //    return page;
            //}
            //{
            //    MasterDetailPage mdp = new MasterDetailPage();//Master Detail 两个页面切换，在Wp上表现不好
            //    ContentPage master = new ContentPage();
            //    master.Title = "master";
            //    Button mb = new Button();
            //    mb.Text = "btnMaster";
            //    master.Content = mb; ;

            //    mdp.Master = master;
            //    ContentPage detail = new ContentPage();
            //    detail.Title = "detail";
            //    detail.Content = new Button();
            //    mdp.Detail = detail;

            //    return mdp;
            //}
            CarouselPage ccp = new CarouselPage();//滚动页面，有bug，子页面高度没自动算对
            //TabbedPage ccp = new TabbedPage();//Tab页面

            ccp.Title = "hi there.";

            for (int i = 0; i < 5; i++)
            {
                ContentPage cp = new ContentPage();
                cp.HeightRequest = cp.HeightRequest - 200;
                ccp.Children.Add(cp);
                cp.Title = "I'm " + i;
                Button btn01 = new Button();
                btn01.Text = "calc 1+2";
                CSLE.CLS_Environment env = new CSLE.CLS_Environment(new Logger());
                btn01.Clicked += (s, e) =>
                    {
                        DateTime t0 = DateTime.Now;
                        var token = env.ParserToken("1+2");
                        var expr = env.Expr_CompilerToken(token, true);

                        var v = env.Expr_Execute(expr);
                        DateTime t1 = DateTime.Now;
                        Debug.WriteLine("v=" + v.value + "," + (t1 - t0).TotalSeconds);
                    };
                //btn01.VerticalOptions = LayoutOptions.Center;
                cp.Content = btn01;

            }
            return ccp;

        }
    }
}
