using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ManiaExchangeClient.Ui
{
    /// <summary>
    /// Interaction logic for ReplayWindow.xaml
    /// </summary>
    public partial class ReplayWindow : MetroWindow
    {
        /// <summary>
        /// Contains the track id
        /// </summary>
        private readonly int _trackId;

        /// <summary>
        /// Creates a new instance of the <see cref="ReplayWindow"/>
        /// </summary>
        /// <param name="trackId">The track id</param>
        public ReplayWindow(int trackId)
        {
            InitializeComponent();

            _trackId = trackId;
        }

        /// <summary>
        /// Occurs when the user hits the close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Occurs when the window is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplayWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ReplayWindowViewModel viewModel)
                viewModel.InitViewModel(DialogCoordinator.Instance, _trackId);
        }
    }
}
