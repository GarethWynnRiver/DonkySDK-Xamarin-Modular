using QuickstartApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickstartApp.Views
{
    public partial class RichInboxContentView : ContentView
    {
        public RichInboxContentView()
        {
            InitializeComponent();

            try
            {
                this.BindingContext = new RichMessageInboxPageViewModel();
            }
            catch(Exception ex)
            {

            }
        }

        
    }
}
