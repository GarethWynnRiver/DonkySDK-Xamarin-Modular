using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Donky.Core.Xamarin.Android;
using Donky.Core.Xamarin.Android.Forms;
using Android.Content;
using Xamarin.Forms;

namespace QuickstartApp.Droid
{
    [Activity(Label = "QuickstartApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            MessagingCenter.Subscribe<string>(this, "ShareRichMessage", ShareText, null);

            DonkyAndroid.ActivityLaunchedWithIntent(this);
            DonkyAndroidForms.Init();
            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        async void ShareText(string text)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");

            intent.PutExtra(Android.Content.Intent.ExtraText, text);

            var intentChooser = Intent.CreateChooser(intent, "Share via");

            StartActivityForResult(intentChooser, 0);
        }
    }
}

