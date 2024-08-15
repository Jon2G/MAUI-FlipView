using AsyncAwaitBestPractices;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using System.ComponentModel;


namespace FlipView
{
    public enum FlipDirection
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
    public class FlippedChangedEventArgs : EventArgs
    {
        public bool Flipped { get; }
        public FlippedChangedEventArgs(bool flipped)
        {
            Flipped = flipped;
        }
    }

    public class FlipContentView : ContentView
    {

        public static readonly BindableProperty BackViewProperty = BindableProperty.Create(nameof(BackView), typeof(IView), typeof(FlipContentView), propertyChanged: OnBackViewPropertyChanged);

        public static new readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(IView), typeof(FlipContentView), propertyChanged: OnContentPropertyChanged);

        public static readonly BindableProperty IsFlippedProperty = BindableProperty.Create(nameof(IsFlipped), typeof(bool), typeof(FlipContentView), false, Microsoft.Maui.Controls.BindingMode.TwoWay, propertyChanged: OnlsFlippedPropertyChanged);


        public static readonly BindableProperty FlipDirectionProperty = BindableProperty.Create(nameof(FlipDirection), typeof(FlipDirection), typeof(FlipContentView), FlipDirection.LeftToRight);

        /// <summary>

        /// Backing BindableProperty for the <see cref="CommandParameter"/> property.

        /// </summary>

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FlipContentView));

        /// <summary>

        /// Backing BindableProperty for the <see cref="Command"/> property.

        /// </summary>

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FlipContentView));

        readonly Microsoft.Maui.WeakEventManager tappedEventManager = new();

        public event EventHandler<EventArgs> ExpandedChanged
        {
            add => tappedEventManager.AddEventHandler(value);
            remove => tappedEventManager.RemoveEventHandler(value);
        }

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value);
        }

        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty); set => SetValue(CommandProperty, value);
        }

        public IView? BackView
        {
            get => (IView?)GetValue(BackViewProperty); 
            set => SetValue(BackViewProperty, value);
        }

        public new IView? Content
        {
            get => (IView?)GetValue(FlipContentView.ContentProperty);
            set => SetValue(FlipContentView.ContentProperty, value);
        }

        public bool IsFlipped
        {
            get => (bool)GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }

        public FlipDirection FlipDirection
        {
            get => (FlipDirection)GetValue(FlipDirectionProperty);
            set => SetValue(FlipDirectionProperty, value);
        }

        static void OnContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flipView = (FlipContentView)bindable;
            if (newValue is View view)
            {
                view.IsVisible = !flipView.IsFlipped;
                if (oldValue != null)
                {
                    flipView.ContentGrid.Remove(oldValue as IView);
                }
                flipView.ContentGrid.Add(newValue as IView);
            }
        }

        static void OnBackViewPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var flipView = (FlipContentView)bindable;
            View? newView = newValue as View;
            if (newView is not null)
            {
                newView.SetBinding(IsVisibleProperty, new Binding(nameof(IsFlipped), source: bindable));
            }
            if (oldValue is View oldView)
            {
                oldView.RemoveBinding(IsVisibleProperty);
                flipView.ContentGrid.Remove(oldView as IView);
            }
            flipView.ContentGrid.Add(newView as IView);
        }

        static void OnlsFlippedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((FlipContentView)bindable).FlippedChanged(((FlipContentView)bindable).IsFlipped);
        }

        void FlippedChanged(bool isFlipped)
        {
            if (isFlipped)
            {
                FlipToBack().SafeFireAndForget();
            }
            else
            {
                FlipToFront().SafeFireAndForget();
            }
            if (Command?.CanExecute(CommandParameter) is true)
            {
                Command.Execute(CommandParameter);
            }
            tappedEventManager.HandleEvent(this, new FlippedChangedEventArgs(isFlipped), nameof(FlippedChanged));
        }

        private readonly Grid ContentGrid;
        public FlipContentView()
        {
            ContentGrid = new Grid();
            base.Content = ContentGrid;
        }

        private async Task Flip(VisualElement from, VisualElement to)
        {
            int rotation = 0;
            switch (this.FlipDirection)
            {
                case FlipDirection.LeftToRight:
                case FlipDirection.BottomToTop:
                    rotation = 90;
                    break;
                case FlipDirection.RightToLeft:
                case FlipDirection.TopToBottom:
                    rotation = -90;
                    break;
            }
            if (this.FlipDirection == FlipDirection.LeftToRight || this.FlipDirection == FlipDirection.RightToLeft)
            {
                await from.RotateYTo(rotation, 600, Easing.SpringIn);
                to.RotationY = 90;
            }
            else
            {
                await from.RotateXTo(rotation, 600, Easing.SpringIn);
                to.RotationX = 90;
            }
            to.IsVisible = true;
            from.IsVisible = false;
            if (this.FlipDirection == FlipDirection.LeftToRight || this.FlipDirection == FlipDirection.RightToLeft)
            {
                await to.RotateYTo(0, 600, Easing.SpringOut);
            }
            else
            {
                await to.RotateXTo(0, 600, Easing.SpringOut);
            }
        }

        private async Task FlipToBack()
        {
            if (this.Content == null || this.BackView == null)
            {
                return;
            }
            var front = this.Content as VisualElement;
            var back = this.BackView as VisualElement;
            await Flip(front, back);
        }

        private async Task FlipToFront()
        {
            if (this.Content == null || this.BackView == null)
            {
                return;
            }
            var back = this.BackView as VisualElement;
            var front = this.Content as VisualElement;
            await Flip(back, front);
        }
    }
}




