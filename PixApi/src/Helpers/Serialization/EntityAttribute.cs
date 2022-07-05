using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Helpers.Serialization
{
    internal class EntityAttribute : Attribute
    {
        public string SingularName { get; set; }
        public string PluralName { get; set; }
    }
}
