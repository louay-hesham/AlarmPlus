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
    public partial class SettingsTab : ContentPage
    {
        public SettingsTab()
        {
            InitializeComponent();
            AlarmsBeforeEntry.Text = App.AppSettings.AlarmsBefore;
            AlarmsAfterEntry.Text = App.AppSettings.AlarmsAfter;
            NaggingIntervalEntry.Text = App.AppSettings.NaggingInterval;
            SnoozeIntervalEntry.Text = App.AppSettings.SnoozeInterval;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            App.AppSettings.AlarmsBefore = AlarmsBeforeEntry.Text;
            App.AppSettings.AlarmsAfter = AlarmsAfterEntry.Text;
            App.AppSettings.NaggingInterval = NaggingIntervalEntry.Text;
            App.AppSettings.SnoozeInterval = SnoozeIntervalEntry.Text;
        }
    }
}
