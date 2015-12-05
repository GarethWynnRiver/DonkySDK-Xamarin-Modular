using System;
using System.Collections.Generic;
using System.Linq;
using Donky.Core.Xamarin.iOS;
using Donky.Messaging.Push.UI.iOS;
using Foundation;
using UIKit;

using QuickstartApp;
using ImageCircle.Forms.Plugin.iOS;
using Donky.Core.Xamarin.iOS.Forms;
using Xamarin.Forms;

namespace QuickstartApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            MessagingCenter.Subscribe<string>(this, "ShareRichMessage", ShareText, null);

            Xamarin.Forms.Forms.Init();
            DonkyiOSForms.Init();         // Xamarin Forms Custom iOS Renderers
            ImageCircleRenderer.Init();
            DonkyiOS.Initialise();        // Xamarin.iOS framework
            DonkyPushUIiOS.Initialise();  // Xamarin.iOS framework
            AppBootstrap.Initialise();    // Xamarin.Forms PCL

            LoadApplication(new App());   // Xamarin.Forms PCL

            return base.FinishedLaunching(app, options);
        }

        async void ShareText(string text)
        {
            var item = NSObject.FromObject(text);
            var activityItems = new[] { item };
            var activityController = new UIActivityViewController(activityItems, null);

            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            topController.PresentViewController(activityController, true, () => { });
        }

        public override void RegisteredForRemoteNotifications(
          UIApplication application, NSData deviceToken)
        {
            DonkyiOS.RegisteredForRemoteNotifications(
            application,
            deviceToken);
        }

        public override void DidReceiveRemoteNotification(
          UIApplication application,
          NSDictionary userInfo,
          Action<UIBackgroundFetchResult> completionHandler)
        {
            DonkyiOS.DidReceiveRemoteNotification(
              application,
              userInfo,
              completionHandler);
        }

        public override void HandleAction(
          UIApplication application,
          string actionIdentifier,
          NSDictionary remoteNotificationInfo,
          Action completionHandler)
        {
            DonkyiOS.HandleAction(
              application,
              actionIdentifier,
              remoteNotificationInfo,
              completionHandler);
        }

        public override void FailedToRegisterForRemoteNotifications(
          UIApplication application,
          NSError error)
        {
            DonkyiOS.FailedToRegisterForRemoteNotifications(
            application,
            error);
        }
    }
}
