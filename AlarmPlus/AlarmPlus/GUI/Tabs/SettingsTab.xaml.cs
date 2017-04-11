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
            AlarmsBeforeEntry.Text = App.AppSettings.AlarmsBeforeString;
            AlarmsAfterEntry.Text = App.AppSettings.AlarmsAfterString;
            NaggingIntervalEntry.Text = App.AppSettings.NaggingIntervalString;
            SnoozeIntervalEntry.Text = App.AppSettings.SnoozeIntervalString;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            App.AppSettings.AlarmsBeforeString = AlarmsBeforeEntry.Text;
            App.AppSettings.AlarmsAfterString = AlarmsAfterEntry.Text;
            App.AppSettings.NaggingIntervalString = NaggingIntervalEntry.Text;
            App.AppSettings.SnoozeIntervalString = SnoozeIntervalEntry.Text;
            await App.SaveAppSettings();
        }
    }
}
