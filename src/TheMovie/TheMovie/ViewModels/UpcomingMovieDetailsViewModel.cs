using Acr.UserDialogs;
using Prism.Navigation;
using Prism.Services;
using TheMovie.Models;

namespace TheMovie.ViewModels
{
    public class UpcomingMovieDetailsViewModel : ViewModelBase
    {
        #region  Properties

        private UpcomingMovieModel _upcomingMovie;
        public UpcomingMovieModel UpcomingMovie
        {
            get => _upcomingMovie;
            set => SetProperty(ref _upcomingMovie, value);
        }

        #endregion

        public UpcomingMovieDetailsViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Details";
        }

        #region Navigation

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            UpcomingMovie = parameters["upcomingMovie"] as UpcomingMovieModel;
        }

        #endregion
    }
}
