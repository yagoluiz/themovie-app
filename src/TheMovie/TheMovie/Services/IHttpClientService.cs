using System.Net.Http;

namespace TheMovie.Services
{
    public interface IHttpClientService
    {
        HttpClient HeaderTheMovieApi();
    }
}
