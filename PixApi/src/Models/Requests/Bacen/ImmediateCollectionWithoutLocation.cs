using PixApi.Models.Responses.Bacen;
using System.Collections.Generic;

namespace PixApi.Models.Requests.Bacen
{
    public class ImmediateCollectionWithoutLocation
    {
        public Calendario calendario { get; set; }
        public string chave { get; set; }
        public Devedor devedor { get; set; }
        public List<InfoAdicionais> infoAdicionais { get; set; }
        public string solicitacaoPagador { get; set; }
        public Valor valor { get; set; }
    }
}
