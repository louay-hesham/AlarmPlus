using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        [JsonIgnore]
        private static readonly DayOfWeek[] Days = { DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };


        [JsonProperty("ID")]
        public readonly int ID;
        public bool Enabled;
        public TimeSpan Time { get; set; }
        public string AlarmName { get; set; }
        public bool IsRepeated;
        public bool[] SelectedDaysBool;
        public bool IsNagging;
        public int AlarmsBefore, AlarmsAfter, Interval;

        [JsonIgnore]
        public readonly List<DateTime> AllTimes;

        [JsonIgnore]
        private int AlarmsPerDay;

        [JsonIgnore]
        public List<DayOfWeek> SelectedDays;

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
                        if (SelectedDaysBool[i])
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
        public string OriginalAlarmTimeString
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

        [JsonIgnore]
        public bool IsEnabled
        {
            get
            {
                return Enabled;
            }
            set
            {
                Enabled = value;
                ToggleAlarm();
            }
        }

        [JsonIgnore]
        public ICommand EditCommand { get; private set; }

        [JsonIgnore]
        public ICommand DeleteCommand { get; private set; }


        public Alarm(TimeSpan Time, string AlarmName, bool IsRepeated, bool[] SelectedDaysBool, bool IsNagging, int[] NaggingSettings)
        {
            EditCommand = new Command(EditAlarm);
            DeleteCommand = new Command(DeleteAlarm);
            AllTimes = new List<DateTime>();

            _IdCount = Alarms.Count != 0 ? Alarms.Last().ID + 1 : 0;
            ID = _IdCount;

            Enabled = true;
            this.Time = Time;
            if (AlarmName == null || AlarmName.Equals(string.Empty))
            {
                if (_NewAlarmCount == 0) this.AlarmName = "New alarm";
                else this.AlarmName = "New alarm " + _NewAlarmCount;
                _NewAlarmCount++;
            }
            else this.AlarmName = AlarmName;
            this.IsRepeated = IsRepeated;
            this.SelectedDaysBool = SelectedDaysBool;
            this.IsNagging = IsNagging;
            AlarmsBefore = IsNagging? (NaggingSettings != null? NaggingSettings[0] : 2) : 0;
            AlarmsAfter = IsNagging? (NaggingSettings != null ? NaggingSettings[1] : 1) : 0;
            Interval = IsNagging? (NaggingSettings != null ? NaggingSettings[2] : 10) : 0;

            SelectedDays = new List<DayOfWeek>();
            for (int i = 0; i < 7; i++)
            {
                if (SelectedDaysBool[i]) SelectedDays.Add(Days[i]);
            }
            AlarmsPerDay = 0;

            if (!IsNagging) AlarmsPerDay = 1;
            else AlarmsPerDay = 1 + AlarmsBefore + AlarmsAfter;
            CalculateAlarms();
        }

        private void CalculateAlarms()
        {
            int AlarmsCount = 0;
            if (!IsRepeated)
            {
                var baseTime = DateTime.Now.Date.Add(Time);
                if (baseTime.Hour < DateTime.Now.Hour || (baseTime.Hour == DateTime.Now.Hour && baseTime.Minute <= DateTime.Now.Minute))
                    baseTime = baseTime.AddDays(1);

                while (AlarmsCount < AlarmsPerDay)
                {
                    var nextDateAndTime = baseTime.AddMinutes((AlarmsCount - AlarmsBefore) * Interval);
                    AlarmsCount++;
                    AllTimes.Add(nextDateAndTime);
                }
            }
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    if (SelectedDaysBool[i])
                    {
                        AlarmsCount = 0;
                        int daysUntilSelectedDay = (((int)Days[i] - (int)DateTime.Now.DayOfWeek) + 7) % 7;
                        var baseTimeOfTheDay = DateTime.Now.Date.AddDays(daysUntilSelectedDay).Add(Time);
                        if (daysUntilSelectedDay == 0 && (baseTimeOfTheDay.Hour < DateTime.Now.Hour || (baseTimeOfTheDay.Hour == DateTime.Now.Hour && baseTimeOfTheDay.Minute <= DateTime.Now.Minute)))
                            baseTimeOfTheDay = baseTimeOfTheDay.AddDays(7);
                        while (AlarmsCount < AlarmsPerDay)
                        {
                            var nextDateAndTimeOfTheDay = baseTimeOfTheDay.AddMinutes((AlarmsCount - AlarmsBefore) * Interval);
                            AlarmsCount++;
                            AllTimes.Add(nextDateAndTimeOfTheDay);
                        }
                        
                    }
                }
            }
        }

        private void EditAlarm()
        {
            Debug.WriteLine("Alarm ID to edit is " + ID);
        }

        private async void DeleteAlarm()
        {
            Alarms.Remove(this);
            await App.SaveAlarms();
            App.AlarmSetter.CancelAlarm(this);
        }

        private void ToggleAlarm()
        {
            if (Enabled)
            {
                if (App.AlarmSetter != null)
                {
                    App.AlarmSetter.SetAlarm(this);
                }
            }
            else
            {
                App.AlarmSetter.CancelAlarm(this);
            }
        }
    }
}
