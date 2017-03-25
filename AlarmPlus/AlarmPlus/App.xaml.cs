using AlarmPlus.Core;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlarmPlus
{
    public partial class App : Application
    {

        public static async Task SaveAlarms()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFile file = await rootFolder.CreateFileAsync("Alarms", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JsonConvert.SerializeObject(Alarm.Alarms));
        }

        public static async Task LoadAlarms()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            var x = await rootFolder.CheckExistsAsync("Alarms");

            if (x.Equals(PCLStorage.ExistenceCheckResult.FileExists))
            {
                IFile file = await rootFolder.GetFileAsync("Alarms");
                Alarm.Alarms.Clear();
                if (file != null)
                {
                    string serializedAlarms = await file.ReadAllTextAsync();
                    if (serializedAlarms != null && !serializedAlarms.Equals(string.Empty))
                    {
                        Alarm.Alarms.AddRange(JsonConvert.DeserializeObject<List<Alarm>>(serializedAlarms));
                    }
                }
            }
            
        }

        public App()
        {
            LoadAlarms();
            InitializeComponent();
            MainPage = new NavigationPage( new AlarmPlus.GUI.MainTabbedPage() );
        }

        protected async override void OnStart()
        {
            await LoadAlarms();
        }

        protected override void OnSleep()
        {
            //await SaveAlarms();
        }

        protected async override void OnResume()
        {
            await LoadAlarms();
        }
    }
}
