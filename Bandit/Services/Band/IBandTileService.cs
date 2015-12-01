namespace Bandit.Services.Band
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Band.Tiles;

    public interface IBandTileService
    {
        Task<IEnumerable<BandTile>> GetTilesAsync(CancellationToken token);

        Task<int> GetRemainingTilesCapacityAsync(CancellationToken token);
    }
}
