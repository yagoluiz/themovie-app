using System.Threading.Tasks;
using TheMovie.Models;

namespace TheMovie.Services
{
    public interface IMovieService
    {
        Task<UpcomingMoviesModel> GetAll(int page = 1, string language = "en-US", string region = "us");
    }
}
