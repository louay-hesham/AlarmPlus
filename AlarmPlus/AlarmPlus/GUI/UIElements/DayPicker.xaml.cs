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
    public partial class DayPicker : ContentView
    {
        public bool[] ButtonsPressed;
        public List<DayOfWeek> SelectedDays
        {
            get
            {
                SelectedDays = new List<DayOfWeek>();
                for(int i = 0; i < 7; i++)
                {
                    if (ButtonsPressed[i]) SelectedDays.Add(Days[i]);
                }
                return SelectedDays;
            }
            private set
            {

            }
        }
        private readonly Button[] Buttons;
        private readonly DayOfWeek[] Days = { DayOfWeek.Saturday, DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

        public DayPicker()
        {
            InitializeComponent();
            ButtonsPressed = new bool[7];
            Buttons = new Button[7];
            Buttons[0] = Sat;
            Buttons[1] = Sun;
            Buttons[2] = Mon;
            Buttons[3] = Tue;
            Buttons[4] = Wed;
            Buttons[5] = Thu;
            Buttons[6] = Fri;
            for (int j = 1; j <= 5; j++) ButtonPressed(j);
        }

        public void SelectDays(bool[] SelectedDays)
        {
            for (int i = 0; i < 7; i++)
            {
                ButtonsPressed[i] = false;
                Buttons[i].BackgroundColor = Color.Gray;
            }

            for (int j = 0; j < 7; j++)
            {
                if (SelectedDays[j])
                {
                    ButtonPressed(j);
                }
            }
        }

        private void ButtonPressed(int i)
        {
            ButtonsPressed[i] = !ButtonsPressed[i];
            Buttons[i].BackgroundColor = ButtonsPressed[i]? Color.FromRgb(0, 255, 255) : Color.Gray;

            bool stayVisible = false;
            foreach (bool x in ButtonsPressed) stayVisible = stayVisible | x;
            if (!stayVisible)
            {
                for (int j = 1; j <= 5; j++)
                {
                    ButtonsPressed[j] = true;
                    Buttons[j].BackgroundColor = Color.FromRgb(0, 255, 255);
                }
            }
            IsVisible = stayVisible;
        }

        private void Sat_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(0);
        }

        private void Sun_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(1);
        }

        private void Mon_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(2);
        }

        private void Tue_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(3);
        }

        private void Wed_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(4);
        }

        private void Thu_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(5);
        }

        private void Fri_Clicked(object sender, EventArgs e)
        {
            ButtonPressed(6);
        }

    }
}
