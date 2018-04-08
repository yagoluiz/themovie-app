using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheMovie.Models;
using TheMovie.Services;
using Xamarin.Forms;

namespace TheMovie.ViewModels
{
    public class UpcomingMovieViewModel : ViewModelBase
    {
        #region Properties

        private int _pageUpcomingMovies = 1;
        private bool _isSearchUpcomingMovies = false;

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
        public DelegateCommand<UpcomingMovieModel> ItemAppearingCommand { get; set; }
        public DelegateCommand<UpcomingMovieModel> ItemTappedCommand { get; set; }

        #endregion

        #region Services

        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        #endregion

        public UpcomingMovieViewModel(INavigationService navigationService, IMovieService movieService, IGenreService genreService)
            : base(navigationService)
        {
            _movieService = movieService;
            _genreService = genreService;
            Title = "Upcoming Movies";
            SearchPlaceHolder = "Search your movie :)";
            UpcomingMovies = new ObservableCollection<UpcomingMovieModel>();
            SearchCommand = new Command(ExecuteSearchCommand);
            UpcomingMovieRefreshCommand = new Command(async () => await ExecuteUpcomingMovieRefreshCommand());
            ItemAppearingCommand = new DelegateCommand<UpcomingMovieModel>(ExecuteItemAppearingCommand);
            ItemTappedCommand = new DelegateCommand<UpcomingMovieModel>(ExecuteItemTappedCommand);
            IsLoading = true;
            InitializeUpcomingMovies();
        }

        #region Methods 

        private async Task GetUpcomingMovies(int pageMovie)
        {
            var movies = await _movieService.GetAll(pageMovie);

            if (movies.UpcomingMovies.Count() > 0)
            {
                var genres = await _genreService.GetAll();

                foreach (var movie in movies.UpcomingMovies)
                {
                    foreach (var genreId in movie.GenreIds)
                    {
                        movie.Genres += $"{genres.GenreMovies.FirstOrDefault(x => x.Id == genreId).Name}\n";
                    }

                    UpcomingMovies.Add(movie);
                }
            }
        }

        private async void InitializeUpcomingMovies()
        {
            IsSearch = true;

            UpcomingMovies.Clear();

            await GetUpcomingMovies(_pageUpcomingMovies);

            IsLoading = false;
        }

        #endregion

        #region Methods Commands

        private void ExecuteSearchCommand()
        {
            _isSearchUpcomingMovies = true;
            _pageUpcomingMovies = 0;

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

        private async Task ExecuteUpcomingMovieRefreshCommand()
        {
            IsUpcomingMovieRefresh = true;
            _isSearchUpcomingMovies = false;

            UpcomingMovies.Clear();

            _pageUpcomingMovies = 1;
            await GetUpcomingMovies(_pageUpcomingMovies);

            IsUpcomingMovieRefresh = false;
        }

        private async void ExecuteItemAppearingCommand(UpcomingMovieModel upcomingMovie)
        {
            if (_isSearchUpcomingMovies || UpcomingMovies.Count == 0 || !(UpcomingMovies.Last().Equals(upcomingMovie)))
            {
                return;
            }
            else
            {
                _pageUpcomingMovies++;
                await GetUpcomingMovies(_pageUpcomingMovies);
            }
        }

        private async void ExecuteItemTappedCommand(UpcomingMovieModel upcomingMovie)
        {
            var navigationParams = new NavigationParameters
            {
                { "upcomingMovie", upcomingMovie }
            };

            await NavigationService.NavigateAsync("UpcomingMovieDetailsPage", navigationParams);
        }

        #endregion
    }
}
