using Com.Xamtastic.Patterns.SmallestMvvm;
using Donky.Messaging.Rich.Inbox.XamarinForms.ViewModels;
using Donky.Messaging.Rich.Logic;
using QuickstartApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace QuickstartApp.Pages
{
    [ViewModelType(typeof(RichMessageInboxPopupPageViewModel))]
    public partial class AllIdiomsRichMessageInboxPopupPage : PageBase
    {
        public AllIdiomsRichMessageInboxPopupPage(RichMessage richMessage, bool expired, ICommand dismissModalDialogCommand)
        {
            InitializeComponent();
            ((RichMessageInboxPopupPageViewModel)this.BindingContext).LoadNavigator(this.Navigation);
            ((RichMessageInboxPopupPageViewModel)this.BindingContext).SetParams(richMessage, expired, dismissModalDialogCommand);
        }
    }
}
