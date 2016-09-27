// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     DisplayBasicMessageAlertEvent class
//  Author:          Ben Moore
//  Created date:    08/09/2016
//  Copyright:       Donky Networks Ltd 2016
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using Donky.Core.Events;
using Donky.Messaging.Common;

namespace Donky.Messaging.Push.UI.XamarinForms
{
	/// <summary>
	/// Display an alert for a generic message
	/// </summary>
	public class DisplayBasicMessageAlertEvent : LocalEvent
	{
		public string NotificationId { get; set; }

		public Message Message { get; set; }

		public string AlertText { get; set; }
	}
}