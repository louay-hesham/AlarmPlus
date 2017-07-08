using Android.App;
using Android.OS;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace AlarmPlus.Droid
{
    [Activity(Label = "AlarmActivity", MainLauncher = false)]
    public class AlarmActivity : FormsAppCompatActivity
    {
        public static AlarmActivity Instance = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Forms.Init(this, savedInstanceState);
            LoadApplication(new AlarmFired());

            Window.AddFlags(WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.TurnScreenOn);
            Instance = this;
        }
    }
}