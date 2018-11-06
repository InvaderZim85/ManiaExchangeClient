namespace ManiaExchangeClient.DataObjects
{
    public enum SearchType
    {
        /// <summary>
        /// Gets the tracks by the filter
        /// </summary>
        Filter = 0,

        /// <summary>
        /// Gets the last 10 tracks
        /// </summary>
        LatestTracks = 2,

        /// <summary>
        /// Gets the last 10 awarded tracks
        /// </summary>
        RecentlyAwarded = 3,

        /// <summary>
        /// Gets the best 10 tracks of the week
        /// </summary>
        BestOfTheWeek = 4,

        /// <summary>
        /// Gets the best 10 tracks of the month
        /// </summary>
        BestOfTheMonth = 5,

        /// <summary>
        /// Gets the best 10 competitive tracks of the week
        /// </summary>
        CompetitiveTracksOfTheWeek = 19,

        /// <summary>
        /// Gets the best 10 competitive tracks of the month
        /// </summary>
        CompetitiveTracksOfTheMonth = 20
    }
}
