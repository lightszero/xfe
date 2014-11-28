
using System;
using Xamarin.Forms;
class page0
{
    static ContentPage _page;
    static public void FillPage(ContentPage page)
    {
        _page = page;
        Button btn = new Button();
        btn.BackgroundColor = Color.Red;
        btn.Text = "这是由脚本配置的主页";

        EventHandler handler =  onClick;
        btn.Clicked += handler;
        page.Content = btn;
    }
    static void onClick(object sender,EventArgs args)
    {
        _page.Navigation.PushAsync(page1.CreatePage());
    }

}