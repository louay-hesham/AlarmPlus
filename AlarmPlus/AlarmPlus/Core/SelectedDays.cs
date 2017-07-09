using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public class SelectedDays
    {
        [PrimaryKey, AutoIncrement, Column(name: "SelectedDaysId")]
        public int ID { get; set; }
        
        public bool sat { get; set; }
        public bool sun { get; set; }
        public bool mon { get; set; }
        public bool tue { get; set; }
        public bool wed { get; set; }
        public bool thu { get; set; }
        public bool fri { get; set; }

        public SelectedDays() { }

        public SelectedDays(bool[] selectedDays)
        {
            sat = selectedDays[0];
            sun = selectedDays[1];
            mon = selectedDays[2];
            tue = selectedDays[3];
            wed = selectedDays[4];
            thu = selectedDays[5];
            fri = selectedDays[6];
        }

        public bool[] ToArray()
        {
            return new bool[] { sat, sun, mon, tue, wed, thu, fri };
        }
    }
}
