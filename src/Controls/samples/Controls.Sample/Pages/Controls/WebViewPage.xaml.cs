﻿using System;
using System.Diagnostics;

namespace Maui.Controls.Sample.Pages
{
	public partial class WebViewPage
	{
		public WebViewPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			MauiWebView.Navigating += OnMauiWebViewNavigating;
			MauiWebView.Navigated += OnMauiWebViewNavigated;
		}

		protected override void OnDisappearing()
		{
			MauiWebView.Navigating -= OnMauiWebViewNavigating;
			MauiWebView.Navigated -= OnMauiWebViewNavigated;
		}

		void OnGoBackClicked(object sender, EventArgs args)
		{
			Debug.WriteLine($"CanGoBack {MauiWebView.CanGoBack}");

			if (MauiWebView.CanGoBack)
			{
				MauiWebView.GoBack();
			}
		}

		void OnGoForwardClicked(object sender, EventArgs args)
		{
			Debug.WriteLine($"CanGoForward {MauiWebView.CanGoForward}");

			if (MauiWebView.CanGoForward)
			{
				MauiWebView.GoForward();
			}
		}

		void OnReloadClicked(object sender, EventArgs args)
		{
			MauiWebView.Reload();
		}

		void OnEvalClicked(object sender, EventArgs args)
		{
			MauiWebView.Eval("alert('text')");
		}

		void OnMauiWebViewNavigating(object sender, Microsoft.Maui.Controls.WebNavigatingEventArgs e)
		{
			Debug.WriteLine($"Navigating - Url: {e.Url}, Event: {e.NavigationEvent}");
		}

		void OnMauiWebViewNavigated(object sender, Microsoft.Maui.Controls.WebNavigatedEventArgs e)
		{
			Debug.WriteLine($"Navigated - Url: {e.Url}, Event: {e.NavigationEvent}, Result: {e.Result}");
		}

		async void OnEvalAsyncClicked(object sender, EventArgs args)
		{
			MauiWebView.Eval("alert('text')");

			var result = await MauiWebView.EvaluateJavaScriptAsync(
				"var test = function(){ return 'This string came from Javascript!'; }; test();");

			EvalResultLabel.Text = result;
		}
	}
}