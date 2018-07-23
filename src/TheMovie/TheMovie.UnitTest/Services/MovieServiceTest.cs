using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TheMovie.Services;

namespace TheMovie.UnitTest.Services
{
    [TestClass()]
    public class MovieServiceTest
    {
        [TestMethod()]
        public async Task GetAllDefaultTest()
        {
            var client = new HttpClientService();
            var movieService = new MovieService(client);
            var page = 1;
            var language = "en-US";
            var region = "us";

            var result = await movieService.GetAll(page, language, region);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetAllPageTest()
        {
            var client = new HttpClientService();
            var movieService = new MovieService(client);
            var page = 1;

            var result = await movieService.GetAll(page, null, null);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetAllLanguageTest()
        {
            var client = new HttpClientService();
            var movieService = new MovieService(client);
            var page = 1;
            var language = "pt-BR";

            var result = await movieService.GetAll(page, language, null);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetAllRegionTest()
        {
            var client = new HttpClientService();
            var movieService = new MovieService(client);
            var page = 1;
            var region = "br";

            var result = await movieService.GetAll(page, null, region);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetAllPageLanguageRegionTest()
        {
            var client = new HttpClientService();
            var movieService = new MovieService(client);
            var page = 1;
            var language = "pt-BR";
            var region = "br";

            var result = await movieService.GetAll(page, language, region);

            Assert.IsNotNull(result);
        }
    }
}
