namespace Bandit.Services.Band
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GalaSoft.MvvmLight.Messaging;

    using Microsoft.Band;
    using Microsoft.Band.Tiles;

    public class BandService : IBandService, IBandTileService
    {
        private IBandClient bandClient;

        public async Task<IBandClient> ConnectBandAsync(IBandInfo band)
        {
            this.bandClient?.Dispose();
            this.bandClient = null;

            try
            {
                this.bandClient = await BandClientManager.Instance.ConnectAsync(band);

                Messenger.Default.Send(new BandConnectedStateChanged());
            }
            catch (BandException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return this.bandClient;
        }

        public async Task<IEnumerable<IBandInfo>> GetBandsAsync()
        {
            try
            {
                return await BandClientManager.Instance.GetBandsAsync();
            }
            catch (BandException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return Enumerable.Empty<IBandInfo>();
        }

        public IBandClient GetConnectedBand()
        {
            return this.bandClient;
        }

        public async Task<IEnumerable<BandTile>> GetTilesAsync(CancellationToken token)
        {
            if (this.bandClient == null)
            {
                return Enumerable.Empty<BandTile>();
            }

            try
            {
                return await this.bandClient.TileManager.GetTilesAsync(token);
            }
            catch (BandException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return Enumerable.Empty<BandTile>();
        }

        public async Task<int> GetRemainingTilesCapacityAsync(CancellationToken token)
        {
            if (this.bandClient == null)
            {
                return 0;
            }

            try
            {
                return await this.bandClient.TileManager.GetRemainingTileCapacityAsync(token);
            }
            catch (BandException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return 0;
        }

        public sealed class BandConnectedStateChanged
        {
        }
    }
}