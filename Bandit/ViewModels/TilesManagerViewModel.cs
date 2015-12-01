namespace Bandit.ViewModels
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;

    using Bandit.Services.Band;

    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Band;
    using Microsoft.Band.Tiles;

    using Prism.Commands;
    using Prism.Windows.Mvvm;
    using Prism.Windows.Navigation;

    public class TilesManagerViewModel : ViewModelBase
    {
        private readonly IBandService bandService;
        private readonly IBandTileService tileService;

        private ObservableCollection<BandTile> tiles; 
        private int remainingTileCapacity;

        public TilesManagerViewModel(IBandService bandService, IBandTileService tileService)
        {
            this.bandService = bandService;
            this.tileService = tileService;

            GetTilesCommand = new DelegateCommand(GetTiles);
        }

        public DelegateCommand GetTilesCommand { get; set; }

        public int RemainingTileCapacity
        {
            get
            {
                return remainingTileCapacity;
            }

            set
            {
                SetProperty(ref remainingTileCapacity, value);
            }
        }

        public ObservableCollection<BandTile> Tiles
        {
            get
            {
                return tiles;
            }

            set
            {
                SetProperty(ref tiles, value);
            }
        } 

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            await GetTilesCommand.Execute();
        }

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        private async void GetTiles()
        {
            RemainingTileCapacity = await tileService.GetRemainingTilesCapacityAsync(CancellationToken.None);
            
            Tiles = new ObservableCollection<BandTile>(await tileService.GetTilesAsync(CancellationToken.None));
        }
    }
}