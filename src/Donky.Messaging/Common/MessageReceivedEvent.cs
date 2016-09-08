// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     MessageReceivedEvent class.
//  Author:          Ben Moore
//  Created date:    04/09/2016
//  Copyright:       Donky Networks Ltd 2016
// ///////////////////////////////////////////////////////////////////////////////////////////

using Donky.Core.Events;

namespace Donky.Messaging.Common
{
	/// <summary>
	/// Generic MessageReceivedEvent used to indicate that a message that is not a simple push needs an alert
	/// </summary>
	public class MessageReceivedEvent : LocalEvent
	{
		public string NotificationId { get; set; }
		public Message Message { get; set; }
		public string AlertText { get; set; }
	}
}

