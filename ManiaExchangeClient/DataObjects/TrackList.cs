using System.Collections.Generic;

namespace ManiaExchangeClient.DataObjects
{
    public class TrackList
    {
        /// <summary>
        /// Gets or sets the list with the tracks
        /// </summary>
        public List<Track> Results { get; set; }

        /// <summary>
        /// Gets or sets the count of the containing items
        /// </summary>
        public int TotalItemCount { get; set; }
    }
}
