using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public class Settings
    {
        [PrimaryKey, Column(name:"SettingsID")]
        public int ID { get; set; }
        [Column(name: "DefaultAlarmsAfter")]
        public string AlarmsAfterString { get; set; }
        [Column(name: "DefaultAlarmsBefore")]
        public string AlarmsBeforeString { get; set; }
        [Column(name: "DefaultNaggingInterval")]
        public string NaggingIntervalString { get; set; }
        [Column(name: "DefaultSnoozeInterval")]
        public string SnoozeIntervalString { get; set; }
        [Column(name: "Ringtone")]
        public string RingtoneName { get; set; }
        [Column(name: "DefaultSelectedDaysID")]
        public int DefaultSelectedDaysID { get; set; }

        [Ignore]
        public bool[] DefaultSelectedDays { get; set; }

        [Ignore]
        public SelectedDays DefaultSelectedDaysObject { get; set; }

        [Ignore]
        public string RingtoneNameDetailed
        {
            get
            {
                if (string.IsNullOrEmpty(RingtoneName)) return "No ringtone selected!";
                else return RingtoneName;
            }
        }

        [Ignore]
        public int AlarmsAfter
        {
            get
            {
                return int.Parse(AlarmsAfterString);
            }
        }

        [Ignore]
        public int AlarmsBefore
        {
            get
            {
                return int.Parse(AlarmsBeforeString);
            }
        }

        [Ignore]
        public int NaggingInterval
        {
            get
            {
                return int.Parse(NaggingIntervalString);
            }
        }

        [Ignore]
        public int SnoozeInterval
        {
            get
            {
                return int.Parse(SnoozeIntervalString);
            }
        }

        public Settings() { }

        public Settings(string AlarmsBefore, string AlarmsAfter, string NaggingInterval, string SnoozeInterval)
        {
            this.AlarmsBeforeString = AlarmsBefore;
            this.AlarmsAfterString = AlarmsAfter;
            this.NaggingIntervalString = NaggingInterval;
            this.SnoozeIntervalString = SnoozeInterval;
            this.DefaultSelectedDaysObject = new SelectedDays(new bool[] { false, true, true, true, true, true, false });
            Database.SaveSelectedDays(DefaultSelectedDaysObject);
            this.DefaultSelectedDaysID = DefaultSelectedDaysObject.ID;
            DefaultSelectedDays = DefaultSelectedDaysObject.ToArray();
        }

        public void InitializeSettings()
        {
            DefaultSelectedDaysObject = Database.GetSelectedDays(DefaultSelectedDaysID);
            DefaultSelectedDays = DefaultSelectedDaysObject.ToArray();
        }
    }
}
