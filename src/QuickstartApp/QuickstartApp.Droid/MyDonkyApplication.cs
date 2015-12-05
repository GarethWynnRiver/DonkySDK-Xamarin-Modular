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
using Donky.Core.Xamarin.Android;
using Donky.Messaging.Push.UI.Android;

namespace QuickstartApp.Droid
{
    [Application]
    public class MyDonkyApplication : Application
    {
        public MyDonkyApplication(
          IntPtr handle,
          JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            DonkyAndroid.Initialise();
            DonkyPushUIAndroid.Initialise();
            QuickstartApp.AppBootstrap.Initialise();
        }
    }
}