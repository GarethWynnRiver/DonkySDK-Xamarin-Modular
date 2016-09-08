// ///////////////////////////////////////////////////////////////////////////////////////////
//  Description:     DonkyPushUIXamarinForms class.
//  Author:          Ben Moore
//  Created date:    31/05/2015
//  Copyright:       Donky Networks Ltd 2015
// ///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Linq;
using Donky.Core;
using Donky.Core.Assets;
using Donky.Core.Framework;
using Donky.Core.Framework.Extensions;
using Donky.Core.Registration;
using Donky.Core.Xamarin.Forms.Alerts;
using Donky.Messaging.Common;
using Donky.Messaging.Push.Logic;
using Xamarin.Forms;

namespace Donky.Messaging.Push.UI.XamarinForms
{
	/// <summary>
	/// Entry point for the Donky Push UI for Xamarin Forms
	/// </summary>
	public static class DonkyPushUIXamarinForms
	{
		public static readonly ModuleDefinition Module = new ModuleDefinition(
			"DonkyXamarinFormsPushUI", AssemblyHelper.GetAssemblyVersion(typeof(DonkyPushUIXamarinForms)).ToString());

		private static bool _isInitialised;
		private static readonly object Lock = new object();

		/// <summary>
		/// Initialises this module.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">DonkyXamarinFormsPushUI is already initialised</exception>
		public static void Initialise()
		{
			lock (Lock)
			{
				if (_isInitialised)
				{
					throw new InvalidOperationException("DonkyXamarinFormsPushUI is already initialised");
				}

				DonkyCore.Instance.RegisterModule(Module);

				DonkyCore.Instance.SubscribeToLocalEvent<DisplaySimplePushAlertEvent>(HandleDisplaySimplePushAlert);
				DonkyCore.Instance.SubscribeToLocalEvent<DisplayBasicMessageAlertEvent>(HandleDisplayBasicMessageAlert);

				_isInitialised = true;
			}
		}

		private static void HandleDisplaySimplePushAlert(DisplaySimplePushAlertEvent alertEvent)
		{
			var messageEvent = alertEvent.MessageReceivedEvent;
			Message message = messageEvent.Message;
			var autoDismiss = true;
			SimplePushAlertView alertView;
			DisplayAlertEvent displayAlertEvent;
			CreateBasicAlertView(message, out alertView, out displayAlertEvent);

			if (messageEvent.PlatformButtonSet != null)
			{
				var manager = DonkyCore.Instance.GetService<IPushMessagingManager>();
				var pushMessage = messageEvent.Message;
				var buttonSet = messageEvent.PlatformButtonSet;
				var description = String.Join("|", buttonSet.ButtonSetActions.Select(a => a.Label));


				var button1Config = buttonSet.ButtonSetActions[0];
				var button2Config = buttonSet.ButtonSetActions[1];
				alertView.AddActionButtons(
					button1Config.Label,
					() =>
					{
						manager.HandleInteractionResultAsync(pushMessage.MessageId, buttonSet.InteractionType,
							description,
							"Button1", button1Config.ActionType, button1Config.Data).ExecuteInBackground();
						displayAlertEvent.Dismiss();
					},
					button2Config.Label,
					() =>
					{
						manager.HandleInteractionResultAsync(pushMessage.MessageId, buttonSet.InteractionType,
							description,
							"Button2", button2Config.ActionType, button2Config.Data).ExecuteInBackground();
						displayAlertEvent.Dismiss();
					});
				autoDismiss = false;
			}

			displayAlertEvent.AutoDismiss = autoDismiss;

			DonkyCore.Instance.PublishLocalEvent(displayAlertEvent, Module);
		}

		private static void CreateBasicAlertView(Message message, out SimplePushAlertView alertView, out DisplayAlertEvent displayAlertEvent, string bodyOverride = null)
		{
			alertView = new SimplePushAlertView();
			var avatarId = message.AvatarAssetId;
			if (!String.IsNullOrEmpty(avatarId))
			{
				var assetHelper = DonkyCore.Instance.GetService<IAssetHelper>();
				alertView.Image.Source = new UriImageSource
				{
					Uri = new Uri(assetHelper.CreateUriForAsset(avatarId))
				};
			}
			alertView.TitleLabel.Text = message.SenderDisplayName;
			alertView.BodyLabel.Text = bodyOverride ?? message.Body;

			displayAlertEvent = new DisplayAlertEvent
			{
				Content = alertView
			};
		}

		private static void HandleDisplayBasicMessageAlert(DisplayBasicMessageAlertEvent alertEvent)
		{
			Message message = alertEvent.Message;
			SimplePushAlertView alertView;
			DisplayAlertEvent displayAlertEvent;
			CreateBasicAlertView(message, out alertView, out displayAlertEvent, alertEvent.AlertText);
			displayAlertEvent.AutoDismiss = true;

			DonkyCore.Instance.PublishLocalEvent(displayAlertEvent, Module);
		}
	}
}