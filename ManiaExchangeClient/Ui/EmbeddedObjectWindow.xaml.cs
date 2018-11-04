using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
    }
}
