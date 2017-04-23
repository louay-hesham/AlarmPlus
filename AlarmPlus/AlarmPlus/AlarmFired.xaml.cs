using AlarmPlus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlarmPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmFired : Application
    {
        public static int AlarmID;

        public AlarmFired()
        {
            InitializeComponent();
            Alarm alarm = Alarm.GetAlarmByID(AlarmID);
            MainPage = new GUI.Pages.FiredAlarm(alarm);
            alarm.IsEnabled = false;
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {

        }
    }
}
