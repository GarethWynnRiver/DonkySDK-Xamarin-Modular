using System;
namespace Donky.Core
{
	public interface IDeviceInteraction
	{
		/// <summary>
		/// Opens the specific URI on the device
		/// </summary>
		/// <remarks>The URI may correspond to a deep / universal link to another app, or an HTTP URI to open in a browser</remarks>
		/// <param name="uri">The URI to open.</param>
		void OpenUri(string uri);
	}
}

