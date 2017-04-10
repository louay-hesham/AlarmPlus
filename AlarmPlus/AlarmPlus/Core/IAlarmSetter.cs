using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public interface IAlarmSetter
    {
        void SetAlarm(Alarm alarm);

        void SetAlarm(int AlarmID);

        void CancelAlarm(Alarm alarm);

        void CancelAlarm(int AlarmID);
    }
}
