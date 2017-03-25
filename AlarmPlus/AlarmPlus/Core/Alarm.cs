using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public class Alarm
    {
        private static int _NewAlarmCount = 0;
        public static List<Alarm> Alarms = new List<Alarm>();

        public readonly TimeSpan Time;
        public readonly string AlarmName;
        public readonly bool IsRepeated;
        public readonly bool[] SelectedDays;
        public readonly bool IsNagging;
        public readonly int AlarmsBefore, AlarmsAfter, Interval;

        public Alarm(TimeSpan Time, string AlarmName, bool IsRepeated, bool[] SelectedDays, bool IsNagging, int[] NaggingSettings)
        {
            this.Time = Time;
            if (AlarmName == null || AlarmName.Equals(string.Empty))
            {
                if (_NewAlarmCount == 0) this.AlarmName = "New alarm";
                else this.AlarmName = "New alarm " + _NewAlarmCount;
                _NewAlarmCount++;
            }
            else this.AlarmName = AlarmName;
            this.IsRepeated = IsRepeated;
            this.SelectedDays = SelectedDays;
            this.IsNagging = IsNagging;
            this.AlarmsBefore = NaggingSettings[0];
            this.AlarmsAfter = NaggingSettings[1];
            this.Interval = NaggingSettings[2];
        }

    }
}
