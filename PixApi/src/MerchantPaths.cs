using System;

namespace PixApi
{
    internal static class MerchantPaths
    {
        /// <summary>
        /// BTG Pactual Merchant API version.
        /// </summary>
        public const string BacenVersion = "v2.x";

        public const string ImmediateCollection = "/pm/pix-api/" + BacenVersion + "/cob/";
        public const string DuedateCollection = "/pm/pix-api/" + BacenVersion + "/cobv/";
        public const string Location = "/pm/pix-api/" + BacenVersion + "/loc/";
        public const string Incoming = "/pm/pix-api/" + BacenVersion + "/pix/";
        public const string IndirectLocation = "/pc/pix/location/";
    }
}
