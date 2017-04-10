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
        public void SetAlarm(int AlarmID)
        {
            Alarm firedAlarm = null;
            foreach (Alarm alarm in Alarm.Alarms)
            {
                if (alarm.ID == AlarmID)
                {
                    firedAlarm = alarm;
                }
            }
            SetAlarm(firedAlarm);
        }

        public void SetAlarm(Alarm alarm)
        {
            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("AlarmID", alarm.ID);
            alarmIntent.SetPackage("com.lo2ay.AlarmPlus");
            alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);
            
            
            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            SetAlarm(alarm, alarmManager, alarmIntent);
        }

        private void SetAlarm(Alarm alarm, AlarmManager alarmManager, Intent alarmIntent)
        {
            Calendar calendar = (Calendar)Calendar.Instance.Clone();
            calendar.Set(CalendarField.Second, 0);
            var Now = DateTime.Now;
            int baseID = GetFirstID(alarm);
            if (!alarm.IsRepeated)
            {
                for (int i = 0; i < alarm.AllTimes.Count; i++)
                {
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, baseID + i, alarmIntent, PendingIntentFlags.UpdateCurrent);
                    var alarmTime = alarm.AllTimes[i];
                    long millisecondsToAlarm = (alarmTime.Hour - Now.Hour) * (60 * 60 * 1000);
                    millisecondsToAlarm = millisecondsToAlarm + (alarmTime.Minute - Now.Minute) * (60 * 1000);
                    if (millisecondsToAlarm <= 0)
                    {
                        millisecondsToAlarm += (24 * 60 * 60 * 1000);
                    }
                    alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis + millisecondsToAlarm, pendingIntent);
                }
            }
        }

        private int GetFirstID(Alarm alarm)
        {
            int i = 10;
            while (i < alarm.AllTimes.Count) i *= 10;
            return (alarm.ID * i);
        }
    }
}