using PixApi.Helpers.Utility;
using PixApi.Models.Responses.Bacen;
using PixApi.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Services.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class Payment : BaseRepository, IPayment
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationData"></param>
        public Payment(Models.Requests.Authorization authenticationData) : base(authenticationData) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authenticationData"></param>
        public Payment(HttpClient httpClient, Models.Requests.Authorization authenticationData) : base(httpClient, authenticationData) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="authentication"></param>
        public Payment(HttpClient httpClient, IAuthentication authentication) : base(httpClient, authentication) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endToEndId"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Payment>> GetAsync(string endToEndId)
        {
            return await this.EnvelopeRequest<Models.Responses.Bacen.Payment>(MerchantPaths.Incoming + endToEndId, HttpMethod.Get);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ListPayment>> ListAsync(Models.Requests.Bacen.ListPayment parameters)
        {
            var query = new Dictionary<string, string>();
            query.Add("inicio", parameters.inicio);
            query.Add("fim", parameters.fim);

            query.Add("txId", parameters.txId);
            query.Add("cnpj", parameters.cnpj);
            query.Add("cpf", parameters.cpf);

            if (parameters.devolucaoPresente.HasValue)
                query.Add("devolucaoPresente", parameters.devolucaoPresente.ToString());

            if (parameters.txIdPresente.HasValue)
                query.Add("txIdPresente", parameters.txIdPresente.ToString());

            if (parameters.paginaAtual > 0)
                query.Add("paginacao.paginaAtual", parameters.paginaAtual.ToString());

            if (parameters.itensPorPagina > 0)
                query.Add("paginacao.itensPorPagina", parameters.itensPorPagina.ToString());

            return await this.EnvelopeRequest<Models.Responses.Bacen.ListPayment>(MerchantPaths.Incoming, HttpMethod.Get, parameters: query);
        }
    }
}
