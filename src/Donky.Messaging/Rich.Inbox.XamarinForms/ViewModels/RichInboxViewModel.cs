using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Donky.Core;
using Donky.Core.Assets;
using Donky.Core.Framework.Extensions;
using Donky.Messaging.Rich.Inbox.XamarinForms.ViewModels;
using Donky.Messaging.Rich.Logic;
using Xamarin.Forms;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	public class RichInboxViewModel : ViewModelBase
	{
		private ObservableCollection<RichInboxItemViewModel> _items;

		public RichInboxViewModel()
		{
			DonkyCore.Instance.SubscribeToLocalEvent<RichMessageReceivedEvent>(e => RefreshAsync().ExecuteInBackground());
			DonkyCore.Instance.SubscribeToLocalEvent<RichMessageDeletedEvent>(e => RefreshAsync().ExecuteInBackground());
			DonkyCore.Instance.SubscribeToLocalEvent<RichMessageReadEvent>(e => RefreshAsync().ExecuteInBackground());
		}

		public ObservableCollection<RichInboxItemViewModel> Items
		{
			get { return _items; }
			set {
				_items = value;
				RaisePropertyChanged();
			}
		}

		public async Task RefreshAsync()
		{
			var data = await DonkyRichLogic.Instance.GetAllAsync();
			var orderedData = data.OrderByDescending(m => m.ReceivedTimestamp);
			var models = orderedData.Select(x => new RichInboxItemViewModel
			{
				Message = x
			});

			Device.BeginInvokeOnMainThread(() =>
			{
				Items = new ObservableCollection<RichInboxItemViewModel>(models);
			});
		}
	}
}

