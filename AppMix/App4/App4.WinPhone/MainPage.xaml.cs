using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace App4.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();

            //Add StartParam
            Dictionary<string, object> startParam = new Dictionary<string, object>();

            //Android.Graphics.Point size = new Android.Graphics.Point();
            //this.WindowManager.DefaultDisplay.GetSize(size);
            //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            
            //IsolatedStorageFile file = "C:\\Data\\Users\\DefaultAppAccount\\AppData\\{2E66C0E1-F4F9-4A54-835E-5E3038237D7A}\\Local\\IsolatedStore";
            startParam["width"] = 480;// size.X;
            startParam["height"] = 800;// size.Y;
            startParam["savepath"] = "/";// path;
            LoadApplication(new AppMix.App(startParam));

            //LoadApplication(new App4.App());
        }
    }
}
