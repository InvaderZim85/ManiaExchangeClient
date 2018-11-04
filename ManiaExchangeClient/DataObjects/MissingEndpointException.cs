using System;

namespace ManiaExchangeClient.DataObjects
{
    public class MissingEndpointException : Exception
    {
        /// <summary>
        /// Creates a new instance of the <see cref="MissingEndpointException"/>
        /// </summary>
        /// <param name="endpoint">The name of the endpoint</param>
        public MissingEndpointException(string endpoint) : base(endpoint) { }

        /// <summary>
        /// Creates a new instance of the <see cref="MissingEndpointException"/>
        /// </summary>
        /// <param name="endpoint">The endpoint</param>
        public MissingEndpointException(EndpointType endpoint) : base(endpoint.ToString()) { }
    }
}
