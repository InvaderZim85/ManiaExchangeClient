using System.Windows;
using MahApps.Metro;
using Props = ManiaExchangeClient.Properties;

namespace ManiaExchangeClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Occurs when the application is started
        /// </summary>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ThemeManager.ChangeAppStyle(Current, ThemeManager.GetAccent(Props.Settings.Default.Accent),
                ThemeManager.GetAppTheme(Props.Settings.Default.Theme));
        }
    }
}
