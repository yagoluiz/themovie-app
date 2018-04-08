using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheMovie.Services;
using TheMovie.ViewModels;

namespace TheMovie.UnitTest.ViewModels
{
    [TestClass()]
    public class UpcomingMovieViewModelTest
    {
        [TestMethod()]
        public void UpcomingMoviesPropertyIsNotNullViewModelInitializationTest()
        {
            var client = new HttpClientService();
            var movieService = new MovieService(client);
            var genreService = new GenreService(client);

            var viewModel = new UpcomingMovieViewModel(null, movieService, genreService);

            Assert.IsNotNull(viewModel.UpcomingMovies);
        }
    }
}
