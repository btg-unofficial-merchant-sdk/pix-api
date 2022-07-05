using System;
using System.Net.Http;

namespace PixApi.Helpers.Utility
{
    public class CustomHttpRequestMessage : HttpRequestMessage
    {
        public CustomHttpRequestMessage(HttpMethod httpMethod, string endpoint, string token) : base(httpMethod, endpoint)
        {
            this.RequestUri = new Uri(endpoint);
            this.Headers.Add("User-Agent", Constants.UserAgent);
            this.Headers.Add("Authorization", "Bearer " + token);
            this.Headers.Add("Accept", "application/json");
        }
    }
}
