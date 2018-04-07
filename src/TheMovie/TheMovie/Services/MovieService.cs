using Newtonsoft.Json;
using System.Threading.Tasks;
using TheMovie.Common.API;
using TheMovie.Models;

namespace TheMovie.Services
{
    public class MovieService : IMovieService
    {
        private readonly IHttpClientService _httpClientService;

        public MovieService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<UpcomingMoviesModel> GetAll(int page, string language, string region)
        {
            var response = await _httpClientService.HeaderTheMovieApi().GetAsync($"{TheMovieApi.API_URL}movie/upcoming?api_key={TheMovieApi.API_KEY}&page={page}&language={language}&region={region}");

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UpcomingMoviesModel>(stringResponse);
            }

            return null;
        }
    }
}
