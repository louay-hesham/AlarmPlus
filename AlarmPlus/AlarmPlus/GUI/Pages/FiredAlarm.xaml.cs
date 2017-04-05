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
        public FiredAlarm(Alarm alarm)
        {
            InitializeComponent();
            TimeLabel.Text = alarm.AlarmTimeString;
            PlayAlarmSound();
        }

        private async void PlayAlarmSound()
        {
            if (App.RingtoneManager != null)
            {
                await CrossMediaManager.Current.Play(App.RingtoneManager.GetRingtone());
            }
        }
    }
}
