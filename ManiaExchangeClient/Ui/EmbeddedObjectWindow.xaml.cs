using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ManiaExchangeClient.Ui
{
    /// <summary>
    /// Interaction logic for EmbeddedObjectWindow.xaml
    /// </summary>
    public partial class EmbeddedObjectWindow : MetroWindow
    {
        /// <summary>
        /// COntains the track id
        /// </summary>
        private readonly int _trackId;

        /// <summary>
        /// Creates a new instance of the <see cref="EmbeddedObjectWindow"/>
        /// </summary>
        /// <param name="trackId">The id of the track</param>
        public EmbeddedObjectWindow(int trackId)
        {
            InitializeComponent();

            _trackId = trackId;
        }

        /// <summary>
        /// Occurs when the window is loading
        /// </summary>
        private void EmbeddedObjectWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is EmbeddedObjectWindowViewModel viewModel)
                viewModel.InitViewModel(DialogCoordinator.Instance, _trackId);
        }

        /// <summary>
        /// Occurs when the user hits the close button
        /// </summary>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
