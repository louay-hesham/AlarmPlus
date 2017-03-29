using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AlarmPlus.Core
{
    public class Alarm
    {
        private static int _NewAlarmCount = 0;
        private static int _IdCount = 0;
        private static readonly string[] _Days = { "Sat", "Sun", "Mon", "Tue", "Wed", "Thu", "Fri" };

        public static ObservableCollection<Alarm> Alarms = new ObservableCollection<Alarm>();


        [JsonProperty("ID")]
        public readonly int ID;
        public TimeSpan Time { get; set; }
        public string AlarmName { get; set; }
        public bool IsRepeated;
        public bool[] SelectedDays;
        public bool IsNagging;
        public int AlarmsBefore, AlarmsAfter, Interval;

        [JsonIgnore]
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
                            sb.Append(_Days[i]);
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

        [JsonIgnore]
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

        [JsonIgnore]
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

        //[JsonIgnore]
        //public int id
        //{
        //    get
        //    {
        //        return ID;
        //    }
        //}

        [JsonIgnore]
        public ICommand EditCommand { get; private set; }

        [JsonIgnore]
        public ICommand DeleteCommand { get; private set; }

        [JsonIgnore]
        public ICommand EnableCommand { get; private set; }

        public Alarm(TimeSpan Time, string AlarmName, bool IsRepeated, bool[] SelectedDays, bool IsNagging, int[] NaggingSettings)
        {
            EditCommand = new Command(EditAlarm);
            DeleteCommand = new Command(DeleteAlarm);
            EnableCommand = new Command(EnableAlarm);

            _IdCount = Alarms.Count != 0 ? Alarms.Last().ID + 1 : 0;
            this.ID = _IdCount;
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

        private void EditAlarm()
        {
            Debug.WriteLine("Alarm ID to edit is " + ID);
        }

        private async void DeleteAlarm()
        {
            Alarms.Remove(this);
            await App.SaveAlarms();
        }

        private void EnableAlarm()
        {
            Debug.WriteLine("Alarm ID to enable/disable is " + ID);
        }
    }
}
