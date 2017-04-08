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
using Xamarin.Forms;
using AlarmPlus.Droid;
using AlarmPlus.Core;

[assembly: Dependency(typeof(AppMinimizer))]
namespace AlarmPlus.Droid
{
    class AppMinimizer : IAppMinimizer
    {
        public void MinimizeApp()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}