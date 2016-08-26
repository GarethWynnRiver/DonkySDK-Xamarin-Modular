using Android.App;
using Android.Content;
using Android.Net;
using Donky.Core.Framework.Logging;

namespace Donky.Core.Xamarin.Android
{
	internal class AndroidDeviceInteraction : IDeviceInteraction
	{
		public void OpenUri(string uri)
		{
			try
			{
				var toOpen = Uri.Parse(uri);
				var intent = new Intent(Intent.ActionView, toOpen);
				intent.SetFlags(ActivityFlags.NewTask);
				Application.Context.StartActivity(intent);
			}
			catch (System.Exception ex)
			{
				Logger.Instance.LogError(ex, "Failed to open uri {0}", uri);
			}
		}
	}
}

