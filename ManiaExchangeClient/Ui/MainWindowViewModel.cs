using System.Collections.ObjectModel;
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
            if (string.IsNullOrEmpty(Author))
                return;

            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Loading",
                $"Loading tracks for the author: \"{Author}\". Please wait...");
            controller.SetIndeterminate();

            var data = await _restManager.LoadTracks("invader_zim");

            if (data != null)
                TrackList = new ObservableCollection<Track>(data);

            await controller.CloseAsync();
        }
    }
}
