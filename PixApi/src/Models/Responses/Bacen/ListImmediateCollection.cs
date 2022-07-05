using System;
using System.Collections.Generic;

namespace PixApi.Models.Responses.Bacen
{
    public class CustomImmediateCollection : Responses.Bacen.ImmediateCollection
    {
        public List<Pix> pix { get; set; }
    }

    public class Devolucao
    {
        public string id { get; set; }
        public string rtrId { get; set; }
        public string valor { get; set; }
        public Horario horario { get; set; }
        public string status { get; set; }
    }

    public class Horario
    {
        public string solicitacao { get; set; }
    }

    public class Paginacao
    {
        public int paginaAtual { get; set; }
        public int itensPorPagina { get; set; }
        public int quantidadeDePaginas { get; set; }
        public int quantidadeTotalDeItens { get; set; }
    }

    public class Parametros
    {
        public string inicio { get; set; }
        public string fim { get; set; }
        public Paginacao paginacao { get; set; }
    }

    public class Pix
    {
        public string endToEndId { get; set; }
        public string txid { get; set; }
        public string valor { get; set; }
        public string horario { get; set; }
        public string infoPagador { get; set; }
        public List<Devolucao> devolucoes { get; set; }
    }

    public class ListImmediateCollection
    {
        public Parametros parametros { get; set; }
        public List<CustomImmediateCollection> cobs { get; set; }
    }
}
