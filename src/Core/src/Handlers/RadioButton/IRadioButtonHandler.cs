﻿#if __IOS__ || MACCATALYST
using PlatformView = Microsoft.Maui.Platform.ContentView;
#elif MONOANDROID
using PlatformView = Android.Views.View;
#elif WINDOWS
using PlatformView = Microsoft.Maui.Platform.MauiRadioButton;
#elif NETSTANDARD || (NET6_0 && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif

namespace Microsoft.Maui.Handlers
{
	public partial interface IRadioButtonHandler : IViewHandler
	{
		new IRadioButton VirtualView { get; }
		new PlatformView PlatformView { get; }
	}
}