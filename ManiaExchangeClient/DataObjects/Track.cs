using System;
using RestSharp.Deserializers;

namespace ManiaExchangeClient.DataObjects
{
    public class Track
    {
        /// <summary>
        /// Gets or sets the id of the track
        /// </summary>
        [DeserializeAs(Name = "TrackID")]
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the user id of the mapper
        /// </summary>
        [DeserializeAs(Name = "UserID")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the mapper
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the date when the track was uploaded
        /// </summary>
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// Gets or sets the date when the track was updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the name of the track
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the track type
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the type of the map / track
        /// </summary>
        public string MapType { get; set; }

        /// <summary>
        /// Gets or sets the name of the title pack
        /// </summary>
        public string TitlePack { get; set; }

        /// <summary>
        /// Gets or sets the name of the style
        /// </summary>
        public string StyleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the mood
        /// </summary>
        public string Mood { get; set; }

        /// <summary>
        /// Gets or sets the display cost value (former coppers)
        /// </summary>
        public int DisplayCost { get; set; }

        /// <summary>
        /// Gets or sets the name of the used mod
        /// </summary>
        public string ModName { get; set; }

        /// <summary>
        /// Gets or sets the lightmap id
        /// </summary>
        public int Lightmap { get; set; }

        /// <summary>
        /// Gets or sets the version of maniaplanet with which the map was build
        /// </summary>
        public string ExeVersion { get; set; }

        /// <summary>
        /// Gets or sets the build version of maniaplanet with which the track was build
        /// </summary>
        public string ExeBuild { get; set; }

        /// <summary>
        /// Gets or sets the environment name
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Gets or sets the vehicle name
        /// </summary>
        public string VehicleName { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the unlimiter tool is required for the track
        /// </summary>
        public bool UnlimiterRequired { get; set; }

        /// <summary>
        /// Gets or sets the route name
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// Gets or sets the length name
        /// </summary>
        public string LengthName { get; set; }

        /// <summary>
        /// Gets or sets the amount of laps
        /// </summary>
        public int Laps { get; set; }

        /// <summary>
        /// Gets or sets the name of the difficulty
        /// </summary>
        public string DifficultyName { get; set; }

        /// <summary>
        /// Gets or sets the replay type name
        /// </summary>
        public object ReplayTypeName { get; set; }

        /// <summary>
        /// Gets or sets the replay world record id
        /// </summary>
        [DeserializeAs(Name = "ReplayWRID")]
        public int? ReplayWrId { get; set; }

        /// <summary>
        /// Gets or sets the amount of replays
        /// </summary>
        public int ReplayCount { get; set; }

        /// <summary>
        /// Gets or sets the track value
        /// </summary>
        public int TrackValue { get; set; }

        /// <summary>
        /// Gets or sets the track comment
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the award count
        /// </summary>
        public int AwardCount { get; set; }

        /// <summary>
        /// Gets or sets the amount of comments
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// Gets or sets the world record time (value is in milliseconds)
        /// </summary>
        [DeserializeAs(Name = "ReplayWRTime")]
        public int? ReplayWrTime { get; set; }

        /// <summary>
        /// Gets or sets the user id of the user who droves the wr
        /// </summary>
        [DeserializeAs(Name = "ReplayWRUserID")]
        public int? ReplayWrUserId { get; set; }

        /// <summary>
        /// Gets or sets the username of the user who droves the wr
        /// </summary>
        [DeserializeAs(Name = "ReplayWRUsername")]
        public string ReplayWrUsername { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the track is unreleased
        /// </summary>
        public bool Unreleased { get; set; }

        /// <summary>
        /// Gets or sets the gbx track name
        /// </summary>
        public string GbxMapName { get; set; }

        /// <summary>
        /// Gets or sets the rating vote count
        /// </summary>
        public int RatingVoteCount { get; set; }

        /// <summary>
        /// Gets or sets the average vote count
        /// </summary>
        public int RatingVoteAverage { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the track
        /// </summary>
        [DeserializeAs(Name = "TrackUID")]
        public string TrackUid { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the track has a screenshot
        /// </summary>
        public bool HasScreenshot { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the track has a thumbnail
        /// </summary>
        public bool HasThumbnail { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the track has ghost blocks
        /// </summary>
        public bool HasGhostBlocks { get; set; }

        /// <summary>
        /// Gets or sets the amount of embedded objects
        /// </summary>
        public int EmbeddedObjectsCount { get; set; }

        /// <summary>
        /// Gets or sets the login of the author
        /// </summary>
        public string AuthorLogin { get; set; }

        /// <summary>
        /// Gets or sets the value which indicates if the map is an MP4 map
        /// </summary>
        [DeserializeAs(Name = "IsMP4")]
        public bool IsMp4 { get; set; }
    }
}
