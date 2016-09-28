@ECHO OFF
PUSHD
CD ..\src\Donky.Core\Core
..\..\..\.nuget\nuget.exe pack Core.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Core.Analytics
..\..\..\.nuget\nuget.exe pack Core.Analytics.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\..\Donky.Core.Xamarin\Core.Xamarin.Android
..\..\..\.nuget\nuget.exe pack Core.Xamarin.Android.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Core.Xamarin.Forms
..\..\..\.nuget\nuget.exe pack Core.Xamarin.Forms.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Core.Xamarin.iOS
..\..\..\.nuget\nuget.exe pack Core.Xamarin.iOS.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\..\Donky.Automation\Automation
..\..\..\.nuget\nuget.exe pack Automation.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\..\Donky.Messaging\Common
..\..\..\.nuget\nuget.exe pack Common.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Push.Logic
..\..\..\.nuget\nuget.exe pack Push.Logic.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Push.UI.Android
..\..\..\.nuget\nuget.exe pack Push.UI.Android.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Push.UI.iOS
..\..\..\.nuget\nuget.exe pack Push.UI.iOS.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Push.UI.XamarinForms
..\..\..\.nuget\nuget.exe pack Push.UI.XamarinForms.nuspec -OutputDirectory ..\..\..\PackagesToPublish
CD ..\Rich.Logic
..\..\..\.nuget\nuget.exe pack Rich.Logic.nuspec -OutputDirectory ..\..\..\PackagesToPublish

POPD