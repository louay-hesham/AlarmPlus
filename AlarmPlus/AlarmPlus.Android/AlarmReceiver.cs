using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AlarmPlus.GUI.Pages;
using AlarmPlus.Core;
using Newtonsoft.Json;

namespace AlarmPlus.Droid
{
    [BroadcastReceiver]
    class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Toast.MakeText(context, "I'm running", ToastLength.Long).Show();
            Alarm alarm = JsonConvert.DeserializeObject <Alarm> (intent.GetStringExtra("Alarm"));
            App.NavPage.Navigation.PushAsync(new FiredAlarm(alarm), true);
        }
    }
}