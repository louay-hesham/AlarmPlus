using AlarmPlus.Core;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.MediaManager;
using System;
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
            BindingContext = App.AppSettings;
            WeekDay.IsVisible = true;
            WeekDay.IsFromSettingsTab = true;
            WeekDay.SelectDays(App.AppSettings.DefaultSelectedDays);
            Disappearing += new EventHandler(SaveSettingsOnSwipeLeft);
        }

        private async void RingtoneChooserButton_Clicked(object sender, EventArgs e)
        {
            FileData fileData = await CrossFilePicker.Current.PickFile();
            await App.RingtoneManager.SetRingtone(fileData);
            RingtoneButton.Text = App.AppSettings.RingtoneName;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Play(App.RingtoneManager.GetRingtone());
        }

        private void SaveSettingsOnSwipeLeft(object sender, EventArgs e)
        {
            App.AppSettings.DefaultSelectedDaysObject.SetDays(WeekDay.Days);
            App.AppSettings.DefaultSelectedDays = WeekDay.Days.ToArray();
            Database.SaveSettings(App.AppSettings);
        }
    }
}
