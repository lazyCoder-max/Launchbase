using Launchbase.Dtos;
using Launchbase.Store.TokenUseCase;

namespace Launchbase.Dtos
{
    public class Chain
    {
        public int ActiveChainId { get; set; }
        public string Address { get; set; }
        public string DeployedChain { get; set; }
        public string ChainLogoUrl { get; set; }
        public string GasAPI { get; set; }
        public string CurrecySymbol { get; set; }
        public List<Currency> Currencies { get; set; }
    }

}
