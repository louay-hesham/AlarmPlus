using AlarmPlus.Core;
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
        public static int FiredAlarmID = -1;

        public static NavigationPage NavPage;

        public static IRingtoneManager RingtoneManager
        {
            get
            {
                return DependencyService.Get<IRingtoneManager>();
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

        public App()
        {
            LoadAlarms();
            InitializeComponent();
            if (FiredAlarmID != -1)
            {
                Alarm firedAlarm = null;
                foreach (Alarm alarm in Alarm.Alarms)
                {
                    if (alarm.ID == FiredAlarmID)
                    {
                        firedAlarm = alarm;
                    }
                }
                NavPage = new NavigationPage(new GUI.Pages.FiredAlarm(firedAlarm));
                FiredAlarmID = -1;
            }
            else
            {
                NavPage = new NavigationPage(new GUI.MainTabbedPage());
            }
            
            MainPage = NavPage;
        }

        protected override void OnStart()
        {
            LoadAlarms();
        }

        protected async override void OnSleep()
        {
            await SaveAlarms();
        }

        protected override void OnResume()
        {
            LoadAlarms();
        }
    }
}
