using PixApi.Models.Responses.Bacen;
using System.Threading.Tasks;

namespace PixApi.Services.Interfaces
{
    public interface ILocation
    {
        Task<Envelope<Models.Responses.Bacen.Location>> GetAsync(long id);
        Task<Envelope<Models.Responses.Bacen.Location>> CreateAsync(CollectionType collectionType);
        Task<Envelope<Models.Responses.Bacen.ListLocation>> ListAsync(Models.Requests.Bacen.ListLocation parameters);
        Task<Envelope<Models.Responses.Bacen.Location>> UnlinkCollectionAsync(long id);
    }
}
