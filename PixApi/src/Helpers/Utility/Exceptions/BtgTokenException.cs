using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Helpers.Utility.Exceptions
{
    public class BtgTokenException : System.Exception
    {
        public string Description { get; set; }
        public BtgTokenException(string msg) : base(msg)
        {

        }

        public BtgTokenException(string msg, string description) : base(msg)
        {
            Description = description;
        }
    }
}
