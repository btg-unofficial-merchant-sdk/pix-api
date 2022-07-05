using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixApi.Services.Interfaces
{
    public interface IPayment
    {
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.Payment>> GetAsync(string endToEndId);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ListPayment>> ListAsync(Models.Requests.Bacen.ListPayment parameters);
    }
}
