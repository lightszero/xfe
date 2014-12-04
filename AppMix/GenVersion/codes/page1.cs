using AppMix;
using Xamarin.Forms;
class page1
{
    public static Page CreatePage()
    {
        ContentPage page = new ContentPage();
        FillPage(page);

        return page;
    }
    static public void FillPage(ContentPage page)
    {
        ScrollView sview = new ScrollView();//以滚动视图为基础
        page.Content = sview;

        StackLayout l = new StackLayout();//以栈面板为下一层
        sview.Content = l;

        for (int i = 0; i < 10; i++)
        {
            Button btn = new Button();
            btn.Text = "stackTest.";
            l.Children.Add(btn);

            Image img = new Image();
            img.HeightRequest = App.height/5;
            l.Children.Add(img);
            img.Source = App.GetImgLocal("imgs/1.jpg");
        }
    }
}