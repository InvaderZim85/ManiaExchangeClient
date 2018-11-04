using System.IO;
using System.Reflection;
using ManiaExchangeClient.DataObjects;
using Newtonsoft.Json;

namespace ManiaExchangeClient
{
    public static class Helper
    {
        /// <summary>
        /// Contains the name of the settings file
        /// </summary>
        private const string SettingsFile = "Settings.json";

        /// <summary>
        /// Gets the path of the base folder, for the application that is currently running
        /// </summary>
        /// <returns>The path of the base folder</returns>
        private static string GetBaseFolder()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Loads the settings 
        /// </summary>
        /// <returns>The settings</returns>
        public static SettingsModel LoadSettings()
        {
            var path = Path.Combine(GetBaseFolder(), SettingsFile);

            if (!File.Exists(path))
                return new SettingsModel();

            var jsonString = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<SettingsModel>(jsonString);
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>true when successful, otherwise false</returns>
        public static bool SaveSettings(SettingsModel settings)
        {
            if (settings == null)
                return false;

            var path = Path.Combine(GetBaseFolder(), SettingsFile);

            var jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(path, jsonString);

            return File.Exists(path);
        }
    }
}
