using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ManiaExchangeClient.DataObjects;
using RestSharp;
using Environment = ManiaExchangeClient.DataObjects.Environment;

namespace ManiaExchangeClient.Business
{
    public class RestManager
    {
        /// <summary>
        /// Contains the settings
        /// </summary>
        private readonly SettingsModel _settings;

        /// <summary>
        /// Creates a new instance of the <see cref="RestManager"/>
        /// </summary>
        public RestManager()
        {
            _settings = Helper.LoadSettings();
        }

        /// <summary>
        /// Executes a rest call
        /// </summary>
        /// <typeparam name="T">The type of the response</typeparam>
        /// <param name="request">The request</param>
        /// <param name="baseUrl">The base url</param>
        /// <returns>The task with the response</returns>
        private Task<T> ExecuteAsync<T>(RestRequest request, string baseUrl) where T : new()
        {
            try
            {
                var client = new RestClient(baseUrl) {Timeout = (int) TimeSpan.FromMinutes(1).TotalMilliseconds};

                var taskCompletionSource = new TaskCompletionSource<T>();

                Logger.Debug(request.Resource);

                client.ExecuteAsync<T>(request, response =>
                {
                    if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == HttpStatusCode.OK)
                    {
                        Logger.Debug("Response Data:" +
                                     $"\r\n\t- Status.....: {response.ResponseStatus}" +
                                     $"\r\n\t- Status code: {response.StatusCode}");

                        taskCompletionSource.SetResult(response.Data);
                    }
                    else
                    {
                        taskCompletionSource.SetResult(default(T));
                        Logger.Error("An error has occurred during the rest call:" +
                                     $"\r\n\t- Request......: {request.Resource}" +
                                     $"\r\n\t- Error code...: {response.StatusCode}" +
                                     $"\r\n\t- Error message: {response.ErrorMessage}");
                    }
                });

                return taskCompletionSource.Task;
            }
            catch (Exception ex)
            {
                Logger.Error(nameof(ExecuteAsync), ex);
                return null;
            }
        }

        /// <summary>
        /// Loads the tracks of the given author
        /// </summary>
        /// <param name="searchType">The search type</param>
        /// <param name="author">The author</param>
        /// <param name="trackName">The name of the track</param>
        /// <param name="environment">The selected environment</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The list of tracks</returns>
        public async Task<List<Track>> LoadTracks(SearchType searchType, string author, string trackName, Environment environment, CancellationTokenSource cancellationToken)
        {
            var limit = 50;

            if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(trackName) && searchType == SearchType.Filter)
            {
                searchType = SearchType.LatestTracks;
            }

            if (searchType != SearchType.Filter)
                limit = 10;

            var page = 1;

            var result = new List<Track>();

            if (cancellationToken.IsCancellationRequested)
                return new List<Track>();

            var initData = await LoadTracks(searchType, author, trackName, environment, limit, page);

            if (initData == null)
                return result;

            result.AddRange(initData.Results);

            if (searchType != SearchType.Filter)
                return result;

            var maxEntries = Math.Round(initData.TotalItemCount / (double)limit);

            for (page = 2; page <= maxEntries; page++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return result;

                var data = await LoadTracks(searchType, author, trackName, environment, limit, page);

                if (data != null)
                    result.AddRange(data.Results);
            }

            return result;
        }

        /// <summary>
        /// Loads a list of tracks of the author
        /// </summary>
        /// <param name="searchType">The search type</param>
        /// <param name="author">The author name</param>
        /// <param name="trackName">The name of the track</param>
        /// <param name="environment">The selected environment</param>
        /// <param name="limit">The limit of results per page</param>
        /// <param name="page">The page</param>
        /// <param name="showLatest">true when only the latest tracks should be shown, otherwise false</param>
        /// <returns>The list with the tracks</returns>
        private async Task<TrackList> LoadTracks(SearchType searchType, string author, string trackName, Environment environment, int limit, int page)
        {
            // Step 0: Get the endpoint
            var endpoint = GetEndpoint(EndpointType.TrackSearch);

            // Step 1: Create the request
            var request = new RestRequest(Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("api", "on");
            request.AddParameter("limit", limit);

            if (searchType != SearchType.Filter)
            {
                request.AddParameter("mode", (int)searchType);
            }
            else
            {
                if (!string.IsNullOrEmpty(author))
                    request.AddParameter("author", author);
                if (!string.IsNullOrEmpty(trackName))
                    request.AddParameter("trackname", trackName);

                request.AddParameter("page", page);
            }

            if (environment.Id != 0)
                request.AddParameter("environments", environment.Id);

            return await ExecuteAsync<TrackList>(request, endpoint.Path);
        }

        /// <summary>
        /// Loads the embedded objects of a track / map
        /// </summary>
        /// <param name="trackId">The id of the track</param>
        /// <returns>The list with the objects</returns>
        public async Task<List<EmbeddedObject>> LoadEmbeddedObjects(int trackId)
        {
            var endpoint = GetEndpoint(EndpointType.GetEmbeddedObjects);

            var request = new RestRequest("{id}");

            request.AddUrlSegment("id", trackId);

            return await ExecuteAsync<List<EmbeddedObject>>(request, endpoint.Path);
        }

        /// <summary>
        /// Loads the replays for the given track id
        /// </summary>
        /// <param name="trackId">The track id</param>
        /// <returns>The replays</returns>
        public async Task<List<Replay>> LoadReplays(int trackId)
        {
            var endpoint = GetEndpoint(EndpointType.GetReplays);

            var request = new RestRequest("{id}");

            request.AddUrlSegment("id", trackId);

            return await ExecuteAsync<List<Replay>>(request, endpoint.Path);
        }

        /// <summary>
        /// Gets the endpoint data for the given type
        /// </summary>
        /// <param name="type">The desired type</param>
        /// <returns>The endpoint</returns>
        private Endpoint GetEndpoint(EndpointType type)
        {
            var endpoint = _settings.Endpoints.FirstOrDefault(f => f.Type == type);

            if (endpoint == null)
                throw new MissingEndpointException(type);

            return endpoint;
        }
    }
}
