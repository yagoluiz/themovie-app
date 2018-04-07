using Newtonsoft.Json;
using TheMovie.Common.API;

namespace TheMovie.Models
{
    public class UpcomingMoviesModel
    {
        [JsonProperty("results")]
        public UpcomingMovieModel[] UpcomingMovies { get; set; }
    }

    public class UpcomingMovieModel
    {
        [JsonProperty("vote_count")]
        public int VoteCount { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("video")]
        public bool Video { get; set; }
        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("popularity")]
        public float Popularity { get; set; }
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }
        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonProperty("adult")]
        public bool Adult { get; set; }
        [JsonProperty("overview")]
        public string Overview { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
        [JsonIgnore]
        public string PosterPathUrl
        {
            get { return $"{TheMovieApi.API_URL_IMAGE}{PosterPath}"; }
        }
        [JsonIgnore]
        public string BackdropPathUrl
        {
            get { return $"{TheMovieApi.API_URL_IMAGE}{BackdropPath}"; }
        }
        [JsonIgnore]
        public string Genres { get; set; }
    }
}
