﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public class Settings
    {
        public string AlarmsAfterString, AlarmsBeforeString, NaggingIntervalString, SnoozeIntervalString;

        [JsonIgnore]
        public int AlarmsAfter
        {
            get
            {
                return int.Parse(AlarmsAfterString);
            }
        }

        [JsonIgnore]
        public int AlarmsBefore
        {
            get
            {
                return int.Parse(AlarmsBeforeString);
            }
        }

        [JsonIgnore]
        public int NaggingInterval
        {
            get
            {
                return int.Parse(NaggingIntervalString);
            }
        }

        [JsonIgnore]
        public int SnoozeInterval
        {
            get
            {
                return int.Parse(SnoozeIntervalString);
            }
        }

        public Settings(string AlarmsBefore, string AlarmsAfter, string NaggingInterval, string SnoozeInterval)
        {
            this.AlarmsBeforeString = AlarmsBefore;
            this.AlarmsAfterString = AlarmsAfter;
            this.NaggingIntervalString = NaggingInterval;
            this.SnoozeIntervalString = SnoozeInterval;
        }
    }
}
