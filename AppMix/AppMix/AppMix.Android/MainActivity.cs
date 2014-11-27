using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;

namespace AppMix.Droid
{
    [Activity(Label = "AppMix", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //不可旋转
            this.RequestedOrientation = ScreenOrientation.Portrait;
            
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            //Add StartParam
            Dictionary<string, object> startParam = new Dictionary<string, object>();

            Android.Graphics.Point size = new Android.Graphics.Point();
            this.WindowManager.DefaultDisplay.GetSize(size);
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            startParam["width"] = size.X;
            startParam["height"] = size.Y;
            startParam["savepath"] = path;

            SetPage(App.GetMainPage(startParam));
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

            //Add this to exit program. clean cache.
            Java.Lang.Runtime.GetRuntime().Exit(0);
        }

    }
}

