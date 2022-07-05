using PixApi.Helpers.Utility;
using PixApi.Models.Responses.Bacen;
using PixApi.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Services.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class Reversal : BaseRepository, IReversal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationData"></param>
        public Reversal(Models.Requests.Authorization authenticationData) : base(authenticationData) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authenticationData"></param>
        public Reversal(HttpClient httpClient, Models.Requests.Authorization authenticationData) : base(httpClient, authenticationData) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authentication"></param>
        public Reversal(HttpClient httpClient, IAuthentication authentication) : base(httpClient, authentication) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endToEndId"></param>
        /// <param name="clientRequestId"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Reversal>> GetAsync(string endToEndId, string clientRequestId)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.Reversal>(MerchantPaths.Incoming + endToEndId + "/devolucao/" + clientRequestId, HttpMethod.Get);
        }

       /// <summary>
       /// Create a reversal
       /// </summary>
       /// <param name="request"></param>
       /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Reversal>> CreateAsync(Models.Requests.Bacen.CreateReversal request)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.Reversal>(MerchantPaths.Incoming + request.endToEndId + "/devolucao/" + request.clientRequestId, HttpMethod.Put, 
                _serializer.Deserialize<object>("{ \"valor\":\"" + request.valor + "\" }"));
        }

        
    }
}
