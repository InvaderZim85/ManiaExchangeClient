using System.Windows;
using System.Windows.Controls;
using ManiaExchangeClient.DataObjects;

namespace ManiaExchangeClient.Ui.CustomControls
{
    /// <summary>
    /// Interaction logic for TrackDetailFullControl.xaml
    /// </summary>
    public partial class TrackDetailFullControl : UserControl
    {
        /// <summary>
        /// Creates a new instance of <see cref="TrackDetailFullControl"/>
        /// </summary>
        public TrackDetailFullControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The dependency property for <see cref="SelectedTrack"/>
        /// </summary>
        public static readonly DependencyProperty SelectedTrackProperty = DependencyProperty.Register(
            nameof(SelectedTrack), typeof(Track), typeof(TrackDetailFullControl), new PropertyMetadata(default(Track)));

        /// <summary>
        /// Gets or sets the selected track
        /// </summary>
        public Track SelectedTrack
        {
            get => (Track) GetValue(SelectedTrackProperty);
            set => SetValue(SelectedTrackProperty, value);
        }
    }
}
