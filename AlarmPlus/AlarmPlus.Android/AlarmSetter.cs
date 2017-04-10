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

        public void CancelAlarm(int AlarmID)
        {
            Alarm firedAlarm = null;
            foreach (Alarm alarm in Alarm.Alarms)
            {
                if (alarm.ID == AlarmID)
                {
                    firedAlarm = alarm;
                }
            }
            CancelAlarm(firedAlarm);
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

        public void CancelAlarm(Alarm alarm)
        {
            Intent alarmIntent = new Intent(Android.App.Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("AlarmID", alarm.ID);
            alarmIntent.SetPackage("com.lo2ay.AlarmPlus");
            alarmIntent.SetFlags(ActivityFlags.IncludeStoppedPackages);


            AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);
            CancelAlarm(alarm, alarmManager, alarmIntent);
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
                    double millisecondsToAlarm = (alarmTime - DateTime.Now).TotalMilliseconds;
                    alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis + (long)millisecondsToAlarm, pendingIntent);
                }
            }
            else
            {
                for (int i = 0; i < alarm.AllTimes.Count; i++)
                {
                    PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, baseID + i, alarmIntent, PendingIntentFlags.UpdateCurrent);
                    var alarmTime = alarm.AllTimes[i];
                    double millisecondsToAlarm = (alarmTime - DateTime.Now).TotalMilliseconds;
                    long millisecondsInWeek = 7 * 24 * 60 * 60 * 1000;
                    alarmManager.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis + (long)millisecondsToAlarm, millisecondsInWeek,pendingIntent);
                }
            }
        }

        private void CancelAlarm(Alarm alarm, AlarmManager alarmManager, Intent alarmIntent)
        {
            int baseID = GetFirstID(alarm);
            for (int i = 0; i < alarm.AllTimes.Count; i++)
            {
                PendingIntent pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, baseID + i, alarmIntent, PendingIntentFlags.UpdateCurrent);
                pendingIntent.Cancel();
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