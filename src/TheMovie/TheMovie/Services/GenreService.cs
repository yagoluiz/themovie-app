using Newtonsoft.Json;
using System.Threading.Tasks;
using TheMovie.Common.API;
using TheMovie.Models;

namespace TheMovie.Services
{
    public class GenreService : IGenreService
    {
        private readonly IHttpClientService _httpClientService;

        public GenreService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<GenreMoviesModel> GetAll(string language = null)
        {
            var response = await _httpClientService.HeaderTheMovieApi().GetAsync($"{TheMovieApi.API_URL}genre/movie/list?api_key={TheMovieApi.API_KEY}&language={language}");

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GenreMoviesModel>(stringResponse);
            }

            return null;
        }
    }
}
