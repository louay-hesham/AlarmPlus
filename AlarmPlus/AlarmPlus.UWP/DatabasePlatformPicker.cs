using AlarmPlus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Interop;

namespace AlarmPlus.UWP
{
    public class DatabasePlatformPicker : IDatabasePlatformPicker
    {
        public ISQLitePlatform GetPlatform()
        {
            throw new NotImplementedException();
        }
    }
}
