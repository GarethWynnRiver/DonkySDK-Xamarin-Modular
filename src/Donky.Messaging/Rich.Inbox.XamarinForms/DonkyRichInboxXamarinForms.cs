// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     DonkyRichInboxXamarinForms module;
//  Author:          Ben Moore
//  Created date:    14/05/2015
//  Copyright:       Donky Networks Ltd 2015
// ///////////////////////////////////////////////////////////////////////////////////////////
using Donky.Core;
using Donky.Core.Framework;
using Donky.Core.Registration;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	/// <summary>
	/// Module class for Common Messaging functionality
	/// </summary>
	public static class DonkyRichInboxXamarinForms
	{
		public static readonly ModuleDefinition Module = new ModuleDefinition(
			"DonkyRichInboxXamarinForms", AssemblyHelper.GetAssemblyVersion(typeof(DonkyRichInboxXamarinForms)).ToString());

		/// <summary>
		/// Initialise the Common Messaging module.
		/// </summary>
		public static void Initialise()
		{
			DonkyCore.Instance.RegisterModule(Module);
		}
	}
}