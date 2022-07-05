using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Models.Responses.Bacen
{
    public class ListPayment
    {
        public Parametros parametros { get; set; }
        public List<Pix> pix { get; set; }
    }
}
