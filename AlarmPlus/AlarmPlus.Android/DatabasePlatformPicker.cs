using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AlarmPlus.Core;
using SQLite.Net.Platform.XamarinAndroid;
using SQLite.Net.Interop;

namespace AlarmPlus.Droid
{
    class DatabasePlatformPicker : IDatabasePlatformPicker
    {
        public ISQLitePlatform getPlatform()
        {
            return new SQLitePlatformAndroid();
        }
    }
}