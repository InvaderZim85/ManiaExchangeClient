using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ManiaExchangeClient.DataObjects;
using RestSharp;

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
                var client = new RestClient(baseUrl);
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
        /// <param name="author">The author</param>
        /// <returns>The list of tracks</returns>
        public async Task<List<Track>> LoadTracks(string author)
        {
            var limit = 50;
            var page = 1;

            var result = new List<Track>();

            var initData = await LoadTracks(author, limit, page);

            if (initData == null)
                return result;

            result.AddRange(initData.Results);

            var maxEntries = Math.Round(initData.TotalItemCount / (double)limit);

            for (page = 2; page <= maxEntries; page++)
            {
                var data = await LoadTracks(author, limit, page);

                if (data != null)
                    result.AddRange(data.Results);
            }

            return result;
        }

        /// <summary>
        /// Loads a list of tracks of the author
        /// </summary>
        /// <param name="author">The author name</param>
        /// <param name="limit">The limit of results per page</param>
        /// <param name="page">The page</param>
        /// <returns>The list with the tracks</returns>
        private async Task<TrackList> LoadTracks(string author, int limit, int page)
        {
            // Step 0: Get the endpoint
            var endpoint = _settings.Endpoints.FirstOrDefault(f => f.Type == EndpointType.TrackSearch);

            if (endpoint == null)
                throw new MissingEndpointException(EndpointType.TrackSearch);

            // Step 1: Create the request
            var request = new RestRequest(Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("api", "on");
            request.AddParameter("limit", limit);
            request.AddParameter("author", author);
            request.AddParameter("page", page);

            return await ExecuteAsync<TrackList>(request, endpoint.Path);
        }
    }
}
