﻿using AlarmPlus.Core;
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
            this.BindingContext = alarm;
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

        private async void SnoozeButton_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
            CrossMediaManager.Current.MediaNotificationManager.StopNotifications();
            //insert snooze code here
            await Navigation.PopAsync(true);
        }

        private async void CloseButton_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Stop();
            CrossMediaManager.Current.MediaNotificationManager.StopNotifications();
            await Navigation.PopAsync(true);
        }
    }
}
