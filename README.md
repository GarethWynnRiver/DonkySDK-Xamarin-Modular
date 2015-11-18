<p align="center" >
  <img src="https://avatars2.githubusercontent.com/u/11334935?v=3&s=200" alt="Donky Networks LTD" title="Donky Network SDK">
</p>

# Donky Modular SDK (V2.2.0)

The modular SDK exposes all of the network functionality in a way that means integrators can consume only the pieces they need in order to:

<ul>
<li>Send custom notifications</li>
<li>Use Donky's messaging features</li>
<li>Automate actions and messaging</li>
<li>Track in app analytics</li>
<li>Build complex M2M, A2P and P2P applications</li>
</ul>

Using Donky as a data network allows developers to focus on writing the apps code rather than having to worry about building a reliable and secure network. The complexities of transferring data across the internet are taken care of, allowing developers to just build their applications.

##Requirements

The minimal technical requirements for the Donky's Xamarin Modular SDK are:

<ul>
<li>Xamarin Platform, available at https://xamarin.com/platform</li>
<li>Either:<br />
<ul>
<li>Xamarin Studio (installed as part of the platform download), or</li>
<li>Visual Studio Community Edition 2013 or 2015, available at https://www.visualstudio.com/en-us/downloads/</li>
</ul>
</li>
</ul>


Read our complete documentation [here](http://docs.mobiledonky.com)

## Author

Donky Networks Ltd, sdk@mobiledonky.com

## License

DonkySDK-Xamarin-Modular is available under the MIT license. See the LICENSE file for more info.

## Installation

To install the SDK, please use one of the following methods:

Cloning the Git Repo:

``` https://github.com/Donky-Network/DonkySDK-Xamarin-Modular.git ```

Using [Nuget](https://www.nuget.org/profiles/donky), for example:

``` PM> Install-Package Donky.Core ```

Please see the comprehensive GitHub package installation guide below!

## Support

Please contact sdk@mobiledonky.com if you have any issues with integrating or using this SDK.

## Contribute

We accept pull requests!

# Donky-Core-SDK

## Modules
The Donky Xamarin SDK contains the following modules:

**Donky.Core**<br />
The Core module provides the basic functionality required for connecting to the Donky Network, handling notifications and sending / receiving custom content. This is provided as a cross-platform PCL. It must be used in conjunction with the relevant platform module (Donky.Core.Xamarin.Android or Donky.Core.Xamarin.IOS) to provide push capabilities, storage capabilities and other contextual information.

**Donky.Core.Xamarin.iOS**<br />
**Donky.Core.Xamarin.Android**<br />
These modules provide Core with platform specific functions.

**Donky.Core.Xamarin.Forms**<br />
If using Xamarin Forms, this module provides a base Application class to aid integration, as well as some base framework for other Forms based functionality.

**Donky.Core.Analytics**<br />
This module tracks app usage, and passes data to the Donky Network to enable statistics around app opens, time in app and influenced launches.

**Donky.Automation**<br />
This module provides the ability to execute 3rd Party Triggers on the Donky Network

**Donky.Messaging.Common**<br />
Provides common functionality that will be used by the logic modules for all message types.

**Donky.Messaging.Push.Logic**<br />
Logic for handling Simple + Interactive Push messages and passing context back the Donky Network.

**Donky.Messaging.Push.UI.XamarinForms**<br />
**Donky.Messaging.Push.UI.iOS**<br />
**Donky.Messaging.Push.UI.Android**<br />
Provides an optional UI for in-app handling of Simple & Interactive Push. This is still rough in terms of presentation and needs extending to allow customisations of theme + some fixes around layout.

**Donky.Messaging.Rich.Logic**<br />
Logic for handling Rich Messages.

**Donky.Messaging.Rich.PopupUI.XamarinForms** (still in Private Preview)<br />
**Donky.Messaging.Rich.PopupUI.Android** (still in Private Preview)<br />
Optional UI for displaying rich messages in a modal popup when they arrive. For Android, an additional module is required to enable local notifications when a message is received while the app is closed / in the background.￼￼￼￼

## Nuget Packages
The Xamarin Forms SDK **Nuget Package Repository** is a repository for build artefacts and you can find the SDK's Donky Core and Module libraries [here](https://www.nuget.org/profiles/donky).

If you follow the walk-throughs in the documentation, beginning with the [Set-up Core and Register Users](http://docs.mobiledonky.com/docs/xamarin-setup-core-register-users) guide, you will learn which packages to install for those features you wish to consume. 

There are also some quick-start guides below for those who are already familiar with Nuget.

**Nuget Fast-track**<br />
You can choose any public module from the above list and simply go:

``` PM> Install-Package namespace ```

Installing the following Donky Modules will cause additional Nuget packages to be loaded into your solution:

n.b. Please note that the versions are the minimum version <br />
that will be loaded and the Nuget Package Manager typically <br />
loads the latest version.<br />

**Donky.Core Version 2.2.0** installs:<br />
Autofac Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.29<br />
modernhttpclient Version 2.4.2<br />
Newtonsoft.Json 7.0.1<br />

**Donky.Analytics Version 2.2.0** installs:<br />
Microsoft.Bcl.Build Version 1.0.21<br />

**Donky.Core.Xamarin.Android Version 2.2.0** installs:<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Xam.Plugins.Forms.ImageCircle Version 1.1.4<br />
Xamarin.Android.Support.v4 Version 22.2.1.0<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Core.Xamarin.Forms Version 2.2.0** installs:<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Core.Xamarin.iOS Version 2.2.0** installs:<br />
Microsoft.Bcl.BuildV ersion 1.0.21<br />
Xam.Plugins.Forms.ImageCircle Version 1.1.4<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Messaging.Push.Logic Version 2.2.0** installs:<br />
Autofac Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.28<br />
Newtonsoft.Json Version 7.0.1<br />

**Donky.Messaging.Push.UI.Android Version 2.2.0** installs:<br />
Autofac Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.29<br />
Newtonsoft.Json Version 7.0.1<br />
Xamarin.Android.Support.v4 Version 22.2.1.0<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Messaging.Push.UI.iOS Version 2.2.0** installs:<br />
Autofac Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.29<br />
Newtonsoft.Json Version 7.0.1<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Messaging.Push.UI.XamarinForms Version 2.2.0** installs:<br />
Autofac Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl" Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.22<br />
Newtonsoft.Json Version 7.0.1<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Messaging.Rich.Inbox.XamarinForms (Private Preview)** installs:<br />
Xam.Plugins.Forms.ImageCircle Version 1.1.4<br />
Xamarin.Forms Version 1.5.0.6446<br />

**Donky.Messaging.Rich.Logic Version 2.2.0** installs:<br />
Autofac Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.28<br />
Newtonsoft.Json Version 7.0.1<br />

**Donky.Messaging.Common Version 2.2.0** installs:<br />
Autofac  Version 3.5.2<br />
Microsoft.AspNet.WebApi.Client Version 5.2.3<br />
Microsoft.Bcl Version 1.1.10<br />
Microsoft.Bcl.Build Version 1.0.21<br />
Microsoft.Net.Http Version 2.2.28<br />
Newtonsoft.Json Version 7.0.1<br />

Please note that this list is not comprehensive as third-party libraries will of course pull-in their own dependencies, and by way of example, the dependencies of Xamarin.Forms Version 1.5.0.6446 will pull the following into the Android Host Project:

```
Xamarin.Android.Support.v4
Xamarin.Android.Support.v7.AppCompat
Xamarin.Android.Support.v7.CardView
Xamarin.Android.Support.v7.MediaRouter 
```

## Source Code (here on GitHub)

If you would prefer to use the source code directly, instead of downloading Donky's Nuget packages, you can download the source here, using the clone command indicated above.

You can determine which namespaces to install for the features that you wish to consume, by following the walk-throughs that commence with the [Set-up Core and Register Users](http://docs.mobiledonky.com/docs/xamarin-setup-core-register-users) guide, although the walk-throughs refer to the Nuget packages, the code projects share the very same namespaces that the Nuget packages are registered under.

For example, where you see the Nuget installation command line:

```
PM> Install-Package Donky.Core
```

then reference the **Donky.Core** source-code project in your solution.

**Note:**<br />
Although referencing a project directly in source code will give you direct access to it, you may wish to look at each project's **packages.config** file to show you which 3rd party Nuget libraries that the project will automatically pull in when all of the Nuget packages are restored during the build process!

# Getting Started

It is recommended that the following Nuget Packages / Source Code Project References, are added to the Xamarin Forms project:

  * Donky.Core.XamarinForms (this will pull in Donky.Core automatically)
  * Donky.Core.Analytics
  * Donky.Automation (if needed)
  * Donky.Messaging.Push.UI.XamarinForms (if using Simple / Interactive Push)
  * Donky.Messaging.Rich.PopupUI.XamarinForms (if using Rich Popup)

To the iOS and Android projects, add any packages used in the Forms project plus the following:

  * Donky.Core.Xamarin.Android / iOS as appropriate
  * Donky.Messaging.Push.UI.Android / iOS if needed
  * Donky.Messaging.Rich.PopupUI.Android if needed 
  
## Xamarin Forms Project Quickstart

Within your Xamarin Forms project, make the following changes to integrate Donky:

  * Change the App class to derive from Donky.Core.Xamarin.Forms.DonkyApplication (which in turn derives from Xamarin.Forms.Application)
  * Create a static bootstrap class (will be called from the native projects) – example:
 
```
public static class AppBootstrap
{
	public static void Initialise()
	{
		// Initialise any modules (except Core) here
		DonkyCoreAnalytics.Initialise();
		DonkyAutomation.Initialise();
		DonkyCommonMessaging.Initialise();
		DonkyPushLogic.Initialise();
		DonkyRichLogic.Initialise();
		DonkyRichPopupUIXamarinForms.Initialise();
		DonkyXamarinForms.Initialise();
    
    InitInternal().ExecuteInBackground();
	￼}

￼ ￼ private static async Task InitInternal()
￼  {
￼	  	var result = await DonkyCore.Instance.InitialiseAsync(
￼								"client API key from Donky Control in here");
￼	  	if (!result.Success)
  ￼ 	{
￼		  	// TODO: Check result for failure info
￼	  	}
￼  }
￼}
```

## Android Project Quickstart

Within the Android project, if you don’t have an Application class, create one as follows, if you already have this then add the relevant calls to OnCreate:

```
[Application]
￼public class DonkyTestApplication : Application
￼{
￼		public DonkyTestApplication(
  		IntPtr handle, 
  		JniHandleOwnership transfer
			): base(handle, transfer)
￼			 {}
￼
￼		public override void OnCreate()
	 {
￼￼		 base.OnCreate();
￼￼		 
     DonkyAndroid.Initialise();
￼     DonkyPushUIAndroid.Initialise();
￼     DonkyRichPopupUIAndroid.Initialise();
￼     AppBootstrap.Initialise();
   }
￼￼}
```

In your Main Activity add the line **DonkyAndroid.ActivityLaunchedWithIntent(this);** to the OnCreate method:

```
protected override void OnCreate(Bundle bundle)
￼{
￼		 base.OnCreate(bundle);
￼		
  	// Ensure Donky knows if we were launched from a notification
￼￼		DonkyAndroid.ActivityLaunchedWithIntent(this);
￼		 
    global::Xamarin.Forms.Forms.Init(this, bundle);
￼		 LoadApplication(new App());
￼}
```

Permissions for GCM:

```
Insert the following into Android.Manifest.xml:

<!-- GCM permissions -->
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.WAKE_LOCK" />
<uses-permission android:name="android.permission.RECEIVE_BOOT_COMPLETED" />
<uses-permission android:name="com.google.android.c2dm.permission.RECEIVE" />
<permission android:name="PACKAGENAME.permission.C2D_MESSAGE" /> 
<uses-permission android:name="PACKAGENAME.permission.C2D_MESSAGE" />
<!-- END GCM permissions -->
```

## iOS Project Quickstart

For iOS, you need to add the following into the AppDelegate class:

```
public override bool FinishedLaunching(
  UIApplication app, 
  NSDictionary options)
{
    Xamarin.Forms.Forms.Init();

    // ImageCircleRenderer.Init(); This is used in the Rich InBox,
    // which is currently still in Private Preview

    DonkyiOS.Initialise();
    DonkyPushUIiOS.Initialise();
    AppBootstrap.Initialise();
  
    LoadApplication(new App());

    return base.FinishedLaunching(app, options);
}

public override void RegisteredForRemoteNotifications(
  UIApplication application, 
  NSData deviceToken)
{
    DonkyiOS.RegisteredForRemoteNotifications(application, deviceToken);
}

public override void DidReceiveRemoteNotification(
  UIApplication application, 
  NSDictionary userInfo,
  Action<UIBackgroundFetchResult> completionHandler)
{
    DonkyiOS.DidReceiveRemoteNotification(
      application, 
      userInfo, 
      completionHandler);
}

public override void HandleAction(
  UIApplication application, 
  string actionIdentifier,
  NSDictionary remoteNotificationInfo,
  Action completionHandler)
{
    DonkyiOS.HandleAction(
      application, 
      actionIdentifier, 
      remoteNotificationInfo, 
      completionHandler);
}

public override void FailedToRegisterForRemoteNotifications(
  UIApplication application, 
  NSError error)
{
    DonkyiOS.FailedToRegisterForRemoteNotifications(application, error);
}
```

In your Localizable.strings file, add the following:

```
//For all standard remote notiications:
//Where the %1$@ is the name of the sender. 
//You can remove this and simply 
//have the %2$@ or vica versa.
"MSG_PRV" = "%1$@ - %2$@";
```
