using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlarmPlus.Backend.DataObjects
{
    public class Alarm : EntityData
    {
        public int LocalID { get; set; }
        public bool Enabled { get; set; }
        public TimeSpan Time { get; set; }
        public string AlarmName { get; set; }
        public bool IsRepeated { get; set; }
        public bool[] SelectedDaysBool { get; set; }
        public bool IsNagging { get; set; }
        public int AlarmsBefore { get; set; }
        public int AlarmsAfter { get; set; }
        public int Interval { get; set; }
    }
}