using System;

using Android.App;
using Android.Content;

using AlarmPlus.Core;
using Xamarin.Forms;
using AlarmPlus.Droid;
using Android.Icu.Util;
using System.Diagnostics;
using Newtonsoft.Json;

[assembly: Dependency(typeof(AlarmSetter))]
namespace AlarmPlus.Droid
{
    class AlarmSetter : IAlarmSetter
    {
        public void SetAlarm(Alarm alarm)
        {
            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("AlarmID", alarm.ID);
            
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            if (!alarm.IsRepeated && !alarm.IsNagging)
            {
                SetOneTimeNonNagging(alarm, alarmManager, pendingIntent);
            }
        }

        private void SetOneTimeNonNagging(Alarm alarm, AlarmManager alarmManager, PendingIntent pendingIntent)
        {
            Calendar calendar = (Calendar)Calendar.Instance.Clone();
            long millisecondsToAlarm = (alarm.Time.Hours - DateTime.Now.Hour) * (60 * 60 * 1000);
            millisecondsToAlarm = millisecondsToAlarm + (alarm.Time.Minutes - DateTime.Now.Minute) * (60 * 1000);
            if (millisecondsToAlarm <= 0)
            {
                millisecondsToAlarm += (24 * 60 * 60 * 1000);
            }
            calendar.Set(CalendarField.Second, 0);

            alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis + millisecondsToAlarm, pendingIntent);
        }
    }
}