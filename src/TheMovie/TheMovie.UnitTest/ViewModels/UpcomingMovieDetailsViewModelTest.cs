using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheMovie.ViewModels;

namespace TheMovie.UnitTest.ViewModels
{
    [TestClass()]
    public class UpcomingMovieDetailsViewModelTest
    {
        [TestMethod()]
        public void UpcomingMoviePropertyIsNullViewModelInitializationTest()
        {
            var viewModel = new UpcomingMovieDetailsViewModel(null);

            Assert.IsNull(viewModel.UpcomingMovie);
        }
    }
}
