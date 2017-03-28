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
                    return sb.ToString();
                }
                else
                {
                    return "One time";
                }
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
