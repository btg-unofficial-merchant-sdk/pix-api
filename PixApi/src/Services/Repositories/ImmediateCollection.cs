using Microsoft.Extensions.Logging;
using PixApi.Helpers.Utility;
using PixApi.Models.Requests.Bacen;
using PixApi.Models.Responses.Bacen;
using PixApi.Services.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Services.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ImmediateCollection : BaseRepository, IImmediateCollection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationData"></param>
        public ImmediateCollection(Models.Requests.Authorization authenticationData) : base(authenticationData)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authenticationData"></param>
        /// <param name="logger"></param>
        public ImmediateCollection(HttpClient httpClient, Models.Requests.Authorization authenticationData) : base(httpClient, authenticationData)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authentication"></param>
        public ImmediateCollection(HttpClient httpClient, IAuthentication authentication) : base(httpClient, authentication)
        { }

        /// <summary>
        /// 
        /// </summary>
        
        /// <param name="txId"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> GetAsync(string txId)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.ImmediateCollection>(MerchantPaths.ImmediateCollection + txId, HttpMethod.Get);
        }

        /// <summary>
        /// Review 
        /// </summary>
        
        /// <param name="txId"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> UpdateAsync(string txId, Models.Requests.Bacen.UpdateImmediateCollection body)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.ImmediateCollection>(MerchantPaths.ImmediateCollection + txId, new HttpMethod("PATCH"), body);
        }

        /// <summary>
        /// Create a collection giving a custom txId
        /// </summary>
        
        /// <param name="txId">Required</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> CreateAsync(string txId, Models.Requests.Bacen.ImmediateCollection body)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.ImmediateCollection>(MerchantPaths.ImmediateCollection + txId, HttpMethod.Put, body);
        }

        /// <summary>
        /// Create a collection with txId generated automatically
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> CreateAsync(Models.Requests.Bacen.ImmediateCollection body)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.ImmediateCollection>(MerchantPaths.ImmediateCollection, HttpMethod.Post, body);
        }

        /// <summary>
        /// Create a collection with location generated automatically. 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="txId">If null generated automatically</param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> CreateWithLocationAsync(Models.Requests.Bacen.ImmediateCollectionWithoutLocation body, string txId = null)
        {
            Models.Requests.Bacen.ImmediateCollection originalBody = new Models.Requests.Bacen.ImmediateCollection()
            {
                calendario = body.calendario,
                chave = body.chave,
                devedor = body.devedor,
                infoAdicionais = body.infoAdicionais,
                solicitacaoPagador = body.solicitacaoPagador,
                valor = body.valor
            };

            var loc = await this.EnvelopeRequest<Models.Responses.Bacen.Location>(MerchantPaths.Location, HttpMethod.Post, 
                                                                                  _serializer.Deserialize<object>("{ \"tipoCob\": \"" + CollectionType.cob + "\" }"), 
                                                                                  retryFlow: true);

            originalBody.loc = new Models.Responses.Bacen.Location()
            {
                id = loc.Body.id
            };

            if (string.IsNullOrWhiteSpace(txId))
            {
                txId = $"T{this.RandomString(34)}";
            }

            return await this.EnvelopeRequest<Models.Responses.Bacen.ImmediateCollection>(MerchantPaths.ImmediateCollection + txId, HttpMethod.Put, originalBody);
        }

        /// <summary>
        /// Listing collections.
        /// </summary>

        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ListImmediateCollection>> ListAsync(Models.Requests.Bacen.ListImmediateCollection parameters)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("inicio", parameters.inicio);
            dict.Add("fim", parameters.fim);
            dict.Add("cpf", parameters.cpf);
            dict.Add("cnpj", parameters.cnpj);

            if (parameters.status.HasValue)
            {
                dict.Add("status", parameters.status.ToString());
            }

            if (parameters.paginaAtual.HasValue)
            {
                dict.Add("paginacao.paginaAtual", parameters.paginaAtual.ToString());
            }

            if (parameters.itensPorPagina.HasValue)
            {
                dict.Add("paginacao.itensPorPagina", parameters.itensPorPagina.ToString());
            }

            if (parameters.locationPresente.HasValue)
            {
                dict.Add("locationPresente", parameters.locationPresente.ToString());
            }

            var response = await this.EnvelopeRequest<Models.Responses.Bacen.ListImmediateCollection>(MerchantPaths.ImmediateCollection, HttpMethod.Get, parameters: dict);

            return response;
        }

        /// <summary>
        /// Get qrcode image from location passing pactualId. If imageFormat = base64 imageType can be png, jpeg and gif.
        /// </summary>
        /// <param name="pactualId"></param>
        /// <param name="pixelsPerModule"></param>
        /// <param name="imageFormat"></param>
        /// <param name="imageType"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<string>> GetImageAsync(string pactualId, int pixelsPerModule = 20, ImageFormat imageFormat = ImageFormat.base64, ImageType imageType = ImageType.Png)
        {
            if (string.IsNullOrWhiteSpace(pactualId))
            {
                return new Envelope<string>(new Errors()
                {
                    status = (int)HttpStatusCode.BadRequest,
                    detail = "send an pactualId"
                });
            }

            if (imageFormat == ImageFormat.base64)
            {
                return await this.EnvelopeRequest<string>(MerchantPaths.IndirectLocation + pactualId + $"/base64?pixelsPerModule={pixelsPerModule}&imageType={imageType}", HttpMethod.Get);
            }
            else
            {
                return await this.EnvelopeRequest<string>(MerchantPaths.IndirectLocation + pactualId + $"/svg?pixelsPerModule={pixelsPerModule}", HttpMethod.Get);
            }
        }
    }
}
