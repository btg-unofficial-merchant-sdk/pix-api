using PixApi.Models.Responses.Bacen;
using System.Collections.Generic;

namespace PixApi.Models.Requests.Bacen
{
    public class ImmediateCollection : ImmediateCollectionWithoutLocation
    {
        public Location loc { get; set; }
    }
}
