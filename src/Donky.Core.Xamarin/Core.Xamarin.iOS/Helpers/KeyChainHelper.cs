using System;
using Donky.Core.Framework.Logging;
using Foundation;
using Security;

namespace Donky.Core.Xamarin.iOS
{
	public static class KeyChainHelper
	{
		public static string GetValue(string key)
		{
			var record = GetRecordForKey(key);
			if (record == null)
			{
				return null;
			}

			return record.ValueData.ToString();
		}

		public static void SetValue(string key, string value)
		{
			var record = GetRecordForKey(key);
			SecStatusCode result;
			if (record != null)
			{
				// We can't update the item, keep getting "Param" status code back.  Delete and re-add.
				// Need to create a new SecRecord for the delete, can't use the actual value
				var toRemove = new SecRecord(SecKind.GenericPassword)
				{
					Service = key,
					Account = key
				};

				result = SecKeyChain.Remove(toRemove);
				if (result != SecStatusCode.Success)
				{
					Logger.Instance.LogError("Could not delete keychain entry for key {0}.  Result was {1}",
											 key, result.ToString());
				}
			}

			var required = new SecRecord(SecKind.GenericPassword)
			{
				Service = key,
				Account = key,
				ValueData = NSData.FromString(value),
				Accessible = SecAccessible.AlwaysThisDeviceOnly
			};

			result = SecKeyChain.Add(required);

			if (result != SecStatusCode.Success)
			{
				Logger.Instance.LogError("Could not add keychain entry for key {0}, value {1}.  Result was {2}",
										 key, value, result.ToString());
			}
		}

		private static SecRecord GetRecordForKey(string key)
		{
			var query = new SecRecord(SecKind.GenericPassword)
			{
				Service = key,
				Account = key
			};

			SecStatusCode queryResult;
			var match = SecKeyChain.QueryAsRecord(query, out queryResult);
			if (queryResult != SecStatusCode.Success)
			{
				return null;
			}

			return match;
		}
	}
}

