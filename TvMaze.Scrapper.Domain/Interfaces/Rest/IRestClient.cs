using System.Net;
using System.Threading.Tasks;

namespace TvMaze.ApiClient.Http
{
    public interface IRestClient
    {
        Task<T> GetContentAsync<T>(string url);
    }
}