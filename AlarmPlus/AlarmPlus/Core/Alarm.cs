using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public class Alarm
    {
        private static int _NewAlarmCount = 0;
        public static ObservableCollection<Alarm> Alarms = new ObservableCollection<Alarm>();
        private static int IdCount = 0;

        private readonly string[] Days = { "Sat", "Sun", "Mon", "Tue", "Wed", "Thu", "Fri" };

        [JsonProperty("ID")]
        public readonly int ID;
        public TimeSpan Time { get; set; }
        public string AlarmName { get; set; }
        public bool IsRepeated;
        public bool[] SelectedDays;
        public bool IsNagging;
        public int AlarmsBefore, AlarmsAfter, Interval;

        public string Repeatition
        {
            get
            {
                if (IsRepeated)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < 7; i++)
                    {
                        if (SelectedDays[i])
                        {
                            sb.Append(Days[i]);
                            sb.Append(", ");
                        }
                    }
                    return sb.ToString().TrimEnd( new char[] {',',' '} );
                }
                else
                {
                    return "One time";
                }
            }
        }
        public string Nagging
        {
            get
            {
                if (IsNagging)
                {
                    var sb = new StringBuilder();
                    sb.Append("Alarms before = " + AlarmsBefore);
                    sb.Append(" \nAlarms after = " + AlarmsAfter);
                    sb.Append(" \nInterval = " + Interval);
                    return sb.ToString();
                }
                else
                {
                    return "Will you really wake up?";
                }
            }
        }
        public string AlarmTimeString
        {
            get
            {
                int h = Time.Hours;
                string AmOrPm = "PM";
                if (h < 12) AmOrPm = "AM";
                else if (h > 12) h %= 12;

                string m = (Time.Minutes < 10) ? "0" + Time.Minutes : Time.Minutes.ToString();
                return ((h==0)? "00":h.ToString()) + ":" + m + " " + AmOrPm;
            }
        }


        public Alarm(TimeSpan Time, string AlarmName, bool IsRepeated, bool[] SelectedDays, bool IsNagging, int[] NaggingSettings)
        {
            IdCount = Alarms.Count != 0 ? Alarms.Last().ID + 1 : 0;
            this.ID = IdCount;
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
            this.AlarmsBefore = NaggingSettings != null? NaggingSettings[0] : 0;
            this.AlarmsAfter = NaggingSettings != null ? NaggingSettings[1] : 0;
            this.Interval = NaggingSettings != null ? NaggingSettings[2] : 10;
        }
    }
}
