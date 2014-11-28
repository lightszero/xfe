using AppMix;
using Xamarin.Forms;
class page1
{
    public static Page CreatePage()
    {
        ContentPage page = new ContentPage();
        StackLayout l = new StackLayout();
        page.Content = l;
        l.Children.Add(new Button() );

        Image img = new Image();
        img.HeightRequest = 50;
        l.Children.Add(img );
        img.Source = App.GetImgLocal("imgs/1.jpg");

        return page;
    }

}