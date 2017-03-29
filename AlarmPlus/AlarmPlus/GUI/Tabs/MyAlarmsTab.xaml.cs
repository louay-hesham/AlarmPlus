using AlarmPlus.Core;
using AlarmPlus.GUI.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlarmPlus.GUI.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAlarmsTab : ContentPage
    {
        public MyAlarmsTab()
        {
            InitializeComponent();
            AlarmsListView.ItemsSource = Alarm.Alarms;
        }

        async private void NewAlarmsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAlarm(this), true);
        }
        
    }
}
