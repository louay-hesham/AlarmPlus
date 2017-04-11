using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using AlarmPlus.Droid;
using AlarmPlus.Core;
using Java.IO;

[assembly: Dependency(typeof(RingtoneManager))]
namespace AlarmPlus.Droid
{
    class RingtoneManager : IRingtoneManager
    {
        private static File ExternalStorage = new File(Environment.ExternalStorageDirectory, "AlarmPlus" + File.Separator + "Ringtones");

        public RingtoneManager()
        {
            ExternalStorage.Mkdirs();
        }

        public string GetRingtone()
        {
            return ExternalStorage.AbsolutePath + File.Separator + "sample.mp3";
        }
    }
}