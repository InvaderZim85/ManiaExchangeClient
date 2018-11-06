using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ManiaExchangeClient.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the user hits the close menu (Program > Close)
        /// </summary>
        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Occurs when the main form is loading
        /// </summary>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!(DataContext is MainWindowViewModel viewModel))
                return;

            viewModel.InitViewModel(DialogCoordinator.Instance);
            ObjectControl.InitControl();
            ReplayControl.InitControl();

            viewModel.SelectionChanged += ViewModel_SelectionChanged;
        }

        /// <summary>
        /// Occurs when the user selects another track
        /// </summary>
        private void ViewModel_SelectionChanged()
        {
            if (!(DataContext is MainWindowViewModel viewModel))
                return;

            if (viewModel.SelectedTrack == null)
                return;

            if (TabControl.SelectedIndex == 2 && viewModel.SelectedTrack.ReplayCount == 0)
            {
                TabControl.SelectedIndex = 0;
            }
            else if (TabControl.SelectedIndex == 3 && viewModel.SelectedTrack.EmbeddedObjectsCount == 0)
            {
                TabControl.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Occurs when the user selects another tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(DataContext is MainWindowViewModel viewModel))
                return;

            if (viewModel.SelectedTrack == null)
                return;

            if (TabControl.SelectedIndex == 2) // Replay tab
            {
                ReplayControl.LoadData(viewModel.SelectedTrack.TrackId);
            }
            else if (TabControl.SelectedIndex == 3) // Object tab
            {
                ObjectControl.LoadData(viewModel.SelectedTrack.TrackId);
            }
            
        }
    }
}
