using PixApi.Helpers.Serialization;
using PixApi.Helpers.Utility.Exceptions;
using PixApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PixApi.Services.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class Authentication : IAuthentication
    {
        private readonly HttpClient _httpClient;
        private readonly Models.Requests.Authorization _authorization;
        public ISerializer _serializer { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        public Authentication(Models.Requests.Authorization authorization)
        {
            _httpClient = new HttpClient();
            _serializer = new JsonEntitySerializer();

            _authorization = authorization;
        }

        public bool IsProduction()
        {
            return _authorization.is_production;
        }

        public int RequestTimeoutInSeconds()
        {
            return _authorization.request_timeout_in_seconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTokenAsync()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, (_authorization.is_production ? Constants.ProdApiBase : Constants.HmlApiBase) + "cm/authentication/token/");
            httpRequest.Headers.Add("User-Agent", Constants.UserAgent);
            httpRequest.Headers.Add("Accept", "application/json");

            var dict = new Dictionary<string, string>();
            dict.Add("client_id", _authorization.client_id);
            dict.Add("client_secret", _authorization.client_secret);
            dict.Add("grant_type", "client_credentials");

            httpRequest.Content = new FormUrlEncodedContent(dict);

            var responseData = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);

            var responseContent = await responseData?.Content?.ReadAsStringAsync();

            if (!responseData.IsSuccessStatusCode)
            {
                throw new BtgTokenException("There is an issue on token response.", responseContent);
            }

            var result = _serializer.Deserialize<Models.Responses.Authorization>(responseContent);

            return result?.access_token;
        }
    }
}
