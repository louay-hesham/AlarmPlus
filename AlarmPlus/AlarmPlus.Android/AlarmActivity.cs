using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using AlarmPlus.Core;

namespace AlarmPlus.Droid
{
    [Activity(Label = "AlarmActivity", MainLauncher = false)]
    public class AlarmActivity : FormsAppCompatActivity
    {
        public static AlarmActivity Instance = null;

        public static void Minimize()
        {
            Intent startMain = new Intent(Intent.ActionMain);
            startMain.AddCategory(Intent.CategoryHome);
            startMain.SetFlags(ActivityFlags.NewTask);
            Instance.StartActivity(startMain);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Window.AddFlags(WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.TurnScreenOn);
            Instance = this;
        }
    }
}