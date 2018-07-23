using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TheMovie.Services;

namespace TheMovie.UnitTest.Services
{
    [TestClass()]
    public class GenreServiceTest
    {
        [TestMethod()]
        public async Task GetAllTest()
        {
            var client = new HttpClientService();
            var genreService = new GenreService(client);

            var result = await genreService.GetAll();

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task GetAllLanguageTest()
        {
            var client = new HttpClientService();
            var genreService = new GenreService(client);
            var language = "pt-BR";

            var result = await genreService.GetAll(language);

            Assert.IsNotNull(result);
        }
    }
}
