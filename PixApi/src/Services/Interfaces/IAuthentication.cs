using System.Threading.Tasks;

namespace PixApi.Services.Interfaces
{
    public interface IAuthentication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> GetTokenAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsProduction();
        int RequestTimeoutInSeconds();
    }
}
