using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using ManiaExchangeClient.DataObjects;

namespace ManiaExchangeClient.Ui
{
    /// <summary>
    /// Interaction logic for ShowImage.xaml
    /// </summary>
    public partial class ShowImage : MetroWindow
    {
        /// <summary>
        /// Contains the image source
        /// </summary>
        private readonly int _trackId;

        /// <summary>
        /// The image type
        /// </summary>
        private readonly ImageType _imageType;

        /// <summary>
        /// Creates a new instance of the <see cref="ShowImage"/>
        /// </summary>
        /// <param name="trackId">The image source</param>
        /// <param name="imageType">The image type</param>
        public ShowImage(int trackId, ImageType imageType)
        {
            InitializeComponent();

            _trackId = trackId;

            _imageType = imageType;
        }

        /// <summary>
        /// Prepares the image source
        /// </summary>
        /// <returns>The image source</returns>
        private string PrepareImageSource()
        {
            var settings = Helper.LoadSettings();

            if (_imageType == ImageType.Thumbnail)
            {
                var thumbnailPath = settings.Endpoints.FirstOrDefault(f => f.Type == EndpointType.Thumbnail);
                return thumbnailPath == null ? "" : $"{thumbnailPath.Path}{_trackId}";
            }
            else
            {
                var screenshotPath = settings.Endpoints.FirstOrDefault(f => f.Type == EndpointType.Screenshot);
                return screenshotPath == null ? "" : $"{screenshotPath.Path}{_trackId}";
            }
        }

        /// <summary>
        /// Occurs when the user hits the close button
        /// </summary>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Occurs when the window is loading
        /// </summary>
        private void ShowImage_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ShowImageViewModel viewModel)
                viewModel.InitViewModel(PrepareImageSource(), _imageType);
        }
    }
}
