namespace ManiaExchangeClient.DataObjects
{
    public class Endpoint
    {
        /// <summary>
        /// Gets or sets the path of the endpoint
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the type of the endpoint
        /// </summary>
        public EndpointType Type { get; set; }

        /// <summary>
        /// Creates a new empty instance of <see cref="Endpoint"/>
        /// </summary>
        public Endpoint() { }

        /// <summary>
        /// Creates a new instance of <see cref="Endpoint"/>
        /// </summary>
        /// <param name="path">The path of the endpoint</param>
        /// <param name="type">The type</param>
        public Endpoint(string path, EndpointType type)
        {
            Path = path;
            Type = type;
        }
    }
}
