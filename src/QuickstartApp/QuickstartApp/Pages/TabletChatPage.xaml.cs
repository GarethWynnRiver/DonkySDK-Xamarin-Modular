using Com.Xamtastic.Patterns.SmallestMvvm;
using QuickstartApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickstartApp.Pages
{
    [ViewModelType(typeof(SendPageViewModel))]
    public partial class TabletChatPage : PageBase
    {
        public TabletChatPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
        }
    }
}
