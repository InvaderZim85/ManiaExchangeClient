using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ManiaExchangeClient.Ui.CustomControls
{
    /// <summary>
    /// Interaction logic for ReplayControl.xaml
    /// </summary>
    public partial class ReplayControl : UserControl
    {
        /// <summary>
        /// Contains the track id
        /// </summary>
        private int _trackId;

        /// <summary>
        /// Creates a new instance of <see cref="ReplayControl"/>
        /// </summary>
        public ReplayControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init the control
        /// </summary>
        public void InitControl()
        {
            if (DataContext is ReplayControlViewModel viewModel)
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

            if (DataContext is ReplayControlViewModel viewModel)
                viewModel.LoadReplays(_trackId);
        }
    }
}
