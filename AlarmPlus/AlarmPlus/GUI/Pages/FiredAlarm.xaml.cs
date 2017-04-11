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

            var Now = DateTime.Now;
            int h = Now.Hour;
            string AmOrPm = "PM";
            if (h < 12) AmOrPm = "AM";
            else if (h > 12) h %= 12;

            string m = (Now.Minute < 10) ? "0" + Now.Minute : Now.Minute.ToString();
            TimeLabel.Text = ((h == 0) ? "00" : h.ToString()) + ":" + m + " " + AmOrPm;
            if (!Alarm.IsRepeated)
                Alarm.IsEnabled = false;
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
            //insert snooze code here
            Navigation.PopAsync(true);
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
