// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     DonkyPushUIAndroid class
//  Author:          Ben Moore
//  Created date:    21/05/2015
//  Copyright:       Donky Networks Ltd 2015
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Donky.Core;
using Donky.Core.Framework;
using Donky.Core.Registration;
using Donky.Core.Xamarin.Android;
using Donky.Messaging.Push.Logic;
using Donky.Messaging.Push.UI.XamarinForms;
using Android.OS;
using Donky.Messaging.Common;
using Android.Media;
using Android.Graphics;

namespace Donky.Messaging.Push.UI.Android
{
	/// <summary>
	/// Entry point for the Donky Android Push UI module.
	/// </summary>
	public static class DonkyPushUIAndroid
	{
		public static readonly ModuleDefinition Module = new ModuleDefinition(
			"DonkyXamarinAndroidPushUI", AssemblyHelper.GetAssemblyVersion(typeof(DonkyPushUIAndroid)).ToString());

		private static bool _isInitialised;
		private static readonly object Lock = new object();

		/// <summary>
		/// Initialises this module.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">DonkyPushUIAndroid is already initialised</exception>
		public static void Initialise(bool initialiseFormsPushUI = true)
		{
			lock (Lock)
			{
				if (_isInitialised)
				{
					throw new InvalidOperationException("DonkyPushUIAndroid is already initialised");
				}

				if (initialiseFormsPushUI)
				{
					DonkyPushUIXamarinForms.Initialise();
				}

				DonkyCore.Instance.RegisterModule(Module);
				DonkyCore.Instance.SubscribeToLocalEvent<SimplePushMessageReceivedEvent>(HandleSimplePushReceived);
				DonkyCore.Instance.SubscribeToLocalEvent<MessageReceivedEvent>(HandleMessageReceived);

				_isInitialised = true;
			}
		}

		private static void HandleSimplePushReceived(SimplePushMessageReceivedEvent messageEvent)
		{
			if (DonkyCore.Instance.GetService<IAppState>().IsOpen)
			{
				DonkyCore.Instance.PublishLocalEvent(new DisplaySimplePushAlertEvent
				{
					MessageReceivedEvent = messageEvent
				}, Module);
			}
			else
			{
				ShowNativeNotification(messageEvent.Message, messageEvent.NotificationId, messageEvent.PlatformButtonSet);
			}
		}

		private static void HandleMessageReceived(MessageReceivedEvent messageEvent)
		{
			if (DonkyCore.Instance.GetService<IAppState>().IsOpen)
			{
				DonkyCore.Instance.PublishLocalEvent(new DisplayBasicMessageAlertEvent
				{
					Message = messageEvent.Message,
					AlertText = messageEvent.AlertText,
					NotificationId = messageEvent.NotificationId
				}, Module);
			}
			else
			{
				ShowNativeNotification(messageEvent.Message, messageEvent.NotificationId, alertTextOverride: messageEvent.AlertText);
			}
		}


		private static void ShowNativeNotification(Common.Message message, string notificationId, ButtonSet buttonSet = null, string alertTextOverride = null)
		{
			var nativeNotificationId = new Random().Next();

			// Setup intent to launch application
			var context = Application.Context;

			// Build the notification
			var builder = new Notification.Builder(context)
				.SetContentTitle(message.SenderDisplayName)
				.SetSmallIcon(Resource.Drawable.donky_notification_small_icon_simple_push)
				.SetContentText(alertTextOverride ?? message.Body)
				.SetVibrate(new[] { 0L, 100L })
			    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

			var pushMessage = message as SimplePushMessage;
			if (pushMessage != null && buttonSet != null && buttonSet.ButtonSetActions.Any())
			{
				builder.SetAutoCancel(false);
				// Jelly Bean + above supports multiple actions
				if (buttonSet.ButtonSetActions.Count == 2
				    && Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
				{
					foreach (var action in buttonSet.ButtonSetActions)
					{
						builder.AddAction(0,
							action.Label,
							CreateIntentForAction(context, nativeNotificationId, 
								notificationId, action, pushMessage,
								buttonSet));
					}
				}
				else
				{
					builder.SetContentIntent(CreateIntentForAction(context, nativeNotificationId, 
						notificationId, buttonSet.ButtonSetActions[0], 
						pushMessage, buttonSet));
				}
			}
			else
			{
				var intent = CreateIntentForBasicPush(context, nativeNotificationId, notificationId);
				builder.SetContentIntent(intent)
					.SetAutoCancel(true);
			}

            // bug
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                builder.SetCategory(Notification.CategoryMessage)
                    .SetPriority((int) NotificationPriority.High)
                    .SetVisibility(NotificationVisibility.Public);

                // this is for a Heads-Up Notification (Out of App Notification)
                var push = new Intent();
                push.AddFlags(ActivityFlags.NewTask);
                push.SetClass(context, Java.Lang.Class.FromType(context.GetType()));
                var fullScreenPendingIntent = PendingIntent.GetActivity(context, 0,
                    push, PendingIntentFlags.CancelCurrent);
                builder
                    .SetContentText(alertTextOverride ?? message.Body)
                    .SetFullScreenIntent(fullScreenPendingIntent, true);

            }

			var notification = builder.Build();

			notification.LedARGB = Color.White;
			notification.Flags |= NotificationFlags.ShowLights;
			notification.LedOnMS = 200;
			notification.LedOffMS = 2000;

			var notificationManager = (NotificationManager) context.GetSystemService(Context.NotificationService);
			notificationManager.Notify(nativeNotificationId, notification);
		}

		private static PendingIntent CreateIntentForAction(Context context, int nativeNotificationId,
			string donkyNotificationId, ButtonSetAction action, SimplePushMessage message, ButtonSet buttonSet)
		{
			var serialiser = DonkyCore.Instance.GetService<IJsonSerialiser>();
			var intent = new Intent(context, typeof(PushActionIntentService));
			if (Build.VERSION.SdkInt >= BuildVersionCodes.HoneycombMr1)
			{
				intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
			}

			intent.PutExtra(DonkyAndroid.DonkyNotificationId, donkyNotificationId);
			intent.PutExtra(PushActionIntentService.NativeNotificationId, nativeNotificationId);
			intent.PutExtra(PushActionIntentService.ButtonActionData, action.Data);
			intent.PutExtra(PushActionIntentService.Message, serialiser.Serialise(message));
			intent.PutExtra(PushActionIntentService.ButtonSet, serialiser.Serialise(buttonSet));
			switch (action.ActionType)
			{
				case "Open":
					intent.SetAction(PushActionIntentService.OpenAction);
					intent.PutExtra(PushActionIntentService.UserAction, "Button" + (buttonSet.ButtonSetActions.IndexOf(action) + 1));
					break;

				case "DeepLink":
					intent.SetAction(PushActionIntentService.DeepLinkAction);
					intent.PutExtra(PushActionIntentService.UserAction, "Button" + (buttonSet.ButtonSetActions.IndexOf(action) + 1));
					break;

				case "Dismiss":
					intent.SetAction(PushActionIntentService.CancelAction);
					intent.PutExtra(PushActionIntentService.UserAction, "Dismissed");
					break;
			}
			
			var pendingIndentId = new Random().Next(Int32.MaxValue);
			return PendingIntent.GetService(context, pendingIndentId, intent, PendingIntentFlags.UpdateCurrent);
		}

		private static PendingIntent CreateIntentForBasicPush(Context context, int nativeNotificationId, string donkyNotificationId)
		{
			var packageName = context.PackageName;
			var packageManager = context.PackageManager;

			var intent = packageManager.GetLaunchIntentForPackage(packageName);

			if (Build.VERSION.SdkInt >= BuildVersionCodes.HoneycombMr1)
			{
				intent.AddFlags(ActivityFlags.IncludeStoppedPackages);
			}

			intent.PutExtra(DonkyAndroid.DonkyNotificationId, donkyNotificationId);

			return PendingIntent.GetActivity(context, nativeNotificationId, intent, PendingIntentFlags.UpdateCurrent);
		}
	}
}