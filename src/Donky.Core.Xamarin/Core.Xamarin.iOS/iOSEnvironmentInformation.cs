// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     iSOEnvironmentInformation class.
//  Author:          Ben Moore
//  Created date:    06/05/2015
//  Copyright:       Donky Networks Ltd 2015
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using Donky.Core.Framework;
using Foundation;
using Security;
using UIKit;

namespace Donky.Core.Xamarin.iOS
{
	/// <summary>
	/// iOS environment info
	/// </summary>
	internal class iOSEnvironmentInformation : IEnvironmentInformation
	{
		private const string DONKY_DEVICE_ID = "DonkyDeviceId";
		private const string DONKY_DEVICE_ID_2 = "DonkyDeviceId2"; // v1 had flawed access patterns, this will allow us to upgrade
		private static readonly object _deviceIdLock = new object();

		public iOSEnvironmentInformation()
		{
		}

		public string OperatingSystem
		{
			get { return "iOS"; }
		}

		public string OperatingSystemVersion
		{
			get { return UIDevice.CurrentDevice.SystemVersion; }
		}

		public string DeviceId
		{
			get
			{
				lock(_deviceIdLock)
				{
					string deviceId = KeyChainHelper.GetValue(DONKY_DEVICE_ID_2);

					if (String.IsNullOrEmpty(deviceId))
					{
						// Can we get an "old" value?  if not generate new 
						deviceId = GetOldDeviceId() ?? Guid.NewGuid().ToString();
						KeyChainHelper.SetValue(DONKY_DEVICE_ID_2, deviceId);
					}

					return deviceId;
				}
			}
		}

		public string Model
		{
			get { return UIDevice.CurrentDevice.Model; }
		}

		public string AppIdentifier
		{
			get { return NSBundle.MainBundle.BundleIdentifier; }
		}

		private string GetOldDeviceId()
		{
			var record = new SecRecord(SecKind.GenericPassword)
			{
				Service = DONKY_DEVICE_ID
			};

			var match = SecKeyChain.QueryAsData(record);
			if (match != null)
			{
				return match.ToString();
			}

			return null;
		}
	}
}