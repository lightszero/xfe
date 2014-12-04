
using AppMix;
using System;
using Xamarin.Forms;
class page0
{
    static ContentPage _page;
    static public void FillPage(ContentPage page)
    {


        _page = page;
        _page.Title = "MainPage";
        ScrollView view = new ScrollView();//页面的基础是一个滚动栏
        page.Content = view;
        Grid g = new Grid();//滚动栏中的Grid布局
        view.Content = g;
        //定义四列
        for (int i = 0; i < 4; i++)
        {
            ColumnDefinition c = new ColumnDefinition();
            g.ColumnDefinitions.Add(c);
        }
        //12行
        for (int i = 0; i < 12; i++)
        {
            RowDefinition r = new RowDefinition();
            r.Height = new GridLength(App.width / 4, GridUnitType.Absolute);
            g.RowDefinitions.Add(r);
        }

        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "充值";
            Grid.SetColumn(btn01, 0);//第零列
            Grid.SetColumnSpan(btn01, 1);//跨一列
            Grid.SetRow(btn01, 0);//第零行
            Grid.SetRowSpan(btn01, 1);//跨一行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "收藏";
            Grid.SetColumn(btn01, 1);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 0);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "历史记录";
            Grid.SetColumn(btn01, 2);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨一列
            Grid.SetRow(btn01, 0);//第零行
            Grid.SetRowSpan(btn01, 1);//跨一行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "设置";
            Grid.SetColumn(btn01, 3);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 0);//第零行
            Grid.SetRowSpan(btn01, 1);//跨一行

            btn01.Clicked += onClick01;//绑定事件
        }

        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "消息";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 1);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "交易密码";
            Grid.SetColumn(btn01, 1);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 1);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "账户信息";
            Grid.SetColumn(btn01, 2);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 1);//第x行
            Grid.SetRowSpan(btn01, 2);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "推荐任务";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 2);//第x行
            Grid.SetRowSpan(btn01, 2);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "等级经验值";
            Grid.SetColumn(btn01, 2);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 3);//第x行
            Grid.SetRowSpan(btn01,1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "所有任务";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 4);//跨x列
            Grid.SetRow(btn01, 4);//第x行
            Grid.SetRowSpan(btn01, 2);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品1";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 6);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品2";
            Grid.SetColumn(btn01, 2);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 6);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品3";
            Grid.SetColumn(btn01, 3);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 6);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品4";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 7);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品5";
            Grid.SetColumn(btn01, 1);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 7);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品6";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 8);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商品7";
            Grid.SetColumn(btn01, 1);//第x列
            Grid.SetColumnSpan(btn01, 1);//跨x列
            Grid.SetRow(btn01, 8);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "商城";
            Grid.SetColumn(btn01, 2);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 7);//第x行
            Grid.SetRowSpan(btn01, 2);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "全部活动";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 4);//跨x列
            Grid.SetRow(btn01, 9);//第x行
            Grid.SetRowSpan(btn01, 1);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "热门活动1";
            Grid.SetColumn(btn01, 0);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 10);//第x行
            Grid.SetRowSpan(btn01, 2);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        {
            Button btn01 = new Button();//增加按钮
            g.Children.Add(btn01);
            btn01.BackgroundColor = Color.Red;
            btn01.Text = "热门活动2";
            Grid.SetColumn(btn01, 2);//第x列
            Grid.SetColumnSpan(btn01, 2);//跨x列
            Grid.SetRow(btn01, 10);//第x行
            Grid.SetRowSpan(btn01, 2);//跨x行

            btn01.Clicked += onClick01;//绑定事件
        }
        //page.Content = btn;
    }
    static void onClick01(object sender, EventArgs args)
    {
        _page.Navigation.PushAsync(page1.CreatePage());
    }

}