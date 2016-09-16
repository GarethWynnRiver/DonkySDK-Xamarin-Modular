// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     RichMessagingManager class.
//  Author:          Ben Moore
//  Created date:    06/07/2015
//  Copyright:       Donky Networks Ltd 2015
// ///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Donky.Core.Framework;
using Donky.Core.Framework.Events;
using Donky.Core.Framework.Extensions;
using Donky.Core.Notifications;
using Donky.Messaging.Common;
using Donky.Messaging.Rich.Logic.Data;
using Donky.Core.Configuration;
using Donky.Core.Framework.Logging;
using Donky.Core;

namespace Donky.Messaging.Rich.Logic
{
	internal class RichMessagingManager : IRichMessagingManager
	{
        private readonly IConfigurationManager _configurationManager;
		private readonly IRichDataContext _context;
		private readonly ICommonMessagingManager _commonMessagingManager;
		private readonly IJsonSerialiser _serialiser;
		private readonly IEventBus _eventBus;
		private readonly INotificationManager _notificationManager;
		private const int RichMessageAvailabilityDaysDefault = 30;

        public RichMessagingManager(IRichDataContext context, ICommonMessagingManager commonMessagingManager, IJsonSerialiser serialiser, IEventBus eventBus, IConfigurationManager configurationManager, INotificationManager notificationManager)
		{
			_context = context;
			_commonMessagingManager = commonMessagingManager;
			_serialiser = serialiser;
			_eventBus = eventBus;
            _configurationManager = configurationManager;
			_notificationManager = notificationManager;
		}

		public async Task<IEnumerable<RichMessage>> GetAllAsync()
		{
			var data = await _context.RichMessages.GetAllAsync();
			return data.Select(d => d.Message);
		}

		public async Task<IEnumerable<RichMessage>> GetAllUnreadAsync()
		{
			var data = await _context.RichMessages.GetAllAsync(
				d => !d.Message.ReadTimestamp.HasValue);
			return data.Select(d => d.Message);
		}

		public async Task<RichMessage> GetByIdAsync(Guid messageId)
		{
			var data = await _context.RichMessages.GetAsync(messageId);
			return data == null
				? null
				: data.Message;
		}

		public async Task MarkMessageAsReadAsync(Guid messageId)
		{
			await MarkMessageAsReadInternalAsync(messageId);
		}

		public async Task DeleteMessagesAsync(params Guid[] messageIds)
		{
			await DeleteMessagesInternalAsync(true, messageIds);
		}

		public async Task DeleteAllMessagesAsync()
		{
			Logger.Instance.LogInformation("Deleting ALL Rich Messages");
			var data = await _context.RichMessages.GetAllAsync();
			var toDelete = data.Select(m => m.Message.MessageId).ToArray();
			await DeleteMessagesAsync(toDelete);
		}

		public async Task HandleRichMessageAsync(ServerNotification notification)
		{
			var message = _serialiser.Deserialise<RichMessage>(notification.Data.ToString());
			message.ReceivedTimestamp = DateTime.UtcNow;
			var expired = message.ExpiryTimeStamp.HasValue && message.ExpiryTimeStamp <= DateTime.UtcNow;

			if (!expired)
			{
				var data = new RichMessageData
				{
					Id = message.MessageId,
					Message = message
				};

				await _context.RichMessages.AddOrUpdateAsync(data);
				await _context.SaveChangesAsync();

				_eventBus.PublishAsync(new RichMessageReceivedEvent
				{
					Message = message,
					NotificationId = notification.NotificationId,
					Publisher = DonkyRichLogic.Module
				}).ExecuteInBackground();
				// Also publish a more generic event that the push UI modules can understand if installed to render alerts
				_eventBus.PublishAsync(new MessageReceivedEvent
				{
					Message = message,
					AlertText = message.Description,
					NotificationId = notification.NotificationId,
					Publisher = DonkyRichLogic.Module
				}).ExecuteInBackground();
			}

			await _commonMessagingManager.NotifyMessageReceivedAsync(message, notification);
		}

		public async Task DeleteExpiredRichMessagesAsync()
        {
            var richMessageAvailabilityDays = _configurationManager.GetValue<int>("RichMessageAvailabilityDays");
			if (richMessageAvailabilityDays <= 0)
			{
				richMessageAvailabilityDays = RichMessageAvailabilityDaysDefault;
			}

            var timeNow = DateTime.UtcNow;

            var messages = await GetAllAsync();

            List<Guid> deletionQueue = new List<Guid>();

			foreach(var message in messages.Where(m => m.ReceivedTimestamp.HasValue))
            {
				var receivedTime = message.ReceivedTimestamp.Value;
				var expiryTime = message.ExpiryTimeStamp ?? receivedTime.AddDays(richMessageAvailabilityDays);

				if(expiryTime <= timeNow)
                {
                    deletionQueue.Add(message.MessageId);
					Logger.Instance.LogInformation("Removing expired message {0} with description {1}", message.MessageId, message.Description);
                }
            }

            if(deletionQueue.Count > 0)
            {
                await DeleteMessagesAsync(deletionQueue.ToArray());
            }
        }

		public Task HandleSyncMessageReadAsync(ServerNotification notification)
		{
			var messageId = Guid.Parse(notification.Data.Value<string>("messageId"));
			return MarkMessageAsReadInternalAsync(messageId, false);
		}

		public Task HandleSyncMessageDeletedAsync(ServerNotification notification)
		{
			var messageId = Guid.Parse(notification.Data.Value<string>("messageId"));
			return DeleteMessagesInternalAsync(false, messageId);
		}

		private async Task DeleteMessagesInternalAsync(bool sendNotification, params Guid[] messageIds)
		{
			foreach (var id in messageIds)
			{
				await _context.RichMessages.DeleteAsync(id);
				if (sendNotification)
				{
					await _notificationManager.QueueClientNotificationAsync(new ClientNotification
					{
						{"type", "MessageDeleted"},
						{"messageId", id}
					});
				}
				Logger.Instance.LogInformation("Deleting rich message {0}", id);
			}

			_eventBus.PublishAsync(new RichMessageDeletedEvent(messageIds, DonkyRichLogic.Module))
					 .ExecuteInBackground();

			await _context.SaveChangesAsync();

			if (sendNotification)
			{
				await _notificationManager.SynchroniseAsync();
			}
		}

		private async Task MarkMessageAsReadInternalAsync(Guid messageId, bool sendNotification = true)
		{
			var data = await _context.RichMessages.GetAsync(messageId);
			if (data != null && !data.Message.ReadTimestamp.HasValue)
			{
				data.Message.ReadTimestamp = DateTime.UtcNow;
				if (sendNotification)
				{
					await _commonMessagingManager.NotifyMessageReadAsync(data.Message);
				}
				await _context.RichMessages.AddOrUpdateAsync(data);
				await _context.SaveChangesAsync();

				_eventBus.PublishAsync(new RichMessageReadEvent(data.Message.MessageId)
				{
					Publisher = DonkyRichLogic.Module
				}).ExecuteInBackground();
			}
		}
	}
}