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

## Nuget
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

