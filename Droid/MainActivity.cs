using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Firebase;
using Android.Gms.Auth.Api.SignIn;
using firebasesample.Droid.Services.FirebaseAuth;
using Xamarin.Forms;

namespace firebasesample.Droid
{
    [Activity(Label = "firebase-sample.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static FirebaseApp app;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            InitFirebaseAuth();
            UserDialogs.Init(this);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
            .SetApplicationId("1:2485447395:android:1bf7180db061f771")
            .SetApiKey("AIzaSyBdszK9ZCwbukS8Qb1iZ_LCXVq2os-KYJA")
            .SetDatabaseUrl("https://fir-sample-d9469.firebaseio.com")
            .Build();



            if (app == null)
                app = FirebaseApp.InitializeApp(this, options, "FirebaseSample");

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == FirebaseAuthService.REQ_AUTH && resultCode == Result.Ok)
            {
                GoogleSignInAccount sg = (GoogleSignInAccount)data.GetParcelableExtra("result");
                MessagingCenter.Send(FirebaseAuthService.KEY_AUTH, FirebaseAuthService.KEY_AUTH, sg.IdToken);
            }
        }
    }
}
