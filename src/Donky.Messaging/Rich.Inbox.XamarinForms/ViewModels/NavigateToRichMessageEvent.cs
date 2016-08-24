using System;
using Donky.Core.Events;
using Donky.Core.Registration;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	public class NavigateToRichMessageEvent : LocalEvent
	{
		public NavigateToRichMessageEvent(Guid messageId)
		{
			MessageId = messageId;
		}

		public Guid MessageId { get; set; }
	}
}