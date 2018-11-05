using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ManiaExchangeClient.DataObjects;
using Newtonsoft.Json;
using Environment = ManiaExchangeClient.DataObjects.Environment;

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
        public static string GetBaseFolder()
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

        /// <summary>
        /// Gets the list with the environments
        /// </summary>
        /// <returns>The list with the environments</returns>
        public static List<Environment> EnvironmentList()
        {
            return new List<Environment>
            {
                new Environment {Id = 0, Name = "All"},
                new Environment {Id = 1, Name = "Canyon"},
                new Environment {Id = 2, Name = "Stadium"},
                new Environment {Id = 3, Name = "Valley"},
                new Environment {Id = 4, Name = "Lagoon"}
            };
        }

        /// <summary>
        /// Gets the information of the given assembly
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <returns>The information of the given assembly</returns>
        /// <exception cref="System.ArgumentNullException">Will be thrown when the given assembly is null</exception>
        public static AssemblyModel GetInformation(this Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var result = new AssemblyModel();

            // Title
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var title = (AssemblyTitleAttribute)attributes[0];
                result.AssemblyTitle = title?.Title ?? "";
            }
            else
                result.AssemblyTitle = Path.GetFileNameWithoutExtension(assembly.CodeBase);

            // Version
            result.AssemblyVersion = assembly.GetName().Version.ToString();

            // Description
            attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            result.AssemblyDescription = attributes.Length == 0
                ? ""
                : ((AssemblyDescriptionAttribute)attributes[0]).Description;

            // Productinformations
            attributes = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            result.AssemblyProduct = attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;

            // Copyright
            attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            result.AssemblyCopyright = attributes.Length == 0
                ? ""
                : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            // Company
            attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            result.AssemblyCompany = attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;

            // Configuration
            attributes = assembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
            result.AssemblyConfiguration = attributes.Length == 0
                ? ""
                : ((AssemblyConfigurationAttribute)attributes[0]).Configuration;

            // Trademark
            attributes = assembly.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);
            result.AssemblyTrademark = attributes.Length == 0
                ? ""
                : ((AssemblyTrademarkAttribute)attributes[0]).Trademark;

            // Culture
            attributes = assembly.GetCustomAttributes(typeof(AssemblyCultureAttribute), false);
            result.AssemblyCulture = attributes.Length == 0 ? "" : ((AssemblyCultureAttribute)attributes[0]).Culture;

            result.AssemblyDate = assembly.GetBuildDate();

            return result;
        }

        /// <summary>
        /// Returns the build date of an assembly
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <returns>The DateTime-Value of the assembly</returns>
        /// <exception cref="ArgumentException">Will be thrown, if the given assembly doesn't exist.</exception>
        private static DateTime GetBuildDate(this Assembly assembly)
        {
            if (!File.Exists(assembly.Location))
                throw new ArgumentException("The assembly doesn't exist.");

            var buffer = new byte[Math.Max(Marshal.SizeOf(typeof(ImageFileHeader)), 4)];
            using (var fileStream = new FileStream(assembly.Location, FileMode.Open, FileAccess.Read))
            {
                fileStream.Position = 0x3C;
                fileStream.Read(buffer, 0, 4);
                fileStream.Position = BitConverter.ToUInt32(buffer, 0); // COFF header offset
                fileStream.Read(buffer, 0, 4); // "PE\0\0"
                fileStream.Read(buffer, 0, buffer.Length);
            }
            var pinnedBuffer = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                var coffHeader = (ImageFileHeader)Marshal.PtrToStructure(pinnedBuffer.AddrOfPinnedObject(), typeof(ImageFileHeader));

                return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1) + new TimeSpan(coffHeader.TimeDateStamp * TimeSpan.TicksPerSecond));
            }
            finally
            {
                pinnedBuffer.Free();
            }
        }

        /// <summary>
        /// Struct to interact with the build date
        /// </summary>
        private struct ImageFileHeader
        {

#pragma warning disable 169, 649
            public ushort Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public ushort Characteristics;
#pragma warning restore 169, 649

        }
    }
}