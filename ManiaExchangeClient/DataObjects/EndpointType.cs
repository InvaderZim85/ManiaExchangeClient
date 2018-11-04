namespace ManiaExchangeClient.DataObjects
{
    /// <summary>
    /// Contains the different api call types
    /// </summary>
    public enum EndpointType
    {
        /// <summary>
        /// The track search
        /// </summary>
        TrackSearch,

        /// <summary>
        /// Type to load information of a specified track
        /// </summary>
        TrackInformation,

        /// <summary>
        /// Type to download a specified track
        /// </summary>
        TrackDownload,

        /// <summary>
        /// Gets the latest 10 tracks
        /// </summary>
        LatestTracks,

        /// <summary>
        /// Gets the world records for a specified track
        /// </summary>
        WorldRecord,

        /// <summary>
        /// Gets a specified amount (max 25) replays for a specified track
        /// </summary>
        GetReplays,

        /// <summary>
        /// Gets a collection of embedded objects for the specified track
        /// </summary>
        GetEmbeddedObjects,

        /// <summary>
        /// Gets the screenshots of the specified track
        /// </summary>
        Screenshot,

        /// <summary>
        /// Gets the thumbnail of the specified track
        /// </summary>
        Thumbnail
    }
}