using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Models.Responses.Bacen
{
    public class Reversal
    {
        public string id { get; set; }
        public string rtrId { get; set; }
        public string valor { get; set; }
        public Horario horario { get; set; }
        public string status { get; set; }
    }
}
