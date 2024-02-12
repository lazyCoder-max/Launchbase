using Launchbase.Services.Web3.Dtos;

namespace Launchbase.Services.Interfaces
{
    public interface IToken
    {
        Task<Response<ContractResponse>> Approve(string spenderAddress, string tokenAddress, decimal amount, decimal decimalResult);
        Task<Response<int>> GetTokenDecimal(string contractAddress);
        Task<Response<string>> GetTokenName(string contractAddress);
        Task<Response<string>> GetTokenSymbol(string contractAddress);
        Task<Response<long>> GetTokenTotalSupply(string contractAddress);
        Task<Response<TokenInfo>> GetTokenInformation(string contractAddress);
    }
}
