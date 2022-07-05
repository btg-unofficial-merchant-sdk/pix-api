using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi
{
    public class Constants
    {
        /// <summary>
        /// Base URI of BTG Pactual API production server.
        /// </summary>
        public const string ProdApiBase = "https://api.developer.btgpactual.com/";

        /// <summary>
        /// Base URI of BTG Pactual API homologation server.
        /// </summary>
        public const string HmlApiBase = "https://api-h.developer.btgpactual.com/";

        /// <summary>
        /// Use this to format date to a string.
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Use this to format date with time to a string.
        /// </summary>
        public const string DateAndTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// SDk user-agentg.
        /// </summary>
        public const string UserAgent = "Unoficial Merchant SDK";

        /// <summary>
        /// Maximal page size (limit) offered by the API.
        /// Use with Limit field in search settings.
        /// </summary>
        public const int MaxLimit = 200;
    }
}
