using System;
using System.Threading.Tasks;
using Donky.Core.Framework.Extensions;
using Donky.Messaging.Rich.Logic;
using Xamarin.Forms;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	public partial class RichMessageView : ContentPage
	{
		private Guid _messageId;
		private readonly WebView _webView;


		public RichMessageView()
		{
			InitializeComponent();
			_webView = new WebView();
			Content = _webView;
		}

		public Guid MessageId
		{
			get { return _messageId; }
			set
			{
				_messageId = value;
				LoadMessageAsync().ExecuteInBackground();;
			}
		}

		private async Task LoadMessageAsync()
		{
			var message = await DonkyRichLogic.Instance.GetByIdAsync(_messageId);
			if (!message.ReadTimestamp.HasValue)
			{
				DonkyRichLogic.Instance.MarkMessageAsReadAsync(_messageId).ExecuteInBackground();
			}

			var body = !String.IsNullOrEmpty(message.ExpiredBody) 
			                  && message.ExpiryTimeStamp.HasValue 
			                  && message.ExpiryTimeStamp <= DateTime.UtcNow
			                  ? message.ExpiredBody : message.Body;
			
			_webView.Source = new HtmlWebViewSource
			{
				Html = body
			};
		}
	}
}

