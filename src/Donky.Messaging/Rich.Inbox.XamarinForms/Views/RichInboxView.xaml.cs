using System;
using System.Collections.Generic;
using System.ComponentModel;
using Donky.Core;
using Donky.Core.Framework.Extensions;
using Xamarin.Forms;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	public partial class RichInboxView : ContentView
	{
		public RichInboxView()
		{
			InitializeComponent();
		}

		public RichInboxViewModel ViewModel
		{
			get { return BindingContext as RichInboxViewModel; }
		}

		void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
		}


		void OnTap(object sender, ItemTappedEventArgs e)
		{
			var model = e.Item as RichInboxItemViewModel;
			model?.ViewCommand.Execute(model);
		}

		void OnRefresh(object sender, EventArgs e)
		{
			var list = (ListView)sender;
			DonkyCore.Instance.NotificationController.SynchroniseAsync()
			         .ContinueWith(x => ViewModel.RefreshAsync())
					 .ContinueWith(x => Device.BeginInvokeOnMainThread(() =>
						{
							list.IsRefreshing = false;
						}))
					 .ExecuteInBackground();
		}
	}
}

