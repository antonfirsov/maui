using System;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;

namespace Microsoft.Maui.Controls
{
	/// <include file="../../../../docs/Microsoft.Maui.Controls/TabbedPage.xml" path="Type[@FullName='Microsoft.Maui.Controls.TabbedPage']/Docs" />
	public partial class TabbedPage
	{
		private protected override void OnHandlerChangedCore()
		{
			base.OnHandlerChangedCore();
		}

		protected override void LayoutChildren(double x, double y, double width, double height)
		{

		}

		public override Rectangle Frame
		{
			get => base.Frame;
			set => base.Frame = value;
		}


		/*protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
		{
			var size =  base.MeasureOverride(widthConstraint, heightConstraint);
			var childWidth = widthConstraint;
			var childHeight = heightConstraint;

			if (!ContainerArea.IsEmpty)
			{
				childWidth = ContainerArea.Width;
				childHeight = ContainerArea.Height;
			}

			foreach (var child in Children)
			{
				if (child is IView view)
				{
					_ = child.Measure(childWidth, childHeight);
				}
			}

			return size;
		}

		protected override Size ArrangeOverride(Rectangle bounds)
		{
			var arrangeSize = base.ArrangeOverride(bounds);
			var childWidth = Frame.Width;
			var childHeight = Frame.Height;

			if (!ContainerArea.IsEmpty)
			{
				childWidth = ContainerArea.Width;
				childHeight = ContainerArea.Height;
			}

			var childBounds = new Rectangle(0, 0, childWidth, childHeight);

			foreach (var child in Children)
			{
				if (child is IView view)
				{
					_ = view.Arrange(childBounds);
				}
			}

			return arrangeSize;
		}*/

		/*protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
		{
			if (Content is IView view)
			{
				_ = view.Measure(widthConstraint, heightConstraint);
			}

			return new Size(widthConstraint, heightConstraint);
		}

		protected override Size ArrangeOverride(Rectangle bounds)
		{
			Frame = this.ComputeFrame(bounds);

			if (Content is IView view)
			{
				_ = view.Arrange(Frame);
			}

			return Frame.Size;
		}*/

		private protected override void OnHandlerChangingCore(HandlerChangingEventArgs args)
		{
			base.OnHandlerChangingCore(args);

			if (args.NewHandler == null)
			{
				PagesChanged -= OnPagesChanged;
				WireUnwireChanges(false);
			}
			else if (args.OldHandler == null)
			{
				PagesChanged += OnPagesChanged;
				WireUnwireChanges(true);
			}

			void OnPagesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
			{
				WireUnwireChanges(false);
				Handler?.UpdateValue(TabbedPage.ItemsSourceProperty.PropertyName);
				WireUnwireChanges(true);
			}

			void WireUnwireChanges(bool wire)
			{
				foreach (var page in Children)
				{
					if (wire)
						page.PropertyChanged += OnPagePropertyChanged;
					else
						page.PropertyChanged -= OnPagePropertyChanged;
				}
			}

			void OnPagePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
			{
				if (e.PropertyName == Page.TitleProperty.PropertyName)
					Handler?.UpdateValue(TabbedPage.ItemsSourceProperty.PropertyName);
			}
		}
	}
}
