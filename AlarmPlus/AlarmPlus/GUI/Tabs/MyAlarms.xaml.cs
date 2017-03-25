using AlarmPlus.GUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlarmPlus.GUI.Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAlarms : ContentPage
    {
        private int AlarmsCount;

        public MyAlarms()
        {
            InitializeComponent();
            this.AlarmsCount = 0;
        }

        public void UpdateLabelText()
        {
            AlarmsCount++;
            AlarmsCountLabel.Text = "#Alarms = " + AlarmsCount;
        }

        async private void NewAlarmsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAlarm(this), true);
        }
    }
}
