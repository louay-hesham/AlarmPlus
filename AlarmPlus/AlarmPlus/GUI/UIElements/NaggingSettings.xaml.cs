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
        }

        public int[] GetNaggingSettings()
        {
            return new int[]
            {
                (Before.Text == null || Before.Text.Equals(string.Empty)) ? 0 : int.Parse(Before.Text),
                (After.Text == null || After.Text.Equals(string.Empty)) ? 0 : int.Parse(After.Text),
                (Interval.Text == null || Interval.Text.Equals(string.Empty)) ? 10 : int.Parse(Interval.Text)
            };
        }
    }
}
