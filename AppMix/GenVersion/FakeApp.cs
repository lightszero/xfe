using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMix
{
    class App
    {
        public static void GetTxtLocal(string filename, Action<string, Exception> ongot)
        {
        }
        public static void GetTxtRemote(string url, Action<string, Exception> ongot)
        {
        }
        public static ImageSource GetImgLocal(string filename)
        {
            return null;
        }
        public static ImageSource GetImgRemote(string url)
        {
            return null;
        }
    }
}
