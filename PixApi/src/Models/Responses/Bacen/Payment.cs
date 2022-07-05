using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Models.Responses.Bacen
{
    public class Payment
    {
        public string chave { get; set; }
        public string endToEndId { get; set; }
        public string txid { get; set; }
        public string valor { get; set; }
        public string horario { get; set; }
        public string infoPagador { get; set; }
        public List<Reversal> devolucoes { get; set; }
        public ComponentesValor componentesValor{get;set;}
    }

    public class ComponentesValor
    {
        public Original original { get; set; }
        public SaqueTroco saque { get; set; }
        public SaqueTroco troco { get; set; }
    }

    public class Original
    {
        public string valor { get; set; }
    }

    public class SaqueTroco
    {
        public string valor { get; set; }
        public ModalidadeAgente? modalidadeAgente { get; set; }
        public string prestadorDeServicoDeSaque { get; set; }
    }

    public enum ModalidadeAgente
    {
        AGTEC,
        AGTOT,
        AGPSS
    }
}
