using System.Collections.Generic;

namespace PixApi.Models.Responses.Bacen
{
    public class Calendario
    {
        public string criacao { get; set; }
        public int expiracao { get; set; }
    }

    public class Devedor
    {
        public string cpf { get; set; }
        public string nome { get; set; }
    }

    public class InfoAdicionais
    {
        public string nome { get; set; }
        public string valor { get; set; }
    }

    public class ImmediateCollection
    {
        public Calendario calendario { get; set; }
        public string status { get; set; }
        public string txid { get; set; }
        public int revisao { get; set; }
        public string location { get; set; }
        public Location loc { get; set; }
        public Devedor devedor { get; set; }
        public Valor valor { get; set; }
        public string chave { get; set; }
        public string solicitacaoPagador { get; set; }
        public List<InfoAdicionais> infoAdicionais { get; set; }
    }

    public class Valor
    {
        public string original { get; set; }
        public int modalidadeAlteracao { get; set; }
    }


}
