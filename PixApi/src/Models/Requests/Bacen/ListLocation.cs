using PixApi.Models.Responses.Bacen;

namespace PixApi.Models.Requests.Bacen
{
    public class ListLocation : ListBase
    {
        public bool txIdPresente { get; set; }
        public CollectionType? tipoCob { get; set; }
    }
}
