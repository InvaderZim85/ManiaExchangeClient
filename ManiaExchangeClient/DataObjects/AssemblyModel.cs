using System;

namespace ManiaExchangeClient.DataObjects
{
    /// <summary>
    /// Provides information about an assembly
    /// </summary>
    public class AssemblyModel
    {
        /// <summary>
        /// Gets or sets the title of the assembly
        /// </summary>
        public string AssemblyTitle { get; set; }
        /// <summary>
        /// Gets or sets the version of the assembly
        /// </summary>
        public string AssemblyVersion { get; set; }
        /// <summary>
        /// Gets or sets the description of the assembly
        /// </summary>
        public string AssemblyDescription { get; set; }
        /// <summary>
        /// Gets or sets the product information of the assembly
        /// </summary>
        public string AssemblyProduct { get; set; }
        /// <summary>
        /// Gets or sets the copyright information of the assembly
        /// </summary>
        public string AssemblyCopyright { get; set; }
        /// <summary>
        /// Gets or sets the company name of the assembly
        /// </summary>
        public string AssemblyCompany { get; set; }
        /// <summary>
        /// Gets or sets the configuration of the assembly
        /// </summary>
        public string AssemblyConfiguration { get; set; }
        /// <summary>
        /// Gets or sets the trademark of the assembly
        /// </summary>
        public string AssemblyTrademark { get; set; }
        /// <summary>
        /// Gets or sets the culture of the assembly
        /// </summary>
        public string AssemblyCulture { get; set; }
        /// <summary>
        /// Gets or sets the build date of the assembly
        /// </summary>
        public DateTime AssemblyDate { get; set; }
    }
}
