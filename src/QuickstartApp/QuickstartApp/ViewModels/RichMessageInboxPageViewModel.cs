using Com.Xamtastic.Patterns.SmallestMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Donky.Core;
using Donky.Messaging.Rich.Inbox.XamarinForms.ViewModels;
using Donky.Messaging.Rich.Logic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Xamarin.Forms;
using QuickstartApp.Pages;
using Donky.Core.Configuration;
using QuickstartApp.Models;

namespace QuickstartApp.ViewModels
{
    public class RichMessageInboxPageViewModel : Com.Xamtastic.Patterns.SmallestMvvm.ViewModelBase
    {
        private INavigation _navigation;
        public IConfigurationManager ConfigurationManager { get; set; }
        public IRichMessagingManager RMManager { get; set; }
        public static bool ForceReload { get; set;}

        private string _mainText = "Rich Message Inbox - Modular";
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

        private ObservableCollection<RichMessageInboxListViewItemViewModel> _richMessages;
        public ObservableCollection<RichMessageInboxListViewItemViewModel> RichMessages
        {
            get
            {
                return _richMessages;
            }
            set
            {
                _richMessages = value;
                RaisePropertyChanged("RichMessages");
            }
        }

        private RichMessageInboxListViewItemViewModel _selectedItem;
        public RichMessageInboxListViewItemViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                MarkMessageAsRead();
                if (_selectedItem != null)
                {
                    if (!ShowItemMultiSelector)
                    {
                        List<DateTime> expiryMatix = new List<DateTime>();

                        var richMessageAvailabilityDays = ConfigurationManager.GetValue<string>("RichMessageAvailabilityDays");

                        var timeNow = DateTime.UtcNow;
                        var richMessageReceivedTime = this.SelectedItem.RichMessage.ReceivedTimestamp;

                        var richMessageExpiryTime = this.SelectedItem.RichMessage.ExpiryTimeStamp;
                        var richMessageAvailabilityExpiry = ((DateTime)richMessageReceivedTime).AddDays(Convert.ToInt32(richMessageAvailabilityDays));

                        if (richMessageExpiryTime != null)
                        {
                            expiryMatix.Add((DateTime)richMessageExpiryTime);
                        }

                        if (richMessageAvailabilityExpiry != null)
                        {
                            expiryMatix.Add((DateTime)richMessageAvailabilityExpiry);
                        }

                        expiryMatix.Sort((a, b) => a.CompareTo(b));

                        var earliest = expiryMatix.FirstOrDefault();

                        if (timeNow <= earliest)
                        {
                            var richMessage = SelectedItem.RichMessage;
                            this.SelectedItem = null;

                            _navigation.PushAsync(new AllIdiomsRichMessageInboxPopupPage(richMessage, false, DismissModalDialogCommand));
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(this.SelectedItem.RichMessage.ExpiredBody))
                            {
                                MessagingCenter.Send<AlertModel>(new AlertModel { Message = this.SelectedItem.RichMessage, Title = "Message Expired", Body = "This message has expired, please delete it or alternatively delete all your expired messages", }, "ExpiredMessageAlert");
                            }
                            else
                            {
                                var richMessage = SelectedItem.RichMessage;
                                this.SelectedItem = null;

                                _navigation.PushAsync(new AllIdiomsRichMessageInboxPopupPage(richMessage, true, DismissModalDialogCommand));
                            }
                        }
                    }
                    else
                    {
                        this.SelectedItem.ItemSelected = !this.SelectedItem.ItemSelected;
                        this.SelectedItem = null;
                        ReloadExistingData();
                    }
                }
                RaisePropertyChanged("SelectedItem");
            }
        }

        private void MarkMessageAsRead()
        {
            if (_selectedItem != null)
            {
                if (_selectedItem.RichMessage.ReadTimestamp == null)
                {
                    _selectedItem.RichMessage.ReadTimestamp = DateTime.UtcNow;
                    _selectedItem.RaisePropertyChanged("RichMessage");
                    this.RMManager.MarkMessageAsReadAsync(_selectedItem.RichMessage.MessageId);
                }
            }
        }

        private bool _showItemMultiSelector;
        public bool ShowItemMultiSelector
        {
            get
            {
                return _showItemMultiSelector;
            }
            set
            {
                _showItemMultiSelector = value;
                RaisePropertyChanged("ShowItemMultiSelector");
            }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged("IsRefreshing");
            }
        }

        private ICommand _ellipsesCommand;
        public ICommand EllipsesCommand
        {
            get
            {
                return _ellipsesCommand;
            }
            set
            {
                _ellipsesCommand = value;
                RaisePropertyChanged("EllipsesCommand");
            }
        }

        public ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }
            set
            {
                _refreshCommand = value;
                RaisePropertyChanged("RefreshCommand");
            }
        }

        public ICommand _forwardCommand;
        public ICommand ForwardCommand
        {
            get
            {
                return _forwardCommand;
            }
            set
            {
                _forwardCommand = value;
                RaisePropertyChanged("ForwardCommand");
            }
        }

        public ICommand _shareCommand;
        public ICommand ShareCommand
        {
            get
            {
                return _shareCommand;
            }
            set
            {
                _shareCommand = value;
                RaisePropertyChanged("ShareCommand");
            }
        }

        public ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
            }
            set
            {
                _deleteCommand = value;
                RaisePropertyChanged("DeleteCommand");
            }
        }

        public ICommand _deleteItemsCommand;
        public ICommand DeleteItemsCommand
        {
            get
            {
                return _deleteItemsCommand;
            }
            set
            {
                _deleteItemsCommand = value;
                RaisePropertyChanged("DeleteItemsCommand");
            }
        }

        public ICommand _toggleSelectorCommand;
        public ICommand ToggleSelectorCommand
        {
            get
            {
                return _toggleSelectorCommand;
            }
            set
            {
                _toggleSelectorCommand = value;
                RaisePropertyChanged("ToggleSelectorCommand");
            }
        }

        public ICommand _selectAllCommand;
        public ICommand SelectAllCommand
        {
            get
            {
                return _selectAllCommand;
            }
            set
            {
                _selectAllCommand = value;
                RaisePropertyChanged("SelectAllCommand");
            }
        }

        private string _toggleText = "Select";
        public string ToggleText
        {
            get
            {
                return _toggleText;
            }
            set
            {
                _toggleText = value;
                RaisePropertyChanged("ToggleText");
            }
        }

        private string _allText = string.Empty;
        public string AllText
        {
            get
            {
                return _allText;
            }
            set
            {
                _allText = value;
                RaisePropertyChanged("AllText");
            }
        }

        public ICommand _dismissModalDialogCommand;
        public ICommand DismissModalDialogCommand
        {
            get
            {
                return _dismissModalDialogCommand;
            }
            set
            {
                _dismissModalDialogCommand = value;
                RaisePropertyChanged("DismissModalDialogCommand");
            }
        }

        public RichMessageInboxPageViewModel()
        {
            this.ConfigurationManager = DonkyCore.Instance.GetService<IConfigurationManager>();
            this.RMManager = DonkyCore.Instance.GetService<IRichMessagingManager>();

            this.ToggleSelectorCommand = new Command<string>(async (key) =>
            {
                ShowItemMultiSelector = !ShowItemMultiSelector;

                if(ShowItemMultiSelector)
                {
                    ToggleText = "Cancel";
                    AllText = "All";
                    foreach(var message in RichMessages)
                    {
                        message.ItemSelected = false;
                    }
                }
                else
                {
                    ToggleText = "Select";
                    AllText = string.Empty;
                }
            });

            this.EllipsesCommand = new Command<object>(async(key) => 
            {
                foreach (var message in RichMessages)
                {
                    if(message.RichMessage.CanShare)
                    {
                        MessagingCenter.Send<ActionSheetModel>(new ActionSheetModel { RichMessageId = (Guid)key, Buttons = new string[1] { "Share" } }, "RichMessageInboxListViewItemActionSheet");
                    }
                    else
                    {
                        MessagingCenter.Send<ActionSheetModel>(new ActionSheetModel { RichMessageId = (Guid)key, Buttons = new string[0] { } }, "RichMessageInboxListViewItemActionSheet");
                    }
                }
            });

            this.SelectAllCommand = new Command(() =>
            {
                if (!ShowItemMultiSelector)
                {
                    AllText = string.Empty;
                }
                else
                {
                    var selection = false;

                    if (AllText.Equals("None"))
                    {
                        AllText = "All";
                        selection = false;
                    }
                    else
                    {
                        AllText = "None";
                        selection = true;
                    }

                    foreach (var message in RichMessages)
                    {
                        message.ItemSelected = selection;
                    }
                }
            });

            this.RefreshCommand = new Command<string>(async (key) =>
            {
                PopulateData();
            });

            this.ForwardCommand = new Command<object>(async (key) =>
            {
                Debug.WriteLine("Forward Command Invoked");
            });

            this.ShareCommand = new Command<object>(async (RichMessageInboxListViewItemViewModel) =>
            {
                Debug.WriteLine("Share Command Invoked");

                if (RichMessageInboxListViewItemViewModel != null)
                {
                    if (((RichMessageInboxListViewItemViewModel)RichMessageInboxListViewItemViewModel).RichMessage.CanShare)
                    {
                        var url = ((RichMessageInboxListViewItemViewModel)RichMessageInboxListViewItemViewModel).RichMessage.UrlToShare;

                        MessagingCenter.Send<string>(string.Format("Check out this message I received!! {0}", url), "ShareRichMessage");
                    }
                    else
                    {
                        MessagingCenter.Send<AlertModel>(new AlertModel { Title = "Unable to Share", Body = "Unfortunately this Rich Message has not been marked for sharing." }, "UnableToShareAlert");
                    }
                }
  
            });

            this.DeleteCommand = new Command<object>(async (key) =>
            {
                Debug.WriteLine("Delete Command Invoked");

                RichMessageInboxListViewItemViewModel itemToRemove = null;
                foreach (var item in RichMessages)
                {
                    if (item.RichMessage.MessageId.Equals(Guid.Parse(key.ToString())))
                    {
                        itemToRemove = item;
                    }
                }

                if (itemToRemove != null)
                {
                    RichMessages.Remove(itemToRemove);

                    await RMManager.DeleteMessagesAsync(Guid.Parse(key.ToString()));
                }
            });

            this.DeleteItemsCommand = new Command<object>(async (key) =>
            {
                var listToDelete = new List<RichMessageInboxListViewItemViewModel>();
                foreach (var item in RichMessages)
                {
                    if (item.ItemSelected)
                    {
                        listToDelete.Add(item);
                    }
                }

                foreach (var item in listToDelete)
                {
                    await RMManager.DeleteMessagesAsync(Guid.Parse(item.RichMessage.MessageId.ToString()));
                    RichMessages.Remove(item);
                }
                listToDelete = null;
            });

            this.DismissModalDialogCommand = new Command<object>(async (key) =>
            {
                _navigation.PopAsync(true);
            });

            MessagingCenter.Subscribe<DeleteModel>(this, "DeleteRichMessage", async (model) =>
            {
                if(model.DeleteAll)
                {
                    List<Guid> deleteQueueMessageIds = new List<Guid>();

                    foreach (var item in RichMessages)
                    {
                        if(DeleteStatus(item.RichMessage))
                        {
                            deleteQueueMessageIds.Add(item.RichMessage.MessageId);
                        }
                    }
                    await RMManager.DeleteMessagesAsync(deleteQueueMessageIds.ToArray());
                }
                else
                {
                    await RMManager.DeleteMessagesAsync(model.Message.MessageId);
                }
                PopulateData(); // Refresh the list without the deleted message/s.
            });

            MessagingCenter.Subscribe<ActionSheetModel>(this, "ShareRichMessageActionSheet", async (model) =>
            {
                RichMessage rm = null;
                foreach(var message in RichMessages)
                {
                    if(message.RichMessage.MessageId == model.RichMessageId)
                    {
                        rm = message.RichMessage;
                    }
                }

                this.ShareCommand.Execute(new RichMessageInboxListViewItemViewModel { RichMessage = rm });
            });

            MessagingCenter.Subscribe<ActionSheetModel>(this, "DeleteRichMessageActionSheet", async (model) =>
            {
                this.DeleteCommand.Execute(model.RichMessageId);
            });

            PopulateData();
        }

        private bool DeleteStatus(RichMessage message)
        {
            bool result = false;
            List<DateTime> expiryMatix = new List<DateTime>();

            var richMessageAvailabilityDays = ConfigurationManager.GetValue<string>("RichMessageAvailabilityDays");

            var timeNow = DateTime.UtcNow;
            var richMessageReceivedTime = message.ReceivedTimestamp;

            var richMessageExpiryTime = message.ExpiryTimeStamp;
            var richMessageAvailabilityExpiry = ((DateTime)richMessageReceivedTime).AddDays(Convert.ToInt32(richMessageAvailabilityDays));

            if (richMessageExpiryTime != null)
            {
                expiryMatix.Add((DateTime)richMessageExpiryTime);
            }

            if (richMessageAvailabilityExpiry != null)
            {
                expiryMatix.Add((DateTime)richMessageAvailabilityExpiry);
            }

            expiryMatix.Sort((a, b) => a.CompareTo(b));

            var earliest = expiryMatix.FirstOrDefault();

            if(timeNow < earliest)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        private async Task PopulateData()
        {
            if (RMManager != null)
            {
                var messages = await RMManager.GetAllAsync();

                if (messages != null)
                {
                    messages = messages.OrderByDescending(message => message.ReceivedTimestamp);

                    var rmvmCollection = new ObservableCollection<RichMessageInboxListViewItemViewModel>();

                    foreach (var item in messages)
                    {
                        rmvmCollection.Add(new RichMessageInboxListViewItemViewModel { RichMessage = item, ShowItemMultiSelector = false, EllipsesCommand = EllipsesCommand, ForwardCommand = ForwardCommand, ShareCommand = ShareCommand, DeleteCommand = DeleteCommand });
                    }

                    this.RichMessages = rmvmCollection;
                }
            }
            IsRefreshing = false;
        }

        private async Task ReloadExistingData()
        {
            var cache = this.RichMessages;
            this.RichMessages = null;
            this.RichMessages = cache;
        }


        internal void SetNavigationManager(INavigation navigation)
        {
            _navigation = navigation;
        }

        internal void OnAppearing()
        {
            if(ForceReload)
            {
                PopulateData();
                ForceReload = false;
            }
        }
    }
}
