using AlarmPlus.Core;
using Plugin.MediaManager;
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
    public partial class FiredAlarm : ContentPage
    {
        private readonly Alarm Alarm;

        public FiredAlarm(Alarm Alarm)
        {
            this.Alarm = Alarm;
            InitializeComponent();
            BindingContext = Alarm;
            SnoozeButton.IsEnabled = !Alarm.IsNagging;

            PlayAlarmSound();
        }

        private async void PlayAlarmSound()
        {
            if (App.RingtoneManager != null)
            {
                await CrossMediaManager.Current.Play(App.RingtoneManager.GetRingtone());
                CrossMediaManager.Current.MediaNotificationManager.StopNotifications();
            }
        }

        private void SnoozeButton_Clicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Stop();
            CrossMediaManager.Current.MediaNotificationManager.StopNotifications();
            App.AlarmSetter.Snooze(Alarm);
            App.NavPage.Navigation.PopAsync(true);
            App.AppMinimizer.MinimizeApp();
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            CrossMediaManager.Current.Stop();
            CrossMediaManager.Current.MediaNotificationManager.StopNotifications();
            App.NavPage.Navigation.PopAsync(true);
            App.AppMinimizer.MinimizeApp();
        }
    }
}
