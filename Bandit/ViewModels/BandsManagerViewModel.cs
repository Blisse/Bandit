namespace Bandit.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Microsoft.Band;

    using Prism.Commands;
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;

    using Services.Band;

    public class BandsManagerViewModel : ViewModelBase
    {
        private readonly IBandService bandService;

        private ObservableCollection<IBandInfo> bands = new ObservableCollection<IBandInfo>();

        private IBandInfo selectedBand;

        public BandsManagerViewModel(IBandService bandService)
        {
            this.bandService = bandService;

            GetBandsCommand = new DelegateCommand(GetBands);
            ConnectToBandCommand = new DelegateCommand(ConnectToBand);
        }

        public ObservableCollection<IBandInfo> Bands
        {
            get
            {
                return this.bands;
            }

            set
            {
                SetProperty(ref this.bands, value);
            }
        }

        public DelegateCommand ConnectToBandCommand { get; set; }

        public DelegateCommand GetBandsCommand { get; set; }

        public IBandInfo SelectedBand
        {
            get
            {
                return selectedBand;
            }

            set
            {
                SetProperty(ref selectedBand, value);
            }
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            await GetBandsCommand.Execute();
        }

        private async void ConnectToBand()
        {
            if (SelectedBand != null)
            {
                await bandService.ConnectBandAsync(SelectedBand);
            }
        }

        private async void GetBands()
        {
            Bands = new ObservableCollection<IBandInfo>(await bandService.GetBandsAsync());
        }
    }
}