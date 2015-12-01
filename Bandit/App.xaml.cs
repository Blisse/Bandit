namespace Bandit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;

    using Bandit.Services.Band;
    using Bandit.ViewModels;

    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Views;

    using Microsoft.Practices.ServiceLocation;

    using Prism.Events;
    using Prism.Mvvm;
    using Prism.Windows;
    using Prism.Windows.AppModel;

    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;

    /// <summary>
    ///     Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismApplication
    {
        public IEventAggregator EventAggregator { get; set; }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            EventAggregator = new EventAggregator();

            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<ISessionStateService, SessionStateService>();
            SimpleIoc.Default.Register<IEventAggregator, EventAggregator>();
            SimpleIoc.Default.Register<IResourceLoader, ResourceLoaderAdapter>();

            // Register repositories

            // Register services
            SimpleIoc.Default.Register<BandService>(() => new BandService());
            SimpleIoc.Default.Register<IBandService>(() => SimpleIoc.Default.GetInstance<BandService>());
            SimpleIoc.Default.Register<IBandTileService>(() => SimpleIoc.Default.GetInstance<BandService>());

            // Register child view models
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<BandsManagerViewModel>();
            SimpleIoc.Default.Register<TilesManagerViewModel>();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ViewModelLocationProvider.SetDefaultViewModelFactory(service => SimpleIoc.Default.GetInstance(service));
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(
                viewType =>
                    {
                        var potentialViewModelTypes = new List<string>
                                                          {
                                                              "Bandit.ViewModels.{0}ViewModel",
                                                              "Bandit.ViewModels.{0}Model"
                                                          };

                        foreach (var potentialType in potentialViewModelTypes)
                        {
                            var viewModelTypeName = string.Format(
                                CultureInfo.InvariantCulture,
                                potentialType,
                                viewType.Name);
                            var viewModelType = Type.GetType(viewModelTypeName);

                            if (viewModelType != null)
                            {
                                return viewModelType;
                            }
                        }

                        throw new InvalidOperationException("Could not find matching ViewModel for View: " + viewType);
                    });

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate("Main", null);
            Window.Current.Activate();
            return Task.FromResult<object>(null);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            // SessionStateService.RegisterKnownType(typeof(Dictionary<string, Collection<string>>));
        }
    }
}