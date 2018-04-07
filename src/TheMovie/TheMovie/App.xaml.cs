using Prism;
using Prism.Ioc;
using TheMovie.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Autofac;
using TheMovie.Services;
using TheMovie.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TheMovie
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/UpcomingMoviePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterNavigation(containerRegistry);
            RegisterServices(containerRegistry);
        }

        private static void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<UpcomingMoviePage, UpcomingMovieViewModel>();
            containerRegistry.RegisterForNavigation<UpcomingMovieDetailsPage, UpcomingMovieDetailsViewModel>();
        }

        private static void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGenreService, GenreService>();
            containerRegistry.Register<IHttpClientService, HttpClientService>();
            containerRegistry.Register<IMovieService, MovieService>();
        }
    }
}
