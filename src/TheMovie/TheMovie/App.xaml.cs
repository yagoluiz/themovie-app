﻿using Prism;
using Prism.Autofac;
using Prism.Ioc;
using TheMovie.Services;
using TheMovie.ViewModels;
using TheMovie.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            containerRegistry.Register<IHttpClientService, HttpClientService>();
            containerRegistry.Register<IGenreService, GenreService>();
            containerRegistry.Register<IMovieService, MovieService>();
        }
    }
}
