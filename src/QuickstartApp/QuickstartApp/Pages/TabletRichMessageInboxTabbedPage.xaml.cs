using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuickstartApp.Pages
{
    public partial class TabletRichMessageInboxTabbedPage : TabbedPage
    {
        public TabletRichMessageInboxTabbedPage()
        {
            InitializeComponent();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (this.CurrentPage != null)
            {
                this.Title = this.CurrentPage.Title;
            }
            else
            {
                this.Title = string.Empty;
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            Debug.WriteLine("Property Name: " + propertyName);
        }
    }
}
