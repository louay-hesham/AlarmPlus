using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public class Settings
    {
        public string AlarmsAfter, AlarmsBefore, NaggingInterval, SnoozeInterval;

        public Settings(string AlarmsBefore, string AlarmsAfter, string NaggingInterval, string SnoozeInterval)
        {
            this.AlarmsBefore = AlarmsBefore;
            this.AlarmsAfter = AlarmsAfter;
            this.NaggingInterval = NaggingInterval;
            this.SnoozeInterval = SnoozeInterval;
        }
    }
}
