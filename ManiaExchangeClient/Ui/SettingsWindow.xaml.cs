using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ManiaExchangeClient.Ui
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SettingsWindow"/>
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the form is loading
        /// </summary>
        private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsWindowViewModel viewModel)
            {
                viewModel.InitViewModel(DialogCoordinator.Instance);
                viewModel.CloseWindow += Close;
            }
        }
    }
}
