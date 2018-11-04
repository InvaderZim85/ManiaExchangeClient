using System.Collections.ObjectModel;
using System.Linq;
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
        /// Event which occurs when the settings were changed
        /// </summary>
        public event CustomEvents.InfoEvent SettingsReloaded;

        /// <summary>
        /// Contains the dialog coordinator
        /// </summary>
        private IDialogCoordinator _dialogCoordinator;

        /// <summary>
        /// Contains the rest manager
        /// </summary>
        private RestManager _restManager;

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
            set => SetField(ref _author, value);
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
                SetField(ref _selectedTrack, value);
                DetailEnabled = value != null;
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
        /// Backing field for <see cref="TrackName"/>
        /// </summary>
        private string _trackName;

        /// <summary>
        /// Gets or sets the track name
        /// </summary>
        public string TrackName
        {
            get => _trackName;
            set => SetField(ref _trackName, value);
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

        /// <summary>
        /// The command to search for a track
        /// </summary>
        public ICommand SearchCommand => new DelegateCommand(SearchTracks);

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
        }

        /// <summary>
        /// Shows the settings dialog
        /// </summary>
        private void ShowSettings()
        {
            var dialog = new SettingsWindow();
            dialog.ShowDialog();

            SettingsReloaded?.Invoke();
            _restManager = new RestManager();
        }

        /// <summary>
        /// Searchs for the tracks
        /// </summary>
        private async void SearchTracks()
        {
            var cancellationToken = new CancellationTokenSource();
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Loading - Plase wait...",
                GetProgressMessage(), true);

            controller.Canceled += (o, e) => cancellationToken.Cancel();

            controller.SetIndeterminate();

            var data = await _restManager.LoadTracks(Author, TrackName, SelectedEnvironment, cancellationToken);

            if (data != null)
                TrackList = new ObservableCollection<Track>(data);

            TrackListHeader = $"Tracks{(data != null ? $" - {data.Count}" : "")}";

            await controller.CloseAsync();
        }

        /// <summary>
        /// Gets the progress message
        /// </summary>
        /// <returns>The message for the progress dialog</returns>
        private string GetProgressMessage()
        {
            var msg = "Values:";

            if (!string.IsNullOrEmpty(Author))
                msg += $"\r\n- Author: {Author}";

            if (!string.IsNullOrEmpty(TrackName))
                msg += $"\r\n- Track name: {TrackName}";

            if (string.IsNullOrEmpty(Author) && string.IsNullOrEmpty(TrackName))
                msg += "\r\n- Latest 10 tracks.";

            if (SelectedEnvironment?.Id != 0)
                msg += $"\r\n- Environment: {SelectedEnvironment?.Name}";

            return msg;
        }
    }
}
