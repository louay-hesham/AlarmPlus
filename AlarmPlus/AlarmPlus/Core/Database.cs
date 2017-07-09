using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlarmPlus.Core
{
    public class Database
    {

        private static SQLiteConnection connection;

        public static void InitializeDatabase()
        {
            connection = new SQLiteConnection(App.DatabasePlatform, DependencyService.Get<IFileHelper>().GetLocalFilePath("Database.db3"));
            connection.CreateTable<Alarm>();
            connection.CreateTable<SelectedDays>();
            connection.CreateTable<Settings>();
        }

        //Alarms operations
        public static List<Alarm> GetAlarms()
        {
            var alarms = connection.Table<Alarm>().ToList();
            foreach (Alarm alarm in alarms)
            {
                alarm.InitAlarm();
            }
            return alarms;
        }

        public static Alarm GetAlarm(int id)
        {
            return connection.Table<Alarm>().Where(i => i.ID == id).FirstOrDefault();
        }

        public static int SaveAlarm(Alarm item)
        {
            if (item.ID != 0)
            {
                return connection.Update(item);
            }
            else
            {
                return connection.Insert(item);
            }
        }

        public static int DeleteAlarm(Alarm item)
        {
            return connection.Delete(item);
        }

        //SelectedDays operations
        public static SelectedDays GetSelectedDays(int id)
        {
            return connection.Table<SelectedDays>().Where(i => i.ID == id).FirstOrDefault();
        }

        public static int SaveSelectedDays(SelectedDays item)
        {
            if (item.ID != 0)
            {
                return connection.Update(item);
            }
            else
            {
                return connection.Insert(item);
            }
        }

        public static int DeleteSelectedDays(SelectedDays item)
        {
            return connection.Delete(item);
        }

        //App Settings operations
        public static Settings GetSettings()
        {
            return connection.Table<Settings>().FirstOrDefault();
        }

        public static int SaveSettings(Settings item)
        {
            SaveSelectedDays(item.DefaultSelectedDaysObject);
            if (item.ID != 0)
            {
                return connection.Update(item);
            }
            else
            {
                return connection.Insert(item);
            }
        }
    }
}
