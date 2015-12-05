using Com.Xamtastic.Patterns.SmallestMvvm;
using Donky.Core;
using QuickstartApp.Models;
using QuickstartApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickstartApp.Pages
{
    [ViewModelType(typeof(RichMessageInboxPageViewModel))]
    public partial class PhoneRichMessageInboxPage : PageBase
    {
        public static bool ForceReload { get; set; }

        public PhoneRichMessageInboxPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            ((RichMessageInboxPageViewModel)this.BindingContext).SetNavigationManager(this.Navigation);

            MessagingCenter.Subscribe<AlertModel>(this, "ExpiredMessageAlert", async (alert) =>
            {
                var result = await DisplayAlert(alert.Title, alert.Body, "Delete All", "Delete");

                MessagingCenter.Send<DeleteModel>(new DeleteModel { DeleteAll = result, Message = alert.Message }, "DeleteRichMessage");

            });

            MessagingCenter.Subscribe<AlertModel>(this, "UnableToShareAlert", async (alert) =>
            {
                await DisplayAlert(alert.Title, alert.Body, "OK");
            });

            MessagingCenter.Subscribe<ActionSheetModel>(this, "RichMessageInboxListViewItemActionSheet", async (actionsheetmodel) =>
            {
               //var action = await DisplayActionSheet("Rich Message Actions", "Cancel", "Delete", "Share");

                var action = await DisplayActionSheet("Rich Message Actions", "Cancel", "Delete", actionsheetmodel.Buttons);

               Debug.WriteLine("Action: " + action);

                switch(action.ToLower())
                {
                    case "share":
                        MessagingCenter.Send<ActionSheetModel>(new ActionSheetModel { RichMessageId = actionsheetmodel.RichMessageId }, "ShareRichMessageActionSheet");
                        break;

                    case "delete":
                        MessagingCenter.Send<ActionSheetModel>(new ActionSheetModel { RichMessageId = actionsheetmodel.RichMessageId }, "DeleteRichMessageActionSheet");
                        break;

                    case "cancel":
                        // do nothing
                        break;
                    default:
                        throw new Exception("Action not found for RichMessageInboxListViewItemActionSheet in PhoneRichMessageInboxPage");
                }
            });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = "Inbox";

            ((RichMessageInboxPageViewModel)this.BindingContext).OnAppearing();
        }

        void SelectAllToggle(object sender, EventArgs e)
        {
            ((RichMessageInboxPageViewModel)this.BindingContext).ToggleSelectorCommand.Execute(null);
        }
    }
}
