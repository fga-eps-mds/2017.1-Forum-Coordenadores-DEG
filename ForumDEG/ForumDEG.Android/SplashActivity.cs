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
using Android.Support.V7.App;

namespace ForumDEG.Droid.Resources {
    [Activity(Label = "Fórum DEG", Icon = "@drawable/icon_android", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity {
        protected override void OnResume() {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}