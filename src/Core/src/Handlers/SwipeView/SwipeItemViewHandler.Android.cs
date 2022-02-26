﻿using System;
using PlatformView = Microsoft.Maui.Platform.ContentViewGroup;

namespace Microsoft.Maui.Handlers
{
	public interface ISwipeItemViewHandler : IViewHandler
	{
		new ISwipeItemView VirtualView { get; }
		new PlatformView PlatformView { get; }
	}

	public class SwipeItemViewHandler : ViewHandler<ISwipeItemView, PlatformView>, ISwipeItemViewHandler
	{
		public static IPropertyMapper<ISwipeItemView, ISwipeItemViewHandler> Mapper = new PropertyMapper<ISwipeItemView, ISwipeItemViewHandler>(ViewHandler.ViewMapper)
		{
			[nameof(ISwipeItemView.Content)] = MapContent,
			[nameof(ISwipeItemView.Visibility)] = MapVisibility
		};

		public static CommandMapper<ISwipeItemView, ISwipeItemViewHandler> CommandMapper = new(ViewHandler.ViewCommandMapper)
		{
		};

		public SwipeItemViewHandler() : base(Mapper, CommandMapper)
		{
		}

		protected SwipeItemViewHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null)
			: base(mapper, commandMapper ?? CommandMapper)
		{
		}

		public SwipeItemViewHandler(IPropertyMapper? mapper = null) : base(mapper ?? Mapper)
		{
		}

		ISwipeItemView ISwipeItemViewHandler.VirtualView => VirtualView;

		PlatformView ISwipeItemViewHandler.PlatformView => PlatformView;

		protected override ContentViewGroup CreatePlatformView()
		{
			if (VirtualView == null)
			{
				throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a ContentViewGroup");
			}

			var viewGroup = new ContentViewGroup(Context)
			{
				CrossPlatformMeasure = VirtualView.CrossPlatformMeasure,
				CrossPlatformArrange = VirtualView.CrossPlatformArrange
			};

			return viewGroup;
		}

		public override void SetVirtualView(IView view)
		{
			base.SetVirtualView(view);
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");

			PlatformView.CrossPlatformMeasure = VirtualView.CrossPlatformMeasure;
			PlatformView.CrossPlatformArrange = VirtualView.CrossPlatformArrange;
		}

		void UpdateContent()
		{
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");

			PlatformView.RemoveAllViews();

			if (VirtualView.PresentedContent is IView view)
				PlatformView.AddView(view.ToPlatform(MauiContext));
		}

		public static void MapContent(ISwipeItemViewHandler handler, ISwipeItemView page)
		{
			if (handler is SwipeItemViewHandler platformHandler)
				platformHandler.UpdateContent();
		}

		public static void MapVisibility(ISwipeItemViewHandler handler, ISwipeItemView view)
		{
			var swipeView = handler.PlatformView?.Parent.GetParentOfType<MauiSwipeView>();
			if (swipeView != null)
				swipeView.UpdateIsVisibleSwipeItem(view);
		}

		protected override void DisconnectHandler(ContentViewGroup platformView)
		{
			// If we're being disconnected from the xplat element, then we should no longer be managing its chidren
			platformView.RemoveAllViews();
			base.DisconnectHandler(platformView);
		}
	}
}
