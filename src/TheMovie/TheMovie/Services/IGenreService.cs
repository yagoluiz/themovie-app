using System.Threading.Tasks;
using TheMovie.Models;

namespace TheMovie.Services
{
    public interface IGenreService
    {
        Task<GenreMoviesModel> GetAll(string language = "en-US");
    }
}
