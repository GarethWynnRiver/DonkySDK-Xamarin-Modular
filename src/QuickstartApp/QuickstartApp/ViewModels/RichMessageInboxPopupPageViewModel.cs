using Com.Xamtastic.Patterns.SmallestMvvm;
using Donky.Core;
using Donky.Messaging.Rich.Inbox.XamarinForms.ViewModels;
using Donky.Messaging.Rich.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickstartApp.ViewModels
{
    public class RichMessageInboxPopupPageViewModel : Com.Xamtastic.Patterns.SmallestMvvm.ViewModelBase
    {
        public IRichMessagingManager RMManager { get; set; }

        private INavigation _navigation;
        private RichMessage _richMessageCache;

        private string _deleteText = "Delete";

        public string DeleteText
        {
            get
            {
                return _deleteText;
            }
            set
            {
                _deleteText = value;
                RaisePropertyChanged("DeleteText");
            }
        }

        private ICommand _deleteCommand;
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


        private ICommand _dismissModalDialogCommand;
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

        private string _HTML5;
        public string HTML5
        {
            get
            {
                return _HTML5;
            }
            set
            {
                _HTML5 = value;
                RaisePropertyChanged("HTML5");
            }
        }

        public RichMessageInboxPopupPageViewModel()
        {
            this.RMManager = DonkyCore.Instance.GetService<IRichMessagingManager>();

            this.DeleteCommand = new Command<object>(async (key) =>
            {
                Debug.WriteLine("Delete Command Invoked");

                await RMManager.DeleteMessagesAsync(_richMessageCache.MessageId);

                RichMessageInboxPageViewModel.ForceReload = true;

                await _navigation.PopAsync();
            });
        }

        internal void SetParams(RichMessage richMessage, bool expired, ICommand dismissModalDialogCommand)
        {
            _richMessageCache = richMessage;

            this.DismissModalDialogCommand = dismissModalDialogCommand;

            if (!expired)
            {
                HTML5 = richMessage.Body;
            }
            else
            {
                HTML5 = richMessage.ExpiredBody;
            }
        }

        internal void LoadNavigator(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
}
