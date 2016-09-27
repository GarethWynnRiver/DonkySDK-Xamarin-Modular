using System;
using UIKit;
using Foundation;
using Donky.Core.Framework.Logging;

namespace Donky.Core.Xamarin.iOS
{
	internal class iOSDeviceInteraction : IDeviceInteraction
	{
		public void OpenUri(string uri)
		{
			UIApplication.SharedApplication.BeginInvokeOnMainThread(() =>
			{
				try
				{
					var url = NSUrl.FromString(uri);
					UIApplication.SharedApplication.OpenUrl(url);
				}
				catch (Exception ex)
				{
					Logger.Instance.LogError(ex, "Failed to open uri {0}", uri);
				}
			});
		}
	}
}

