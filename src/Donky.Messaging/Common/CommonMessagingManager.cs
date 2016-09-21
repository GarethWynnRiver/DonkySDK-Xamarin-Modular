// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     CommonMessagingManager class.
//  Author:          Ben Moore
//  Created date:    14/05/2015
//  Copyright:       Donky Networks Ltd 2015
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Threading.Tasks;
using Donky.Core;
using Donky.Core.Framework.Extensions;
using Donky.Core.Notifications;

namespace Donky.Messaging.Common
{
	/// <summary>
	/// Implementation of common messaging logic.
	/// </summary>
	internal class CommonMessagingManager : ICommonMessagingManager
	{
		private readonly INotificationManager _notificationManager;
		private readonly IAppState _appState;

		public CommonMessagingManager(INotificationManager notificationManager, IAppState appState)
		{
			_notificationManager = notificationManager;
			_appState = appState;
		}

		public async Task NotifyMessageReceivedAsync(Message message, ServerNotification serverNotification)
		{
			var notification = CreateMessageReceivedNotification(message, serverNotification);
			await _notificationManager.QueueClientNotificationAsync(notification);
			if (_appState.IsOpen)
			{
				_notificationManager.SynchroniseAsync().ExecuteInBackground();
			}
		}

		public async Task NotifyMessageReadAsync(Message message)
		{
			var notification = CreateMessageReadNotification(message);
			await _notificationManager.QueueClientNotificationAsync(notification);
			if (_appState.IsOpen)
			{
				_notificationManager.SynchroniseAsync().ExecuteInBackground();
			}
		}

		private ClientNotification CreateMessageReadNotification(Message message)
		{
			var notification = new ClientNotification
			{
				{"type", "MessageRead"},
				{"senderInternalUserId", message.SenderInternalUserId},
				{"messageId", message.MessageId},
				{"senderMessageId", message.SenderMessageId},
				{"messageType", message.MessageType},
				{"messageScope", message.MessageScope},
				{"sentTimestamp", message.SentTimestamp},
				{"contextItems", message.ContextItems},
				{"timeToReadSeconds", (int)CalculatedTimeToRead(message).TotalSeconds}
			};

			return notification;
		}

		private TimeSpan CalculatedTimeToRead(Message message)
		{
			if(!(message.ReceivedTimestamp.HasValue && message.ReadTimestamp.HasValue))
			{
				return TimeSpan.Zero;
			}

			return message.ReadTimestamp.Value - message.ReceivedTimestamp.Value;
		}

		private ClientNotificationWithAcknowledgement CreateMessageReceivedNotification(Message message, ServerNotification serverNotification)
		{
			var notification = new ClientNotificationWithAcknowledgement
			{
				{"type", "MessageReceived"},
				{"senderInternalUserId", message.SenderInternalUserId},
				{"messageId", message.MessageId},
				{"senderMessageId", message.SenderMessageId},
				{"receivedExpired", message.ExpiryTimeStamp.HasValue && message.ExpiryTimeStamp <= DateTime.UtcNow},
				{"messageType", message.MessageType},
				{"messageScope", message.MessageScope},
				{"sentTimestamp", message.SentTimestamp},
				{"contextItems", message.ContextItems},
			};

			notification.AcknowledgementDetail = new ClientNotificationAcknowledgement
			{
				Result = NotificationResult.Delivered,
				ServerNotificationId = serverNotification.NotificationId,
				SentTime = serverNotification.CreatedOn,
				Type = serverNotification.Type
			};

			return notification;
		}
	}
}