using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using TheMovie.Models;
using TheMovie.Services;
using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class UpcomingMovieViewModel : ViewModelBase
    {
        #region Properties

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isSearch;
        public bool IsSearch
        {
            get => _isSearch;
            set => SetProperty(ref _isSearch, value);
        }

        private bool _isUpcomingMovieRefresh;
        public bool IsUpcomingMovieRefresh
        {
            get => _isUpcomingMovieRefresh;
            set => SetProperty(ref _isUpcomingMovieRefresh, value);
        }

        private string _searchPlaceHolder;
        public string SearchPlaceHolder
        {
            get => _searchPlaceHolder;
            set => SetProperty(ref _searchPlaceHolder, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ObservableCollection<UpcomingMovieModel> UpcomingMovies { get; set; }

        #endregion

        #region Commands

        public Command SearchCommand { get; set; }
        public Command UpcomingMovieRefreshCommand { get; set; }
        public DelegateCommand<UpcomingMovieModel> ItemTappedCommand { get; set; }

        #endregion

        #region Services

        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        #endregion

        public UpcomingMovieViewModel(INavigationService navigationService, IMovieService movieService, IGenreService genreService)
            : base(navigationService)
        {
            Title = "Upcoming Movies";
            SearchPlaceHolder = "Search for your movie :)";
            _movieService = movieService;
            _genreService = genreService;
            UpcomingMovies = new ObservableCollection<UpcomingMovieModel>();
            SearchCommand = new Command(ExecuteSearchCommand);
            UpcomingMovieRefreshCommand = new Command(ExecuteUpcomingMovieRefreshCommand);
            ItemTappedCommand = new DelegateCommand<UpcomingMovieModel>(ExecuteItemTappedCommand);
            IsLoading = true;
            GetUpcomingMovies();
        }

        private async void GetUpcomingMovies()
        {
            var genres = await _genreService.GetAll();
            var movies = await _movieService.GetAll();

            UpcomingMovies.Clear();

            foreach (var movie in movies.UpcomingMovies)
            {
                foreach (var genreId in movie.GenreIds)
                {


                    movie.Genres += $"{genres.GenreMovies.FirstOrDefault(x => x.Id == genreId).Name}\n";
                }

                UpcomingMovies.Add(movie);
            }

            IsLoading = false;
            IsSearch = true;
        }

        private void ExecuteSearchCommand()
        {
            var searchResult = UpcomingMovies.Where(x => x.Title.ToUpper().Contains(SearchText.ToUpper())).ToList();

            if (searchResult.Count > 0)
            {
                UpcomingMovies.Clear();

                foreach (var search in searchResult)
                {
                    UpcomingMovies.Add(search);
                }
            }
        }

        private void ExecuteUpcomingMovieRefreshCommand()
        {
            IsUpcomingMovieRefresh = true;
            GetUpcomingMovies();
            IsUpcomingMovieRefresh = false;
        }

        private void ExecuteItemTappedCommand(UpcomingMovieModel upcomingMovie)
        {
            return;
        }
    }
}
