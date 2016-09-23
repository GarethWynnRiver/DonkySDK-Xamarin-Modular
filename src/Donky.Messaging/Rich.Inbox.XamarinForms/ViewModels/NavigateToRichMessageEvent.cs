using Donky.Core.Events;
using Donky.Messaging.Rich.Logic;

namespace Donky.Messaging.Rich.Inbox.XamarinForms
{
	public class NavigateToRichMessageEvent : LocalEvent
	{
		public NavigateToRichMessageEvent(RichMessage message)
		{
			Message = message;
		}

		public RichMessage Message { get; set; }
	}
}