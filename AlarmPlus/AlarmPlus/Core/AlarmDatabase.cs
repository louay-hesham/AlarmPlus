using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlarmPlus.Core
{
    class AlarmDatabase
    {

        private static SQLiteConnection AlarmsDatabase;

        public static void InitializeDatabase()
        {
            AlarmsDatabase = new SQLiteConnection(App.DatabasePlatform, DependencyService.Get<IFileHelper>().GetLocalFilePath("AlarmDatabase.db3"));
            AlarmsDatabase.CreateTable<Alarm>();
        }

        public static List<Alarm> GetAlarms()
        {
            return AlarmsDatabase.Table<Alarm>().ToList();
        }

        public static Alarm GetAlarm(int id)
        {
            return AlarmsDatabase.Table<Alarm>().Where(i => i.ID == id).FirstOrDefault();
        }

        public static int SaveAlarm(Alarm item)
        {
            if (item.ID != 0)
            {
                return AlarmsDatabase.Update(item);
            }
            else
            {
                return AlarmsDatabase.Insert(item);
            }
        }

        public static int DeleteAlarm(Alarm item)
        {
            return AlarmsDatabase.Delete(item);
        }
    }
}
