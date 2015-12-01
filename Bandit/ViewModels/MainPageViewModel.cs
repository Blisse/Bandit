namespace Bandit.ViewModels
{
    using System.Collections.Generic;

    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;

    /// <summary>
    /// The main page view model.
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        private BandsManagerViewModel bandsManagerViewModel;

        private TilesManagerViewModel tilesManagerViewModel;

        public MainPageViewModel(BandsManagerViewModel bandManagerViewModel, TilesManagerViewModel tilesManagerViewModel)
        {
            BandsManagerViewModel = bandManagerViewModel;
            TilesManagerViewModel = tilesManagerViewModel;
        }

        public BandsManagerViewModel BandsManagerViewModel
        {
            get
            {
                return bandsManagerViewModel;
            }

            set
            {
                SetProperty(ref bandsManagerViewModel, value);
            }
        }

        public TilesManagerViewModel TilesManagerViewModel
        {
            get
            {
                return tilesManagerViewModel;
            }

            set
            {
                SetProperty(ref tilesManagerViewModel, value);
            }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            BandsManagerViewModel.OnNavigatedTo(e, viewModelState);
            TilesManagerViewModel.OnNavigatedTo(e, viewModelState);
        }

        public override void OnNavigatingFrom(
            NavigatingFromEventArgs e, 
            Dictionary<string, object> viewModelState, 
            bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);

            BandsManagerViewModel.OnNavigatingFrom(e, viewModelState, suspending);
            TilesManagerViewModel.OnNavigatingFrom(e, viewModelState, suspending);
        }
    }
}