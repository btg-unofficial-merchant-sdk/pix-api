using PixApi.Helpers.Utility;
using PixApi.Models.Responses.Bacen;
using PixApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Services.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class Location : BaseRepository, ILocation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationData"></param>
        public Location(Models.Requests.Authorization authenticationData) : base(authenticationData) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authenticationData"></param>
        public Location(HttpClient httpClient, Models.Requests.Authorization authenticationData) : base(httpClient, authenticationData) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authentication"></param>
        public Location(HttpClient httpClient, IAuthentication authentication) : base(httpClient, authentication) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Location>> GetAsync(long id)
        {
            var response = await this.EnvelopeRequest<Models.Responses.Bacen.Location>(MerchantPaths.Location + id, HttpMethod.Get);

            return response;
        }

        /// <summary>
        /// Create a location
        /// </summary>
        /// <param name="collectionType"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Location>> CreateAsync(CollectionType collectionType)
        {
            var response = await this.EnvelopeRequest<Models.Responses.Bacen.Location>(MerchantPaths.Location, HttpMethod.Post, _serializer.Deserialize<object>("{ \"tipoCob\": \"" + collectionType + "\" }"));

            return response;
        }

        /// <summary>
        /// Listing collection
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ListLocation>> ListAsync(Models.Requests.Bacen.ListLocation parameters)
        {
            var query = new Dictionary<string, string>();
            query.Add("inicio", parameters.inicio);
            query.Add("fim", parameters.fim);
            
            if (parameters.tipoCob.HasValue)
                query.Add("tipoCob", parameters.tipoCob.ToString());
            
            if (parameters.paginaAtual > 0)
                query.Add("paginacao.paginaAtual", parameters.paginaAtual.ToString());

            if (parameters.itensPorPagina > 0)
                query.Add("paginacao.itensPorPagina", parameters.itensPorPagina.ToString());

            var response = await this.EnvelopeRequest<Models.Responses.Bacen.ListLocation>(MerchantPaths.Location, HttpMethod.Get, parameters: query);

            return response;
        }

        public async Task<Envelope<Models.Responses.Bacen.Location>> UnlinkCollectionAsync(long id)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.Location>(MerchantPaths.Location + id + "/txid", HttpMethod.Delete);
        }
    }
}
