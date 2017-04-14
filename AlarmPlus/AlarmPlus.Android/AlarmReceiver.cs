
using Android.Content;
using Android.Widget;
using AlarmPlus.Core;
using Newtonsoft.Json;
using Android.Media;
using static Android.Provider.MediaStore.Audio;
using AlarmPlus.GUI.Pages;

namespace AlarmPlus.Droid
{
    [BroadcastReceiver]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "Alarm Fired!!", ToastLength.Long).Show();
            
            var x = (string)intent.Extras.Get("AlarmID");
            int alarmID  = int.Parse(x);

            AlarmFired.AlarmID = alarmID;

            AudioManager audio = (AudioManager)context.GetSystemService(Context.AudioService);
            audio.SetStreamVolume(Stream.Music, audio.GetStreamMaxVolume(Stream.Music), VolumeNotificationFlags.Vibrate);

            Intent applicationIntent = new Intent(context, typeof(AlarmActivity));
            applicationIntent.AddFlags(ActivityFlags.NewTask);
            applicationIntent.SetFlags(ActivityFlags.ReceiverForeground);
            context.StartActivity(applicationIntent);
        }
    }
}