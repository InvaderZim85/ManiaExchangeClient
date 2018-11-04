using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
using ManiaExchangeClient.Business;
using ManiaExchangeClient.DataObjects;
using WpfUtility.Services;

namespace ManiaExchangeClient.Ui
{
    public class ReplayWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Contains the dialog coordinator
        /// </summary>
        private IDialogCoordinator _dialogCoordinator;

        /// <summary>
        /// Contains the instance of the <see cref="RestManager"/>
        /// </summary>
        private RestManager _restManager;

        /// <summary>
        /// Contains the track id
        /// </summary>
        private int _trackId;

        /// <summary>
        /// Backing field for <see cref="ReplayList"/>
        /// </summary>
        private ObservableCollection<Replay> _replayList;

        /// <summary>
        /// Gets or sets the object list
        /// </summary>
        public ObservableCollection<Replay> ReplayList
        {
            get => _replayList;
            set => SetField(ref _replayList, value);
        }

        /// <summary>
        /// Backing field for <see cref="ReplayHeader"/>
        /// </summary>
        private string _replayHeader = "Replays";

        /// <summary>
        /// Gets or sets the object header 
        /// </summary>
        public string ReplayHeader
        {
            get => _replayHeader;
            set => SetField(ref _replayHeader, value);
        }

        /// <summary>
        /// Init the view model
        /// </summary>
        /// <param name="dialogCoordinator">The dialog coordinator</param>
        /// <param name="trackId">The track id</param>
        public void InitViewModel(IDialogCoordinator dialogCoordinator, int trackId)
        {
            _dialogCoordinator = dialogCoordinator;

            _restManager = new RestManager();

            _trackId = trackId;

            LoadObjects();
        }

        /// <summary>
        /// Loads the objects and shows them
        /// </summary>
        private async void LoadObjects()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Loading replays - Plase wait...", "");
            controller.SetIndeterminate();

            var data = await _restManager.LoadReplays(_trackId);

            if (data != null)
                ReplayList = new ObservableCollection<Replay>(data);

            ReplayHeader = $"Replays{(data != null ? $" - {data.Count}" : "")}";

            await controller.CloseAsync();
        }
    }
}
