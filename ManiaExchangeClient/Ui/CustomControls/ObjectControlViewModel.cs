using System.Collections.ObjectModel;
using MahApps.Metro.Controls.Dialogs;
using ManiaExchangeClient.Business;
using ManiaExchangeClient.DataObjects;
using WpfUtility.Services;

namespace ManiaExchangeClient.Ui.CustomControls
{
    class ObjectControlViewModel : ObservableObject
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
        /// Backing field for <see cref="ObjectList"/>
        /// </summary>
        private ObservableCollection<EmbeddedObject> _objectList;

        /// <summary>
        /// Gets or sets the object list
        /// </summary>
        public ObservableCollection<EmbeddedObject> ObjectList
        {
            get => _objectList;
            set => SetField(ref _objectList, value);
        }

        /// <summary>
        /// Backing field for <see cref="ObjectHeader"/>
        /// </summary>
        private string _objectHeader = "Objects";

        /// <summary>
        /// Gets or sets the object header 
        /// </summary>
        public string ObjectHeader
        {
            get => _objectHeader;
            set => SetField(ref _objectHeader, value);
        }

        /// <summary>
        /// Init the view model
        /// </summary>
        /// <param name="dialogCoordinator">The dialog coordinator</param>
        /// <param name="trackId">The track id</param>
        public void InitViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;

            _restManager = new RestManager();
        }

        /// <summary>
        /// Loads the embedded objects for the given track id
        /// </summary>
        /// <param name="trackId">The track id</param>
        public void LoadObjects(int trackId)
        {
            _trackId = trackId;

            LoadObjects();
        }

        /// <summary>
        /// Loads the objects and shows them
        /// </summary>
        private async void LoadObjects()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Loading embedded objects - Please wait...", "");
            controller.SetIndeterminate();

            var data = await _restManager.LoadEmbeddedObjects(_trackId);

            if (data != null)
                ObjectList = new ObservableCollection<EmbeddedObject>(data);

            ObjectHeader = $"Objects{(data != null ? $" - {data.Count}" : "")}";

            await controller.CloseAsync();
        }
    }
}
