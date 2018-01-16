using System.Threading.Tasks;

namespace WhereIsMyBusAPI.Contracts
{   
    public interface IHttpRequest<TProvider>
    {
        Task<T> CreateGetRequestAsync<T>(string url);
    }
}