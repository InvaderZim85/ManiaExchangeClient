using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using ManiaExchangeClient.Business;
using ManiaExchangeClient.DataObjects;
using WpfUtility.Services;

namespace ManiaExchangeClient.Ui
{
    public class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Occurs when the user selects another track
        /// </summary>
        public event CustomEvents.InfoEvent SelectionChanged;

        /// <summary>
        /// Contains the dialog coordinator
        /// </summary>
        private IDialogCoordinator _dialogCoordinator;

        /// <summary>
        /// Contains the rest manager
        /// </summary>
        private RestManager _restManager;

        /// <summary>
        /// Contains the settings
        /// </summary>
        private SettingsModel _settings;

        /// <summary>
        /// Backing field for <see cref="Author"/>
        /// </summary>
        private string _author;

        /// <summary>
        /// Gets or sets the author name
        /// </summary>
        public string Author
        {
            get => _author;
            set
            {
                SetField(ref _author, value);
                SearchButtonEnabled = !string.IsNullOrEmpty(TrackName) || !string.IsNullOrEmpty(Author);
            }
        }

        /// <summary>
        /// Backing field for <see cref="TrackName"/>
        /// </summary>
        private string _trackName;

        /// <summary>
        /// Gets or sets the track name
        /// </summary>
        public string TrackName
        {
            get => _trackName;
            set
            {
                SetField(ref _trackName, value);
                SearchButtonEnabled = !string.IsNullOrEmpty(TrackName) || !string.IsNullOrEmpty(Author);
            }
        }

        /// <summary>
        /// Backing field for <see cref="TrackList"/>
        /// </summary>
        private ObservableCollection<Track> _trackList;

        /// <summary>
        /// Gets or sets the list with the tracks
        /// </summary>
        public ObservableCollection<Track> TrackList
        {
            get => _trackList;
            set => SetField(ref _trackList, value);
        }

        /// <summary>
        /// Backing field for <see cref="SelectedTrack"/>
        /// </summary>
        private Track _selectedTrack;

        /// <summary>
        /// Gets or sets the selects track
        /// </summary>
        public Track SelectedTrack
        {
            get => _selectedTrack;
            set
            {
                DetailEnabled = value != null;

                if (SetField(ref _selectedTrack, value))
                {
                    SelectionChanged?.Invoke();
                    SetImagePath();
                }
            }
        }

        /// <summary>
        /// Backing field for <see cref="DetailEnabled"/>
        /// </summary>
        private bool _detailEnabled;

        /// <summary>
        /// Gets or sets the value which indicates if the detail window is enabled
        /// </summary>
        public bool DetailEnabled
        {
            get => _detailEnabled;
            set => SetField(ref _detailEnabled, value);
        }

        /// <summary>
        /// Backing field for <see cref="EnvironmentList"/>
        /// </summary>
        private ObservableCollection<Environment> _environmentList;

        /// <summary>
        /// Gets or sets the list with the environments
        /// </summary>
        public ObservableCollection<Environment> EnvironmentList
        {
            get => _environmentList;
            set => SetField(ref _environmentList, value);
        }

        /// <summary>
        /// Backing field for <see cref="SelectedEnvironment"/>
        /// </summary>
        private Environment _selectedEnvironment;

        /// <summary>
        /// Gets or sets the selected environment
        /// </summary>
        public Environment SelectedEnvironment
        {
            get => _selectedEnvironment;
            set => SetField(ref _selectedEnvironment, value);
        }

        /// <summary>
        /// Backing field for <see cref="TrackListHeader"/>
        /// </summary>
        private string _trackListHeader = "Tracks";

        /// <summary>
        /// Gets or sets the tracklist header
        /// </summary>
        public string TrackListHeader
        {
            get => _trackListHeader;
            set => SetField(ref _trackListHeader, value);
        }

        // Flyout data
        /// <summary>
        /// Backing field for <see cref="Title"/>
        /// </summary>
        private string _title;

        /// <summary>
        /// Gets or sets the program title
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        /// <summary>
        /// Backing field for <see cref="Version"/>
        /// </summary>
        private string _version;

        /// <summary>
        /// Gets or sets the version
        /// </summary>
        public string Version
        {
            get => _version;
            set => SetField(ref _version, value);
        }

        /// <summary>
        /// Backing field for <see cref="BuildDate"/>
        /// </summary>
        private string _buildDate;

        /// <summary>
        /// Gets or sets the build date
        /// </summary>
        public string BuildDate
        {
            get => _buildDate;
            set => SetField(ref _buildDate, value);
        }

        /// <summary>
        /// Backing field for <see cref="Location"/>
        /// </summary>
        private string _location;

        /// <summary>
        /// Gets or sets the location of the program
        /// </summary>
        public string Location
        {
            get => _location;
            set => SetField(ref _location, value);
        }

        /// <summary>
        /// Backing field for <see cref="Copyright"/>
        /// </summary>
        private string _copyright;

        /// <summary>
        /// Gets or sets the copyright information
        /// </summary>
        public string Copyright
        {
            get => _copyright;
            set => SetField(ref _copyright, value);
        }

        /// <summary>
        /// Backing field for <see cref="SearchButtonEnabled"/>
        /// </summary>
        private bool _searchButtonEnabled;

        /// <summary>
        /// Gets or sets the value which indicates if the search button is enabled
        /// </summary>
        public bool SearchButtonEnabled
        {
            get => _searchButtonEnabled;
            set => SetField(ref _searchButtonEnabled, value);
        }

        /// <summary>
        /// Backing field for <see cref="ThumbnailPath"/>
        /// </summary>
        private string _thumbnailPath;

        /// <summary>
        /// Gets or sets the path of the thumbnail
        /// </summary>
        public string ThumbnailPath
        {
            get => _thumbnailPath;
            set => SetField(ref _thumbnailPath, value);
        }

        /// <summary>
        /// Backing field for <see cref="ScreenshotPath"/>
        /// </summary>
        private string _screenshotPath;

        /// <summary>
        /// Gets or sets the path of the screenshot
        /// </summary>
        public string ScreenshotPath
        {
            get => _screenshotPath;
            set => SetField(ref _screenshotPath, value);
        }

        /// <summary>
        /// Backing field for <see cref="ShowScreenshotNoImage"/>
        /// </summary>
        private bool _showScreenshotNoImage;

        /// <summary>
        /// Gets or sets the value which indicates if a message should be shown instead of the screenshot
        /// </summary>
        public bool ShowScreenshotNoImage
        {
            get => _showScreenshotNoImage;
            set => SetField(ref _showScreenshotNoImage, value);
        }

        /// <summary>
        /// Backing field for <see cref="ShowThumbnailNoImage"/>
        /// </summary>
        private bool _showThumbnailNoImage;

        /// <summary>
        /// Gets or sets the value which indicates if a message should be shown instead of the thumbnail
        /// </summary>
        public bool ShowThumbnailNoImage
        {
            get => _showThumbnailNoImage;
            set => SetField(ref _showThumbnailNoImage, value);
        }

        /// <summary>
        /// The command to search for a track
        /// </summary>
        public ICommand SearchCommand => new RelayCommand<SearchType>(SearchTracks);

        /// <summary>
        /// The command to open the settings
        /// </summary>
        public ICommand SettingsCommand => new DelegateCommand(ShowSettings);

        /// <summary>
        /// Init the view model
        /// </summary>
        /// <param name="dialogCoordinator">The dialog coordinator</param>
        public void InitViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;

            _restManager = new RestManager();

            EnvironmentList = new ObservableCollection<Environment>(Helper.EnvironmentList());

            SelectedEnvironment = EnvironmentList.FirstOrDefault(f => f.Id == 0);

            InitInfoFlyout();
        }

        /// <summary>
        /// Init the data for the flyout
        /// </summary>
        private void InitInfoFlyout()
        {
            var details = Assembly.GetExecutingAssembly().GetInformation();

            if (details == null)
                return;

            Title = details.AssemblyTitle;
            Version = details.AssemblyVersion;
            Copyright = details.AssemblyCopyright;
            Location = Helper.GetBaseFolder();
            BuildDate = details.AssemblyDate.ToString("G");

        }

        /// <summary>
        /// Gets the path of the images
        /// </summary>
        private void SetImagePath()
        {
            if (SelectedTrack == null)
            {
                ThumbnailPath =
                    PrepareImageSource(ImageType.Thumbnail, 0);
                ScreenshotPath =
                    PrepareImageSource(ImageType.Screenshot, 0);

                ShowScreenshotNoImage = true;
                ShowThumbnailNoImage = true;
                return;
            }

            ThumbnailPath =
                PrepareImageSource(ImageType.Thumbnail, SelectedTrack.HasThumbnail ? SelectedTrack.TrackId : 0);
            ScreenshotPath =
                PrepareImageSource(ImageType.Screenshot, SelectedTrack.HasScreenshot ? SelectedTrack.TrackId : 0);

            ShowThumbnailNoImage = !SelectedTrack.HasThumbnail;
            ShowScreenshotNoImage = !SelectedTrack.HasScreenshot;
        }

        /// <summary>
        /// Prepares the image source
        /// </summary>
        /// <returns>The image source</returns>
        private string PrepareImageSource(ImageType imageType, int trackId)
        {
            if (_settings == null)
                _settings = Helper.LoadSettings();

            if (imageType == ImageType.Thumbnail)
            {
                var thumbnailPath = _settings.Endpoints.FirstOrDefault(f => f.Type == EndpointType.Thumbnail);
                return thumbnailPath == null ? "" : $"{thumbnailPath.Path}{trackId}";
            }
            else
            {
                var screenshotPath = _settings.Endpoints.FirstOrDefault(f => f.Type == EndpointType.Screenshot);
                return screenshotPath == null ? "" : $"{screenshotPath.Path}{trackId}";
            }
        }

        /// <summary>
        /// Shows the settings dialog
        /// </summary>
        private void ShowSettings()
        {
            var dialog = new SettingsWindow();
            dialog.ShowDialog();

            _restManager = new RestManager();

            _settings = Helper.LoadSettings();
        }

        /// <summary>
        /// Searchs for the tracks
        /// </summary>
        /// <param name="searchType">The search type</param>
        private async void SearchTracks(SearchType searchType)
        {
            var cancellationToken = new CancellationTokenSource();
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Loading - Please wait...",
                GetProgressMessage(searchType), true);

            controller.Canceled += (o, e) => cancellationToken.Cancel();

            controller.SetIndeterminate();

            var data = await _restManager.LoadTracks(searchType, Author, TrackName, SelectedEnvironment, cancellationToken);

            if (data != null)
                TrackList = new ObservableCollection<Track>(data);

            TrackListHeader = $"Tracks{(data != null ? $" - {data.Count}" : "")}";

            await controller.CloseAsync();
        }

        /// <summary>
        /// Gets the progress message
        /// </summary>
        /// <param name="searchType">The search type</param>
        /// <returns>The message for the progress dialog</returns>
        private string GetProgressMessage(SearchType searchType)
        {
            var msg = "Values:";

            switch (searchType)
            {
                case SearchType.Filter:
                    if (!string.IsNullOrEmpty(Author))
                        msg += $"\r\n- Author: {Author}";

                    if (!string.IsNullOrEmpty(TrackName))
                        msg += $"\r\n- Track name: {TrackName}";
                    break;
                case SearchType.LatestTracks:
                    msg += "\r\n- Latest 10 tracks";
                    break;
                case SearchType.RecentlyAwarded:
                    msg += "\r\n- Recently awarded tracks (10 entries)";
                    break;
                case SearchType.BestOfTheWeek:
                    msg += "\r\n- Best of the week (10 entries)";
                    break;
                case SearchType.BestOfTheMonth:
                    msg += "\r\n- Best of the month (10 entries)";
                    break;
                case SearchType.CompetitiveTracksOfTheWeek:
                    msg += "\r\n- Best competitive tracks of the week (10 entries)";
                    break;
                case SearchType.CompetitiveTracksOfTheMonth:
                    msg += "\r\n- Best competitive tracks of the month (10 entries)";
                    break;
            }

            if (SelectedEnvironment?.Id != 0)
                msg += $"\r\n- Environment: {SelectedEnvironment?.Name}";

            return msg;
        }
    }
}
