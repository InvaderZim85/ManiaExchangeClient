using System.Windows;
using System.Windows.Controls;
using ManiaExchangeClient.DataObjects;

namespace ManiaExchangeClient.Ui.CustomControls
{
    /// <summary>
    /// Interaction logic for TrackDetailControl.xaml
    /// </summary>
    public partial class TrackDetailControl : UserControl
    {
        /// <summary>
        /// Creates a new instance of the <see cref="TrackDetailControl"/>
        /// </summary>
        public TrackDetailControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The dependency property for <see cref="SelectedTrack"/>
        /// </summary>
        public static readonly DependencyProperty SelectedTrackProperty = DependencyProperty.Register(
            nameof(SelectedTrack), typeof(Track), typeof(TrackDetailControl), new PropertyMetadata(default(Track)));

        /// <summary>
        /// Gets or sets the selected track
        /// </summary>
        public Track SelectedTrack
        {
            get => (Track)GetValue(SelectedTrackProperty);
            set => SetValue(SelectedTrackProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="ThumbnailPath"/>
        /// </summary>
        public static readonly DependencyProperty ThumbnailPathProperty = DependencyProperty.Register(
            nameof(ThumbnailPath), typeof(string), typeof(TrackDetailControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the thumbnail path
        /// </summary>
        public string ThumbnailPath
        {
            get => (string) GetValue(ThumbnailPathProperty);
            set => SetValue(ThumbnailPathProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="ScreenshotPath"/>
        /// </summary>
        public static readonly DependencyProperty ScreenshotPathProperty = DependencyProperty.Register(
            nameof(ScreenshotPath), typeof(string), typeof(TrackDetailControl), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the path of the screenshot
        /// </summary>
        public string ScreenshotPath
        {
            get => (string) GetValue(ScreenshotPathProperty);
            set => SetValue(ScreenshotPathProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="ShowThumbnailNoImage"/>
        /// </summary>
        public static readonly DependencyProperty ShowThumbnailNoImageProperty = DependencyProperty.Register(
            nameof(ShowThumbnailNoImage), typeof(bool), typeof(TrackDetailControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Gets or sets the value which indicates if a message should be shown instead the thumbnail
        /// </summary>
        public bool ShowThumbnailNoImage
        {
            get => (bool) GetValue(ShowThumbnailNoImageProperty);
            set => SetValue(ShowThumbnailNoImageProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="ShowScreenshotNoImage"/>
        /// </summary>
        public static readonly DependencyProperty ShowScreenshotNoImageProperty = DependencyProperty.Register(
            nameof(ShowScreenshotNoImage), typeof(bool), typeof(TrackDetailControl), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Gets or sets the value which indicates if a message should be shown instead the screenshot
        /// </summary>
        public bool ShowScreenshotNoImage
        {
            get => (bool) GetValue(ShowScreenshotNoImageProperty);
            set => SetValue(ShowScreenshotNoImageProperty, value);
        }

        /// <summary>
        /// Occurs when the index was changed
        /// </summary>
        private void FlipViewTest_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (FlipView.SelectedIndex)
            {
                case 0:
                    FlipView.BannerText = "Thumbnail";
                    break;
                case 1:
                    FlipView.BannerText = "Screenshot";
                    break;
            }
        }

        /// <summary>
        /// Occurs when the track id was changed
        /// </summary>
        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            FlipView.SelectedIndex = 0;
        }
    }
}
