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
using Plugin.FilePicker.Abstractions;
using System.Threading.Tasks;

[assembly: Dependency(typeof(RingtoneManager))]
namespace AlarmPlus.Droid
{
    class RingtoneManager : IRingtoneManager
    {
        private static File RingtonesFolder = new File(Environment.ExternalStorageDirectory, "AlarmPlus" + File.Separator + "Ringtones");

        public RingtoneManager()
        {
            RingtonesFolder.Mkdirs();
        }

        public string GetRingtone()
        {
            return RingtonesFolder.AbsolutePath + File.Separator + App.AppSettings.RingtoneName;
        }

        public async Task SetRingtone(FileData filedata)
        {
            var ringtones = RingtonesFolder.List();
            bool ringtoneExists = false;
            foreach (string ringtone in ringtones)
            {
                if (ringtone.Equals(filedata.FileName))
                {
                    ringtoneExists = true;
                    break;
                }
            }
            
            if (!ringtoneExists)
            {
                string fileName;
                if (filedata.FileName.EndsWith(".mp3")) fileName = filedata.FileName;
                else fileName = filedata.FileName + ".mp3";
                App.AppSettings.RingtoneName = fileName;
                var newRingtone = new File(RingtonesFolder, fileName);
                FileOutputStream fos = new FileOutputStream(newRingtone);
                await fos.WriteAsync(filedata.DataArray);
                fos.Close();
            }
            else App.AppSettings.RingtoneName = filedata.FileName;
        }
    }
}