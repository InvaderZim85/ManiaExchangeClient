using System.Diagnostics;
using System.Linq;
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
        /// Contains the settings
        /// </summary>
        private SettingsModel _settings;

        /// <summary>
        /// Creates a new instance of the <see cref="TrackDetailControl"/>
        /// </summary>
        public TrackDetailControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inits the control
        /// </summary>
        public void InitControl()
        {
            _settings = Helper.LoadSettings();
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
            get => (Track) GetValue(SelectedTrackProperty);
            set => SetValue(SelectedTrackProperty, value);
        }

        /// <summary>
        /// Occurs when the user hits the show thumbnail button
        /// </summary>
        private void ButtonShowThumbnail_Click(object sender, RoutedEventArgs e)
        {
            var id = SelectedTrack.TrackId;
            new ShowImage(id, ImageType.Thumbnail).ShowDialog();
        }

        /// <summary>
        /// Occurs when the user hits the show screenshot button
        /// </summary>
        private void ButtonShowScreenshot_Click(object sender, RoutedEventArgs e)
        {
            var id = SelectedTrack.TrackId;
            new ShowImage(id, ImageType.Screenshot).ShowDialog();
        }

        /// <summary>
        /// Occurs when the user hits the show online button
        /// </summary>
        private void ButtonShowOnline_Click(object sender, RoutedEventArgs e)
        {
            var id = SelectedTrack.TrackId;
            var trackPath = _settings?.Endpoints.FirstOrDefault(f => f.Type == EndpointType.TrackPage)?.Path ?? "";

            if (string.IsNullOrEmpty(trackPath))
                return;

            Process.Start($"{trackPath}{id}");
        }
    }
}
