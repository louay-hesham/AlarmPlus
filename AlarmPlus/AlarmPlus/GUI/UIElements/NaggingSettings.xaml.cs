using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlarmPlus.GUI.UIElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NaggingSettings : ContentView
    {
        public NaggingSettings()
        {
            InitializeComponent();
            Before.Text = App.AppSettings.AlarmsBeforeString;
            After.Text = App.AppSettings.AlarmsAfterString;
            Interval.Text = App.AppSettings.NaggingIntervalString;
        }

        public void SetNaggingSettings(int Before, int After, int Interval)
        {
            this.Before.Text = Before.ToString();
            this.After.Text = After.ToString();
            this.Interval.Text = Interval.ToString();
        }

        public int[] GetNaggingSettings()
        {
            return new int[]
            {
                (Before.Text == null || Before.Text.Equals(string.Empty)) ? App.AppSettings.AlarmsBefore : int.Parse(Before.Text),
                (After.Text == null || After.Text.Equals(string.Empty)) ? App.AppSettings.AlarmsAfter : int.Parse(After.Text),
                (Interval.Text == null || Interval.Text.Equals(string.Empty)) ? App.AppSettings.NaggingInterval : int.Parse(Interval.Text)
            };
        }
    }
}
