
using Xamarin.Forms;
class page0
{
    ContentPage mypage = null;
    public Page CreatePage()
    {
        mypage = new ContentPage();
        Button btn = new Button();
        btn.BackgroundColor = Color.Red;
        mypage.Content = btn;
        return mypage;
    }
}