using Microsoft.Extensions.Logging;
using PixApi.Helpers.Serialization;
using PixApi.Services.Interfaces;
using PixApi.Services.Repositories;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PixApi.Helpers.Utility
{
    public class BaseRepository
    {
        private static Random random = new Random();
        internal const int MAX_RETRIES = 3;

        internal AsyncRetryPolicy<HttpResponseMessage> asyncRetryPolicy = Policy.HandleResult<HttpResponseMessage>(ex => (int)ex.StatusCode > 499 || (int)ex.StatusCode < 600)
                                                                                .WaitAndRetryAsync(retryCount: MAX_RETRIES,
                                                                                                   sleepDurationProvider: (attemptCount) => TimeSpan.FromMilliseconds(attemptCount * 500),
                                                                                                   onRetry: (status, sleepDuration, attemptNumber, context) =>
                                                                                                   {
                                                                                                       Console.WriteLine($"Error on response (httpStatusCode: {status}). Retrying in {sleepDuration}. {attemptNumber} / {MAX_RETRIES}");
                                                                                                   });

        /// <summary>
        /// Default HttpClient instance.
        /// </summary>
        internal static readonly HttpClient _httpClientSharedInstance = new HttpClient();

        /// <summary>
        /// Http client used under-the-hood for all request.
        /// </summary>
        internal HttpClient _httpClient { get; set; } = _httpClientSharedInstance;

        internal bool _isProduction { get; set; } = false;

        internal int _requestTimeoutInSeconds { get; set; } = 300;

        /// <summary>
        /// 
        /// </summary>
        internal string _token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal ISerializer _serializer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal IAuthentication _authentication { get; set; }

        internal string _baseUrl
        {
            get
            {
                if (_isProduction)
                {
                    return Constants.ProdApiBase;
                }
                else
                {
                    return Constants.HmlApiBase;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationData"></param>
        public BaseRepository(Models.Requests.Authorization authenticationData)
        {
            _httpClient = new HttpClient();
            _authentication = new Authentication(authenticationData);
            _serializer = new JsonEntitySerializer();
            _isProduction = authenticationData.is_production;
            _requestTimeoutInSeconds = authenticationData.request_timeout_in_seconds;

            _token = _authentication.GetTokenAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authenticationData"></param>
        public BaseRepository(HttpClient httpClient, Models.Requests.Authorization authenticationData)
        {
            _httpClient = httpClient;
            _authentication = new Authentication(authenticationData);
            _serializer = new JsonEntitySerializer();
            _isProduction = authenticationData.is_production;
            _requestTimeoutInSeconds = authenticationData.request_timeout_in_seconds;

            _token = _authentication.GetTokenAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authentication"></param>
        public BaseRepository(HttpClient httpClient, IAuthentication authentication)
        {
            _httpClient = httpClient;
            _authentication = authentication;
            _serializer = new JsonEntitySerializer();
            _isProduction = authentication.IsProduction();
            _requestTimeoutInSeconds = authentication.RequestTimeoutInSeconds();

            _token = _authentication.GetTokenAsync().GetAwaiter().GetResult();
        }


        public async Task<Models.Responses.Bacen.Envelope<T>> EnvelopeRequest<T>(string path, HttpMethod httpMethod,
                                                                            object content = null, Dictionary<string, string> parameters = null, bool retryFlow = false)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource(_requestTimeoutInSeconds * 1000);

            path = path.TrimStart('/');
            string requestUriWithQuery = _baseUrl + path;

            if (parameters != null && parameters.Count > 0)
            {
                requestUriWithQuery = Extensions.GetUriWithQueryString(_baseUrl + path, parameters);
            }

            HttpResponseMessage responseData;
            if (!retryFlow)
            {
                responseData = await SendAsync(httpMethod, requestUriWithQuery, tokenSource, content);
            }
            else
            {
                responseData = await asyncRetryPolicy.ExecuteAsync(async () =>
                {
                    return await SendAsync(httpMethod, requestUriWithQuery, tokenSource, content);
                });
            }

            var responseContent = await responseData?.Content?.ReadAsStringAsync();

            if (!responseData.IsSuccessStatusCode)
            {
                return new Models.Responses.Bacen.Envelope<T>(responseContent.ToError());
            }

            var result = new Models.Responses.Bacen.Envelope<T>(_serializer.Deserialize<T>(responseContent));

            return result;
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string requestUriWithQuery, CancellationTokenSource tokenSource, object content = null)
        {
            var httpRequest = new CustomHttpRequestMessage(httpMethod, requestUriWithQuery, _token);

            if (content != null && httpMethod != HttpMethod.Get)
            {
                httpRequest.Content = new StringContent(_serializer.Serialize(content), Encoding.UTF8, "application/json");
            }

            try
            {
                return await _httpClient.SendAsync(httpRequest, tokenSource.Token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                throw new TaskCanceledException("The request has timeout. Increase 'Authorization.request_timeout_in_seconds'.");
            }
        }

        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
