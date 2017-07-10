using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmPlus.Core
{
    public interface IDatabasePlatformPicker
    {

        ISQLitePlatform GetPlatform();
    }
}
