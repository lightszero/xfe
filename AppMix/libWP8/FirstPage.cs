using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppMix
{
    interface IPage
    {
        Page page
        {
            get;
        }
        void InitPage();
    }
    class FirstPage
    {
        public Page page
        {
            get;
            private set;
        }
        Editor editUrl;
        public void InitPage()
        {
            ContentPage _page = new ContentPage();//页面
            this.page = _page;//设定当前Page

            _page.Title = "DownPage.";

            ScrollView sview = new ScrollView();//滚动视图
            _page.Content = sview;//直接放到页面


            StackLayout layout = new StackLayout();//堆叠布局，一个一个往下摆
            sview.Content = layout;//放到滚动视图

            Label label1 = new Label();
            label1.Text = "在下面输入更新服务器地址";
            layout.Children.Add(label1);

            Editor edit = new Editor();//文本框
            edit.BackgroundColor = Color.Gray;
            edit.Text = "http://lightszero.github.io/tui/";
            layout.Children.Add(edit);

            editUrl = edit;//记录文本框

            Button btn = new Button();
            btn.Text = "检查更新并启动";

            btn.Clicked += ClickBegin;//给按钮添加事件

            layout.Children.Add(btn);

            Button btn2 = new Button();
            btn2.Text = "Debug http://10.10.10.200/";

            btn2.Clicked += ClickBegin2;//给按钮添加事件
            layout.Children.Add(btn2);
        }
        void ClickBegin(object sender, EventArgs args)
        {
            string srcurl = editUrl.Text;
            editUrl = null;
            string localpath = App.startparams["savepath"] as string;

            ContentPage _page = page as ContentPage;
            ScrollView view = _page.Content as ScrollView;
            StackLayout layout = new StackLayout();//更换布局
            view.Content = layout;

            Label label1 = new Label();
            label1.Text = "开始下载";

            layout.Children.Add(label1);

            App.Download(
                srcurl
                , () =>
                {
                    Label label = new Label();
                    label.Text = "Prepare";

                    layout.Children.Add(label);
                }
                , () =>
                {
                    Label label = new Label();
                    label.Text = "state";

                    layout.Children.Add(label);
                }
                , () =>
                {
                    Label label = new Label();
                    label.Text = "done.";

                    layout.Children.Add(label);
                    InitFirstPage();
                }
                , (txt) =>
                {
                    Label label = new Label();
                    label.Text = "Error" + txt;

                    layout.Children.Add(label);
                }
                );

        }
        void ClickBegin2(object sender, EventArgs args)
        {
            editUrl.Text = "http://10.10.10.200/";
            ClickBegin(sender, args);
        }
        void InitFirstPage()
        {
            int height = (int)App.startparams["height"];


            ContentPage _page = page as ContentPage;
            _page.Title = "初始化";
            ScrollView view = _page.Content as ScrollView;
            StackLayout layout = new StackLayout();//更换布局
            view.Content = layout;

            Image img = new Image();//test load image.
            img.Source = App.GetImgLocal("imgs/1.jpg");
            img.HeightRequest = height / 10;
            layout.Children.Add(img);

            App.GetTxtLocal("txts/regtype.txt", (str, err) =>
            {
                if (err != null)
                {
                    Label label = new Label();
                    label.Text = "读取脚本配置出错。";
                    layout.Children.Add(label);
                    return;
                }
                App.envScript = new CSLE.CLS_Environment(new App.Logger());

                //App.envScript.RegType(new CSLE.RegHelper_DeleAction<object, EventArgs>(typeof(EventHandler), "EventHandler"));


                string[] lines = str.Split('\n');
                foreach (var l in lines)
                {
                    if (string.IsNullOrEmpty(l)) continue;
                    string[] ss = l.Split(new string[] { "=>", ":" }, StringSplitOptions.None);
                    if (ss.Length < 2) continue;
                    if (ss.Length == 2)
                    {
                        Type t = Type.GetType(ss[0]);
                        if (t != null)
                        {
                            App.envScript.RegType(new CSLE.RegHelper_Type(t, ss[1]));
                        }
                        else
                        {
                            App.envScript.logger.Log_Error("RegType Error：" + ss[1] + "  from" + ss[0]);
                        }
                    }
                    else
                    {
                        try
                        {

                            Type tReg = Type.GetType(ss[1]);
                            Type tDele = Type.GetType(ss[2]);
                            var con = tReg.GetConstructor(new Type[] { typeof(Type), typeof(string) });
                            var type = con.Invoke(new object[] { tDele, ss[3] }) as CSLE.ICLS_Type_Dele;
                            App.envScript.RegType(type);

                            //logger.Log_Warn("注册Dele:" + s[3] + "  from" + s[2] + "||" + s[1]);
                        }
                        catch
                        {
                            App.envScript.logger.Log_Error("Error注册Dele:" + ss[3] + "  from" + ss[2] + "||" + ss[1]);

                        }
                    }
                }
                Label labelr = new Label();
                labelr.Text = "注册类型：" + lines.Length;
                layout.Children.Add(labelr);

                App.InitScript("codes/codes.txt", (err2) =>
                    {


                        Label label = new Label();
                        label.Text = "读取脚本" + (err2 == null ? "成功" : ("出错" + err2.ToString()));
                        layout.Children.Add(label);
                        if (err2 == null)
                        {

                            NavigationPage.SetHasNavigationBar(_page, false);

                            //Button hi = new Button();
                            //_page.Content = hi;
                            //hi.BackgroundColor = Color.Red;
                            //hi.Clicked += (s, e) =>
                            //    {
                            //        ContentPage next = new ContentPage();
                            //        StackLayout l = new StackLayout();
                            //        next.Content = l;
                            //        l.Children.Add(new Button());

                            //        //Image img2 = new Image();
                            //        //img2.HeightRequest = 50;
                            //        //l.Children.Add(img);
                            //        //img2.Source = App.GetImgLocal("imgs/1.jpg");
                            //        _page.Navigation.PushAsync(next);
                            //    };
                            var type = App.envScript.GetTypeByKeyword("page0");
                            List<CSLE.CLS_Content.Value> values = new List<CSLE.CLS_Content.Value>();
                            CSLE.CLS_Content.Value v = new CSLE.CLS_Content.Value();
                            v.type = typeof(ContentPage);
                            v.value = page;
                            values.Add(v);
                            type.function.StaticCall(App.envScript.CreateContent(), "FillPage", values);
                        }
                        return;

                    });

            });



        }
    }
}
