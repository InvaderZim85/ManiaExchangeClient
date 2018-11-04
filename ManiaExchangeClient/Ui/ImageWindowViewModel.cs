using ManiaExchangeClient.DataObjects;
using WpfUtility.Services;

namespace ManiaExchangeClient.Ui
{
    public class ImageWindowViewModel: ObservableObject
    {
        /// <summary>
        /// Backing field for <see cref="ImageSource"/>
        /// </summary>
        private string _imageSource;

        /// <summary>
        /// Gets or sets the image source
        /// </summary>
        public string ImageSource
        {
            get => _imageSource;
            set => SetField(ref _imageSource, value);
        }

        /// <summary>
        /// Backing field for <see cref="Header"/>
        /// </summary>
        private string _header = "Image";

        /// <summary>
        /// Gets or sets the header
        /// </summary>
        public string Header
        {
            get => _header;
            set => SetField(ref _header, value);
        }

        /// <summary>
        /// Init the view model
        /// </summary>
        /// <param name="imageSource">The image source</param>
        /// <param name="imageType">The image type</param>
        public void InitViewModel(string imageSource, ImageType imageType)
        {
            ImageSource = imageSource;

            Header = imageType.ToString();
        }
    }
}
