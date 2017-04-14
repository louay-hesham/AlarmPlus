using AlarmPlus.Abstractions;
using AlarmPlus.Core;
using AlarmPlus.Services;
using Newtonsoft.Json;
using PCLStorage;
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

        public static ICloudService CloudService { get; set; }

        public static ICloudService CloudService { get; set; }

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

        public static async Task SaveAlarms()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile file = await rootFolder.CreateFileAsync("Alarms", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JsonConvert.SerializeObject(Alarm.Alarms));
        }

        public static void LoadAlarms()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var x = rootFolder.CheckExistsAsync("Alarms").Result;

            if (x.Equals(PCLStorage.ExistenceCheckResult.FileExists))
            {
                IFile file = rootFolder.GetFileAsync("Alarms").Result;
                Alarm.Alarms.Clear();
                if (file != null)
                {
                    string serializedAlarms = file.ReadAllTextAsync().Result;
                    if (serializedAlarms != null && !serializedAlarms.Equals(string.Empty))
                    {
                        var loadedAlarms = JsonConvert.DeserializeObject<List<Alarm>>(serializedAlarms);
                        foreach (var alarm in loadedAlarms)
                        {
                            Alarm.Alarms.Add(alarm);
                        }

                    }
                }
            }
            
        }

        public static async Task SaveAppSettings()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile file = await rootFolder.CreateFileAsync("Settings", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JsonConvert.SerializeObject(AppSettings));
        }

        public static void LoadAppSettings()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var x = rootFolder.CheckExistsAsync("Settings").Result;
            if (x.Equals(ExistenceCheckResult.FileExists))
            {
                IFile file = rootFolder.GetFileAsync("Settings").Result;
                if (file != null)
                {
                    string serializedSettings = file.ReadAllTextAsync().Result;
                    if (serializedSettings != null && !serializedSettings.Equals(string.Empty))
                    {
                        AppSettings = JsonConvert.DeserializeObject<Settings>(serializedSettings);
                    }
                    else
                    {
                        AppSettings = new Settings("2", "1", "10", "10");
                    }
                }
            }
            else
            {
                AppSettings = new Settings("2", "1", "10", "10");
            }
        }

        public App()
        {
            CloudService = new AzureCloudService();
            LoadAppSettings();
            LoadAlarms();
            InitializeComponent();
            NavPage = new NavigationPage();
            MainPage = NavPage;
            NavPage.Navigation.PushAsync(new GUI.MainTabbedPage(), true);
        }

        protected override void OnStart()
        {

        }

        protected async override void OnSleep()
        {
            await SaveAlarms();
            await SaveAppSettings();
        }

        protected override void OnResume()
        {
            
        }
    }
}
