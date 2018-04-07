using Newtonsoft.Json;

namespace TheMovie.Models
{

    public class GenreMoviesModel
    {
        [JsonProperty("genres")]
        public GenreMovieModel[] GenreMovies { get; set; }
    }

    public class GenreMovieModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
