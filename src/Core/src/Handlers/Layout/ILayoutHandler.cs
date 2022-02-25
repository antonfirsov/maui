#if __IOS__ || MACCATALYST
using PlatformView = Microsoft.Maui.Platform.LayoutView;
#elif MONOANDROID
using PlatformView = Microsoft.Maui.Platform.LayoutViewGroup;
#elif WINDOWS
using PlatformView = Microsoft.Maui.Platform.LayoutPanel;
#elif NETSTANDARD || (NET6_0 && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif

namespace Microsoft.Maui
{
	public interface ILayoutHandler : IViewHandler
	{
		new ILayout VirtualView { get; }
		new PlatformView PlatformView { get; }

		void Add(IView view);
		void Remove(IView view);
		void Clear();
		void Insert(int index, IView view);
		void Update(int index, IView view);
		void UpdateZIndex(IView view);
	}
}
