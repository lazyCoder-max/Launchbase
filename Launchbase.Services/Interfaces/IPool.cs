using Launchbase.Services.Web3.Dtos;

namespace Launchbase.Services.Interfaces
{
    public interface IPool
    {
        Task<Response<List<PoolInfo>>> GetAllPoolAsync();
        Task<Response<ContractResponse>> CreatePoolAsync(string tokenAddress, string adminWallet, long startTime, long endTime, decimal softCap, decimal hardCap, decimal minContribution, decimal maxContribution, string[] otherInfo, string[] tokens, decimal[] rates);
    }
}
