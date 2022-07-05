using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Models.Requests.Bacen
{
    public class ListBase
    {
        public string inicio { get; set; }
        public string fim { get; set; }
        public int? paginaAtual { get; set; }
        public int? itensPorPagina { get; set; }
    }
}
