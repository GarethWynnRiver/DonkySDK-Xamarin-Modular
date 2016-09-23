using System;
using Donky.Core.Framework.Extensions;
using Donky.Messaging.Rich.Logic;
using Xamarin.Forms;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	public partial class RichMessageView : ContentPage
	{
		private RichMessage _message;
		private readonly WebView _webView;


		public RichMessageView()
		{
			InitializeComponent();
			_webView = new WebView();
			Content = _webView;
		}

		public RichMessage Message
		{
			get { return _message; }
			set
			{
				_message = value;
				UpdateView();
			}
		}

		private void UpdateView()
		{
			var message = _message;
			if (!message.ReadTimestamp.HasValue)
			{
				DonkyRichLogic.Instance.MarkMessageAsReadAsync(_message.MessageId).ExecuteInBackground();
			}

			var body = !String.IsNullOrEmpty(message.ExpiredBody) 
			                  && message.ExpiryTimeStamp.HasValue 
			                  && message.ExpiryTimeStamp <= DateTime.UtcNow
			                  ? message.ExpiredBody : message.Body;


			Device.BeginInvokeOnMainThread(() =>
			{
				_webView.Source = new HtmlWebViewSource
				{
					Html = body
				};
			});
		}
	}
}

