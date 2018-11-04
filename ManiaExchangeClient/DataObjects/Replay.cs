using System;
using RestSharp.Deserializers;

namespace ManiaExchangeClient.DataObjects
{
    public class Replay
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [DeserializeAs(Name = "ReplayID")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [DeserializeAs(Name = "UserID")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the track id
        /// </summary>
        [DeserializeAs(Name = "TrackID")]
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the uploaded date
        /// </summary>
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// Gets or sets the replay time
        /// </summary>
        public int ReplayTime { get; set; }

        /// <summary>
        /// Gets or sets the stund score
        /// </summary>
        public int StuntScore { get; set; }

        /// <summary>
        /// Gets or sets the respawn amount
        /// </summary>
        public int Respawns { get; set; }

        /// <summary>
        /// Gets or sets the position
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the beaten value
        /// </summary>
        public int Beaten { get; set; }

        /// <summary>
        /// Gets or sets the percentage
        /// </summary>
        public double Percentage { get; set; }

        /// <summary>
        /// Gets or sets the replay points
        /// </summary>
        public double ReplayPoints { get; set; }

        /// <summary>
        /// Gets or sets the nadeo points
        /// </summary>
        public double NadeoPoints { get; set; }
    }
}
