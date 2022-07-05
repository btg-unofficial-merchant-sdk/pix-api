using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Models.Responses
{
    public class Authorization
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
    }
}
