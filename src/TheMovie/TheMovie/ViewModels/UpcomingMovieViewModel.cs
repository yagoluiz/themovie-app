using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
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

        private int _pageUpcomingMovies = 0;
        private int _totalPagesUpcomingMovies = 0;
        private bool _isSearchUpcomingMovies = false;

        private bool _isUpcomingMovieRefresh;
        public bool IsUpcomingMovieRefresh
        {
            get => _isUpcomingMovieRefresh;
            set => SetProperty(ref _isUpcomingMovieRefresh, value);
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isLoadingUpcomingMovies;
        public bool IsLoadingUpcomingMovies
        {
            get => _isLoadingUpcomingMovies;
            set => SetProperty(ref _isLoadingUpcomingMovies, value);
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

        public UpcomingMovieViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IMovieService movieService, IGenreService genreService)
            : base(navigationService, pageDialogService)
        {
            _movieService = movieService;
            _genreService = genreService;
            Title = "Upcoming Movies";
            SearchPlaceHolder = "Search your movie :)";
            UpcomingMovies = new ObservableCollection<UpcomingMovieModel>();
            SearchCommand = new Command(async () => await ExecuteSearchCommand());
            UpcomingMovieRefreshCommand = new Command(async () => await ExecuteUpcomingMovieRefreshCommand());
            ItemAppearingCommand = new DelegateCommand<UpcomingMovieModel>(ExecuteItemAppearingCommand);
            ItemTappedCommand = new DelegateCommand<UpcomingMovieModel>(ExecuteItemTappedCommand);
            InitializeUpcomingMovies();
        }

        #region Methods 

        private async Task GetUpcomingMovies(int pageMovie = 1)
        {
            var movies = await _movieService.GetAll(pageMovie);

            if (movies.UpcomingMovies.Count() > 0)
            {
                _pageUpcomingMovies = movies.Page;
                _totalPagesUpcomingMovies = movies.TotalPages;

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
            IsLoading = true;

            UpcomingMovies.Clear();

            await GetUpcomingMovies();

            IsLoading = false;
        }

        #endregion

        #region Methods Commands

        private async Task ExecuteSearchCommand()
        {
            _isSearchUpcomingMovies = true;

            var searchResult = UpcomingMovies.Where(x => x.Title.ToUpper().Contains(SearchText.ToUpper())).ToList();

            if (searchResult.Count > 0)
            {
                UpcomingMovies.Clear();

                foreach (var search in searchResult)
                {
                    UpcomingMovies.Add(search);
                }
            }
            else
            {
                await PageDialogService.DisplayAlertAsync("Ooops!", "Movie not found :(", "Ok");
                return;
            }
        }

        private async Task ExecuteUpcomingMovieRefreshCommand()
        {
            IsUpcomingMovieRefresh = true;
            _isSearchUpcomingMovies = false;

            UpcomingMovies.Clear();

            await GetUpcomingMovies();

            IsUpcomingMovieRefresh = false;
        }

        private async void ExecuteItemAppearingCommand(UpcomingMovieModel upcomingMovie)
        {
            var pagination = _pageUpcomingMovies + 1;

            if (_isSearchUpcomingMovies ||
                (pagination > _totalPagesUpcomingMovies) ||
                UpcomingMovies.Count == 0 ||
                !(UpcomingMovies.Last().Equals(upcomingMovie)))
            {
                return;
            }
            else
            {
                IsLoadingUpcomingMovies = true;

                await GetUpcomingMovies(pagination);

                IsLoadingUpcomingMovies = false;
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
