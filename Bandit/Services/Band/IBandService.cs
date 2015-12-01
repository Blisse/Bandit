namespace Bandit.Services.Band
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Band;

    public interface IBandService
    {
        Task<IEnumerable<IBandInfo>> GetBandsAsync();

        Task<IBandClient> ConnectBandAsync(IBandInfo band);

        IBandClient GetConnectedBand();
    }
}
