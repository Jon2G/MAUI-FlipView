using FlipView;
using System.ComponentModel;
using System.Windows.Input;

namespace FlipViewTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }

    public class MainPageViewModel : BindableObject, INotifyPropertyChanged
    {
        public ICommand FlipAllCommand { get; set; }
        public ICommand FlipImageCommand { get; set; }
        public ICommand FlipControls { get; set; }
        public ICommand FlipFrameCommand { get; set; }

        public bool IsImageFlipped { get; set; }
        public bool IsControlsFlipped { get; set; }
        public bool IsFrameFlipped { get; set; }

        private FlipDirection _FlipDirection = FlipDirection.BottomToTop;
        public FlipDirection FlipDirection
        {
            get => _FlipDirection; set
            {
                _FlipDirection = value;
                OnPropertyChanged(nameof(FlipDirection));
            }
        }
        public List<FlipDirection> FlipDirections { get; } = new List<FlipDirection> { FlipDirection.LeftToRight, FlipDirection.RightToLeft, FlipDirection.TopToBottom, FlipDirection.BottomToTop };
        public MainPageViewModel()
        {
            this.FlipAllCommand = new Command(() =>
            {
                IsImageFlipped = !IsImageFlipped;
                IsControlsFlipped = !IsControlsFlipped;
                IsFrameFlipped = !IsFrameFlipped;
                OnPropertyChanged(nameof(IsImageFlipped));
                OnPropertyChanged(nameof(IsControlsFlipped));
                OnPropertyChanged(nameof(IsFrameFlipped));
            });
            this.FlipImageCommand = new Command(() =>
            {
                IsImageFlipped = !IsImageFlipped;
                OnPropertyChanged(nameof(IsImageFlipped));
            });
            this.FlipControls = new Command(() =>
            {
                IsControlsFlipped = !IsControlsFlipped;
                OnPropertyChanged(nameof(IsControlsFlipped));
            });
            this.FlipFrameCommand = new Command(() =>
            {
                IsFrameFlipped = !IsFrameFlipped;
                OnPropertyChanged(nameof(IsFrameFlipped));
            });
        }

    }

}
