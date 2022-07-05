using PixApi.Models.Requests.Bacen;
using System.Threading.Tasks;

namespace PixApi.Services.Interfaces
{
    public interface IImmediateCollection
    {
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> GetAsync(string txId);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> UpdateAsync(string txId, Models.Requests.Bacen.UpdateImmediateCollection body);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> CreateAsync(string txId, Models.Requests.Bacen.ImmediateCollection body);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> CreateAsync(Models.Requests.Bacen.ImmediateCollection body);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ImmediateCollection>> CreateWithLocationAsync(Models.Requests.Bacen.ImmediateCollectionWithoutLocation body, string txId = null);
        Task<Models.Responses.Bacen.Envelope<Models.Responses.Bacen.ListImmediateCollection>> ListAsync(Models.Requests.Bacen.ListImmediateCollection parameters);
        Task<Models.Responses.Bacen.Envelope<string>> GetImageAsync(string pactualId, int pixelsPerModule = 20, ImageFormat imageFormat = ImageFormat.base64, ImageType imageType = ImageType.Png);
    }
}
