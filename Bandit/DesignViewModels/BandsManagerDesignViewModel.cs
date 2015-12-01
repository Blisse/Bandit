namespace Bandit.DesignViewModels
{
    using System.Collections.ObjectModel;

    using Bandit.Services.Band;

    using Microsoft.Band;

    using Prism.Commands;
    using Prism.Windows.Mvvm;

    public class BandsManagerDesignViewModel : ViewModelBase
    {
        private IBandService bandService;

        private ObservableCollection<IBandInfo> bands = new ObservableCollection<IBandInfo>();

        private IBandInfo selectedBand;

        public BandsManagerDesignViewModel(IBandService bandService)
        {
            this.bandService = bandService;
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

    }
}
