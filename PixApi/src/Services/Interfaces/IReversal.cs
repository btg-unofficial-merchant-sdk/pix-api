using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Services.Interfaces
{
    public interface IReversal
    {
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Reversal>> GetAsync(string endToEndId, string clientRequestId);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Reversal>> CreateAsync(Models.Requests.Bacen.CreateReversal request);
    }
}
