using Com.Xamtastic.Patterns.SmallestMvvm;
using Donky.Core;
using Donky.Core.Notifications.Content;

using Donky.Core.Framework.Extensions;
using QuickstartApp.ViewModels.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickstartApp.ViewModels
{
    public class SendPageViewModel : ViewModelBase
    {
        private IDonkyCore _target = DonkyCore.Instance;

        public ICommand _sendCommand;
        public ICommand SendCommand
        {
            get
            {
                return _sendCommand;
            }
            set
            {
                _sendCommand = value;
                RaisePropertyChanged("SendCommand");
            }
        }

        private string _mainText = "Send Page";
        public string MainText
        {
            get
            {
                return _mainText;
            }
            set
            {
                _mainText = value;
                RaisePropertyChanged("MainText");
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RaisePropertyChanged("Username");
            }
        }

        private string _sendMessage;
        public string SendMessage
        {
            get
            {
                return _sendMessage;
            }
            set
            {
                _sendMessage = value;
                RaisePropertyChanged("SendMessage");
            }
        }

        private ObservableCollection<RandomCustomMessageNotificationViewModel> _receivedMessages = new ObservableCollection<RandomCustomMessageNotificationViewModel>();
        public ObservableCollection<RandomCustomMessageNotificationViewModel> ReceivedMessages
        {
            get
            {
                return _receivedMessages;
            }
            set
            {
                _receivedMessages = value;
                RaisePropertyChanged("ReceivedMessages");
            }
        }


        private RandomCustomMessageNotificationViewModel _selectedReceivedMessage;
        public RandomCustomMessageNotificationViewModel SelectedReceivedMessage
        {
            get
            {
                return _selectedReceivedMessage;
            }
            set
            {
                _selectedReceivedMessage = value;
                RaisePropertyChanged("SelectedReceivedMessage");
            }
        }

        public SendPageViewModel()
        {
            // This is a Xamarin Forms messaging pub/sub
            MessagingCenter.Subscribe<RandomCustomMessageNotificationViewModel>(this, "RandomCustomMessageStructure", (vm) =>
            {
                Debug.WriteLine("Received RandomCustomMessageStructure values:");
                Debug.WriteLine("   customId: " + vm.customId);
                Debug.WriteLine("   from: " + vm.from);
                Debug.WriteLine("   to: " + vm.to);
                Debug.WriteLine("   message: " + vm.message);
                this.ReceivedMessages.Insert(0, vm);
            });

            SendCommand = new Command<string>((key) =>
            {
                if (string.IsNullOrEmpty(Username)) return;

                Task.Run(() =>
                {
                    Debug.WriteLine("Send button pressed in Send Page");

                    var notification = new ContentNotification()
                    .ForUsers(Username.Trim())
                    .WithCustomContent("RandomCustomMessageStructure", new
                    {
                        customId = Guid.NewGuid().ToString(),
                        to = Username.Trim(),
                        from = "iosuser",
                        message = SendMessage.Trim()
                    });

                    try
                    {
                        DonkyCore.Instance.NotificationController.SendContentNotificationsAsync(notification)
                            .ExecuteSynchronously();
                        Debug.WriteLine("SendContentNotificationsAsync completed.");
                    }
                    catch (Exception ex)
                    {

                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        this.Username = string.Empty;
                        this.SendMessage = string.Empty;
                    });


                    Debug.WriteLine("Send handler completed.");

                });
            });

            InitialiseAsync().ExecuteInBackground();
        }

        private async Task InitialiseAsync()
        {

        }
    }
}
