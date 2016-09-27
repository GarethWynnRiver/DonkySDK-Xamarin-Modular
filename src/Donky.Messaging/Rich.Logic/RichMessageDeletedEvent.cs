// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     RichMessageDeletedEvent class.
//  Author:          Ben Moore
//  Created date:    23/08/2016
//  Copyright:       Donky Networks Ltd 2016
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using Donky.Core.Events;
using Donky.Core.Registration;

namespace Donky.Messaging.Rich.Logic
{
	/// <summary>
	/// Local event indicating that messages have been deleted.
	/// </summary>
	public class RichMessageDeletedEvent : LocalEvent
	{
		public RichMessageDeletedEvent()
		{

		}

		public RichMessageDeletedEvent(Guid[] messageIds, ModuleDefinition module)
		{
			DeletedMessageIds = messageIds;
			Publisher = module;
		}

		/// <summary>
		/// The ids of the messages that have been deleted
		/// </summary>
		public Guid[] DeletedMessageIds { get; set; }
	}
}

