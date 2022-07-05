using System;
using System.Collections.Generic;
using System.Text;

namespace PixApi.Helpers.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T entity);
        T Deserialize<T>(string content);
    }
}
