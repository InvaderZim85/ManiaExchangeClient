using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ManiaExchangeClient.Ui.CustomControls
{
    /// <summary>
    /// Interaction logic for ObjectControl.xaml
    /// </summary>
    public partial class ObjectControl : UserControl
    {
        /// <summary>
        /// Contains the current track id
        /// </summary>
        private int _trackId;

        /// <summary>
        /// Creates a new instance of the object
        /// </summary>
        public ObjectControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init the control
        /// </summary>
        public void InitControl()
        {
            if (DataContext is ObjectControlViewModel viewModel)
                viewModel.InitViewModel(DialogCoordinator.Instance);
        }

        /// <summary>
        /// Loads the data for the given track id
        /// </summary>
        /// <param name="trackId">The track id</param>
        public void LoadData(int trackId)
        {
            if (_trackId == trackId)
                return;

            _trackId = trackId;

            if (DataContext is ObjectControlViewModel viewModel)
                viewModel.LoadObjects(_trackId);
        }
    }
}
