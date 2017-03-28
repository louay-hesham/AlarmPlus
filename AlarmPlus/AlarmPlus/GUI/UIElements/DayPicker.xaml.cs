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
        public readonly bool[] ButtonsPressed;
        private readonly Button[] Buttons;

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
        }

        private void ButtonPressed(int i)
        {
            ButtonsPressed[i] = !ButtonsPressed[i];
            Buttons[i].BackgroundColor = ButtonsPressed[i]? Color.Teal : Color.Gray;
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
