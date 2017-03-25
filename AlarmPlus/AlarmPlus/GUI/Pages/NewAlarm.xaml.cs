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
        private readonly MyAlarmsTab MyAlarmsPage;

        public NewAlarm(MyAlarmsTab MyAlarmsPage)
        {
            InitializeComponent();
            InitializeUIComponents();
            this.MyAlarmsPage = MyAlarmsPage;
        }

        private void InitializeUIComponents()
        {
            WeekDay.IsVisible = IsRepeated.On;
            Nagging.IsVisible = IsNagging.On;
        }

        private void RepeatStatusChanged(object sender, EventArgs e)
        {
            WeekDay.IsVisible = IsRepeated.On;
        }

        private void NaggingStatus_OnChanged(object sender, ToggledEventArgs e)
        {
            Nagging.IsVisible = IsNagging.On;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            TimeSpan time = AlarmTime.Time;
            string alarmName = (AlarmName.Text == null || AlarmName.Text.Equals(string.Empty)) ? null : AlarmName.Text;
            bool[] selectedDays = IsRepeated.On ? WeekDay.ButtonsPressed : new bool[7];
            int[] naggingData = IsNagging.On ? Nagging.GetNaggingSettings() : new int[3];
            Alarm alarm = new Alarm(time, alarmName, IsRepeated.On, selectedDays, IsNagging.On, naggingData);
            Alarm.Alarms.Add(alarm);
            MyAlarmsPage.AddAlarmToUI();

            Navigation.PopAsync(true);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
    }
}
