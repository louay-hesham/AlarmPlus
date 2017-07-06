using AlarmPlus.Core;
using AlarmPlus.GUI.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlarmPlus.GUI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAlarm : ContentPage
    {
        private Alarm AlarmToEdit;

        public NewAlarm(Alarm AlarmToEdit)
        {
            this.AlarmToEdit = AlarmToEdit;
            InitializeComponent();
            InitializeUIComponents();
            AlarmTime.Time = AlarmToEdit.Time;
            AlarmName.Text = AlarmToEdit.AlarmName;
            IsRepeated.On = AlarmToEdit.IsRepeated;
            IsNagging.On = AlarmToEdit.IsNagging;

            WeekDay.SelectDays(AlarmToEdit.SelectedDaysBool);
            Nagging.SetNaggingSettings(AlarmToEdit.AlarmsBefore, AlarmToEdit.AlarmsAfter, AlarmToEdit.Interval);

            Title = "Edit " + AlarmToEdit.AlarmName;
        }

        public NewAlarm()
        {
            InitializeComponent();
            InitializeUIComponents();
            AlarmToEdit = null;
        }

        private void InitializeUIComponents()
        {
            IsRepeated.BindingContext = WeekDay;
            IsRepeated.On = false;
            IsNagging.BindingContext = Nagging;
            IsNagging.On = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            TimeSpan time = AlarmTime.Time;
            string alarmName = (AlarmName.Text == null || AlarmName.Text.Equals(string.Empty)) ? null : AlarmName.Text;
            int[] naggingData = IsNagging.On ? Nagging.GetNaggingSettings() : new int[3];

            SelectedDays days = WeekDay.Days;
            Database.SaveSelectedDays(days);

            Alarm alarm = new Alarm(time, alarmName, IsRepeated.On, days, IsNagging.On, naggingData);
            if (AlarmToEdit == null) Alarm.Alarms.Add(alarm);
            else AlarmToEdit.SetAlarmProperties(alarm);

            App.SaveAlarms();
            await Navigation.PopAsync(true);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}
