using System.Linq;

namespace PixApi.Models.Responses.Bacen
{
    public class Location
    {
        public string criacao { get; set; }
        public long id { get; set; }
        public string location { get; set; }
        public CollectionType tipoCob { get; set; }
        public string txId { get; set; }
        public string pactualId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(location))
                {
                    return location.Split('/').LastOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public enum CollectionType
    {
        cob,
        cobv
    }
}
