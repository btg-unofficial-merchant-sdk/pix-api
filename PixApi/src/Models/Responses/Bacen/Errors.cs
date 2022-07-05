using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Models.Responses.Bacen
{
    public class Errors
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string detail { get; set; }
        public string correlationId { get; set; }
        public string traceId { get; set; }
        public List<Violation> violations { get; set; }
        public string raw { get; set; }
    }

    public class Violation
    {
        public string reason { get; set; }
        public string property { get; set; }
        public string value { get; set; }
    }
}
