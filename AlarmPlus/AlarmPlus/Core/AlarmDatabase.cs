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

        private static SQLiteConnection database;

        public static void InitializeDatabase()
        {
            database = new SQLiteConnection(App.DatabasePlatform, DependencyService.Get<IFileHelper>().GetLocalFilePath("AlarmDatabase.db3"));
            database.CreateTable<Alarm>();
        }

        public static List<Alarm> GetItems()
        {
            return database.Table<Alarm>().ToList();
        }

        public static List<Alarm> GetItemsNotDone()
        {
            return database.Query<Alarm>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public static Alarm GetItem(int id)
        {
            return database.Table<Alarm>().Where(i => i.ID == id).FirstOrDefault();
        }

        public static int SaveItem(Alarm item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public static int DeleteItem(Alarm item)
        {
            return database.Delete(item);
        }
    }
}
