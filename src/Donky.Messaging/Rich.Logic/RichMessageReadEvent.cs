// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     RichMessageReadEvent class.
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
	/// Local event indicating that messages have been read.
	/// </summary>
	public class RichMessageReadEvent : LocalEvent
	{
		public RichMessageReadEvent()
		{

		}

		public RichMessageReadEvent(Guid id)
		{
			MessageId = id;
		}

		/// <summary>
		/// The id of the message that has been read
		/// </summary>
		public Guid MessageId { get; set; }
	}
}

