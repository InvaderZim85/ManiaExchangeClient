using System.Collections.Generic;

namespace ManiaExchangeClient.DataObjects
{
    public class SettingsModel
    {
        /// <summary>
        /// Gets or sets the list of endpoints
        /// </summary>
        public List<Endpoint> Endpoints { get; set; } = new List<Endpoint>();
    }
}
