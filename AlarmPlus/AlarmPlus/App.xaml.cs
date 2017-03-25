using AlarmPlus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AlarmPlus
{
    public partial class App : Application
    { 
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new AlarmPlus.GUI.MainTabbedPage() );
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (Application.Current.Properties.ContainsKey("Alarms"))
            {
                var alarms = Current.Properties["Alarms"] as List<Alarm>;
                Alarm.Alarms.AddRange(alarms);
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Current.Properties["Alarms"] = Alarm.Alarms;
            SavePropertiesAsync();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (Application.Current.Properties.ContainsKey("Alarms"))
            {
                var alarms = Current.Properties["Alarms"] as List<Alarm>;
                Alarm.Alarms.AddRange(alarms);
            }
        }
    }
}
