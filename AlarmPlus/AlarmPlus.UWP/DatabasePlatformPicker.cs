using AlarmPlus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;
using Xamarin.Forms;
using AlarmPlus.UWP;

[assembly: Dependency(typeof(DatabasePlatformPicker))]
namespace AlarmPlus.UWP
{
    public class DatabasePlatformPicker : IDatabasePlatformPicker
    {
        public ISQLitePlatform GetPlatform()
        {
            return new SQLitePlatformWinRT();
        }
    }
}
