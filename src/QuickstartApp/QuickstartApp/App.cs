using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Donky.Core.Xamarin.Forms;
using Xamarin.Forms;
using Donky.Core;
using System.Threading.Tasks;
using QuickstartApp.Pages;
using System.Diagnostics;
using QuickstartApp.ViewModels.Notifications;
using Donky.Core.Registration;
using Donky.Core.Notifications;

namespace QuickstartApp
{
    public class App : DonkyApplication
    {
        public App()
        {
            DonkyCore.Instance.SubscribeToNotifications(
            new ModuleDefinition("RandomCustomMessageVersionDefinition", "1.0"),
            new[]
                {
                    new CustomNotificationSubscription()
                    {
                        Type = "RandomCustomMessageStructure",
                        Handler = (x) =>
                        {
                            Debug.WriteLine("Notifcation Json Received: " + x.Data.ToString());

                            var msg = new RandomCustomMessageNotificationViewModel(x.Data.ToString());
                            MessagingCenter.Send(msg, "RandomCustomMessageStructure");

                            return Task.FromResult(string.Format("Message: Some work was done on {0}", x.Data.ToString()));
                        }
                    }
                }
            );
            // The root page of your application

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
            {
                MainPage = new NavigationPage(new PhoneRichMessageInboxTabbedPage());
            }
            else if(Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                MainPage = new NavigationPage(new TabletRichMessageInboxTabbedPage());
            }
            else if(Xamarin.Forms.Device.Idiom == TargetIdiom.Desktop)
            {

            }
            else if (Xamarin.Forms.Device.Idiom == TargetIdiom.Unsupported)
            {
                MainPage = new NavigationPage(new PhoneRichMessageInboxTabbedPage());
            }
            else
            {
                MainPage = new NavigationPage(new PhoneRichMessageInboxTabbedPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
