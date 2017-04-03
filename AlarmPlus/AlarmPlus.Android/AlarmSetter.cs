using System;

using Android.App;
using Android.Content;

using AlarmPlus.Core;
using Xamarin.Forms;
using AlarmPlus.Droid;
using Android.Icu.Util;

[assembly: Dependency(typeof(AlarmSetter))]
namespace AlarmPlus.Droid
{
    class AlarmSetter : IAlarmSetter
    {
        public void SetAlarm(Alarm alarm)
        {
            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", "This is my test message!");
            alarmIntent.PutExtra("title", "This is my test title!");

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);

            DateTime alarmTime = DateTime.Now.Date.Add(alarm.Time);
            if (alarmTime.Hour < DateTime.Now.Hour || alarmTime.Minute <= DateTime.Now.Minute)
            {
                alarmTime.AddDays(1);
            }
            Calendar calendar = Calendar.Instance;
            calendar.Set(alarmTime.Year + 1900, alarmTime.Month, alarmTime.Day, alarmTime.Hour, alarmTime.Minute, 0);

            System.Diagnostics.Debug.WriteLine("askjkadjsads" + calendar.TimeInMillis + " adkgdhsaj");

            alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);


        }
    }
}