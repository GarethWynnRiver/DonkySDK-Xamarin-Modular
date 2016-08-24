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
	public class RichInboxItemViewModel : ViewModelBase
	{
		private static Lazy<IAssetHelper> AssetHelper = new Lazy<IAssetHelper>(() => DonkyCore.Instance.GetService<IAssetHelper>(),
																				System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

		public RichInboxItemViewModel()
		{
			DeleteCommand = new Command(item =>
			{
				var model = item as RichInboxItemViewModel;
				if (model != null)
				{
					DonkyRichLogic.Instance.DeleteMessagesAsync(model.Message.MessageId).ExecuteInBackground();
				}
			});

			ViewCommand = new Command(item =>
			{
				var model = item as RichInboxItemViewModel;
				if (model != null)
				{
					DonkyCore.Instance.PublishLocalEvent(new NavigateToRichMessageEvent(model.Message.MessageId), DonkyRichInboxXamarinForms.Module);
				}
			});
		
			ShareCommand = new Command(item =>
			{
				var model = item as RichInboxItemViewModel;
				// TODO: Invoke sharing
			}, item =>
			{
				var model = item as RichInboxItemViewModel;
				return model != null && model.Message.CanShare;
			});
		}

		public RichMessage Message { get; set; }

		public string AvatarUrl
		{
			get
			{
				if (String.IsNullOrEmpty(Message?.AvatarAssetId))
				{
					return null;
				}
				    
				return AssetHelper.Value.CreateUriForAsset(Message.AvatarAssetId);
			}
		}

		public bool IsRead
		{
			get
			{
				return Message.ReadTimestamp.HasValue;
			}
		}

		public ICommand DeleteCommand { get; private set; }

		public ICommand ViewCommand { get; private set; }

		public ICommand ShareCommand { get; private set; }
	}
}
