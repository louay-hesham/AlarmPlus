using AlarmPlus.Core;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlarmPlus
{
    public partial class App : Application
    {
        private static App Instance = null;

        public static NavigationPage NavPage;

        public static Settings AppSettings { get; set; }

        public static new App Current
        {
            get
            {
                if (Instance == null) Instance = new App();
                return Instance;
            }
        }

        public static ISQLitePlatform DatabasePlatform
        {
            get
            {
                return DependencyService.Get<IDatabasePlatformPicker>().GetPlatform();
            }
        }

        public static IRingtoneManager RingtoneManager
        {
            get
            {
                return DependencyService.Get<IRingtoneManager>();
            }
        }

        public static IAppMinimizer AppMinimizer
        {
            get
            {
                return DependencyService.Get<IAppMinimizer>();
            }
        }

        public static IAlarmSetter AlarmSetter
        {
            get
            {
                return DependencyService.Get<IAlarmSetter>();
            }
        }

        public static void LoadAlarms()
        {
            if (Alarm.Alarms.Count == 0)
            {
                var loadedAlarms = Database.GetAlarms();
                foreach (Alarm alarm in loadedAlarms)
                {
                    Alarm.Alarms.Add(alarm);
                }
            }
        }

        public static void LoadAppSettings()
        {
            AppSettings = Database.GetSettings();
            if (AppSettings == null)
            {
                AppSettings = new Settings("2", "1", "10", "10");
                Database.SaveSettings(AppSettings);
            }
            else AppSettings.InitializeSettings();
        }

        public App()
        {
            Database.InitializeDatabase();
            LoadAppSettings();
            LoadAlarms();
            InitializeComponent();
            NavPage = new NavigationPage();
            MainPage = NavPage;
            NavPage.Navigation.PushAsync(new GUI.MainTabbedPage(), true);
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
