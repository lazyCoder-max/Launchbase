using Microsoft.JSInterop;
using Newtonsoft.Json;
using Launchbase.Services.Interfaces;
using Launchbase.Services.Web3.Dtos;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Org.BouncyCastle.Utilities;
using Nethereum.Util;

namespace Launchbase.Services.Web3
{
    public class Pool : IPool
    {
        private readonly IJSRuntime javascript;
        private readonly string _contractAddress;
        private readonly string _accountAddress;
        private readonly string _apiAddress;
        public Pool(IJSRuntime javascript, string contractAddress, string accountAddress, string apiAddress = "")
        {
            this.javascript = javascript;
            _accountAddress = accountAddress;
            _contractAddress = contractAddress;
            _apiAddress = apiAddress;
        }
        public async Task<Response<ContractResponse>> ContributeAsync(string account, string tokenAddress, decimal paymentAmount, bool isToken, int poolId, double decimals=16)
        {
            try
            {
                var convertedValue = "";
                object[] args;
                if (isToken == false)
                {
                    var weiResult = double.Parse(paymentAmount.ToString()) * Math.Pow(10, 18);
                    convertedValue = weiResult.ToString("F0");
                    args = new object[6] { _contractAddress, _accountAddress, tokenAddress, convertedValue.ToString(), isToken, poolId };
                }
                else
                {
                    var weiResult = double.Parse(paymentAmount.ToString()) * Math.Pow(10, decimals);
                    convertedValue = weiResult.ToString("F0");
                    args = new object[6] { _contractAddress, _accountAddress, tokenAddress, convertedValue.ToString(), isToken, poolId };
                }
                var result = await javascript.InvokeAsync<string>("contribute", args);
                if (result.ToString().Contains("status"))
                {
                    var finalResult = JsonConvert.DeserializeObject<ContractResponse>(result.ToString());
                    return new() { Result = finalResult, Status = finalResult.status, Message = result.ToString() };
                }
                return new() { Status = false, Message = result.ToString() };
            }
            catch (Exception ex)
            {
                return new() { Status = false, Message = ex.Message };
            }

        }
        public async Task<Response<List<PoolInfo>>> GetAllPoolAsync()
        {
            try
            {
                object[] args;
                args = new object[1] { _contractAddress };
                var result = await javascript.InvokeAsync<object>("getAllPools", args);
                List<JsonElement> elements = new List<JsonElement>();
                if (result.GetType() == typeof(JsonElement))
                {
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(result);
                    var jsonElement = JsonDocument.Parse(jsonString).RootElement;
                    elements.AddRange(jsonElement.EnumerateArray());
                }
                List<PoolInfo> pools = new List<PoolInfo>();
                if (elements != null)
                {
                    foreach (var element in elements)
                    {
                        var otherInfo = new string[3];
                        var pool = new PoolInfo();
                        Token token = new(javascript, element[1].GetString(), _accountAddress);
                        using (JsonDocument document = JsonDocument.Parse(element[12].GetRawText()))
                        {
                            pool.Currencies = new();
                            foreach (JsonElement currencyElement in document.RootElement.EnumerateArray())
                            {
                                var addres = currencyElement[0].GetString();
                                if (addres != "0x0000000000000000000000000000000000001010")
                                {
                                    var currencyInfo = await token.GetTokenInformation(currencyElement[0].GetString());
                                    pool.Currencies.Add(new()
                                    {
                                        Address = currencyElement[0].GetString(),
                                        IsToken = currencyElement[1].GetBoolean(),
                                        Rate = decimal.Parse(currencyElement[2].GetString()),
                                        Symbol = currencyInfo.Result.Symbol,
                                        TotalSupply = currencyInfo.Result.TotalSupply,
                                        Decimals = currencyInfo.Result.Decimals
                                    });
                                }
                                else
                                {
                                    pool.Currencies.Add(new()
                                    {
                                        Address = currencyElement[0].GetString(),
                                        IsToken = currencyElement[1].GetBoolean(),
                                        Rate = decimal.Parse(currencyElement[2].GetString()),
                                        Decimals = 18
                                    });
                                }
                            }
                        }
                        var tokenInfo = await token.GetTokenInformation(element[1].GetString());
                        pool.Token = tokenInfo.Result;
                        pool.PoolId = int.Parse(element[0].GetString());
                        pool.Token.Address = element[1].GetString();
                        pool.AdminWallet = element[2].GetString();
                        pool.StartTime = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(element[3].GetString())).LocalDateTime);
                        pool.EndTime = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(element[4].GetString())).LocalDateTime);
                        pool.TotalRaised = Nethereum.Web3.Web3.Convert.FromWei(System.Numerics.BigInteger.Parse(element[5].GetString()));
                        pool.SoftCap = decimal.Parse(element[6].GetString());
                        pool.HardCap = decimal.Parse(element[7].GetString());
                        if(DateTime.Now > pool.EndTime && DateTime.Now> pool.StartTime)
                        {
                            pool.State = PoolState.Completed;
                        }
                        else if (DateTime.Now < pool.StartTime && DateTime.Now < pool.EndTime)
                        {
                            pool.State = PoolState.NotStarted;
                        }
                        else if(DateTime.Now> pool.StartTime && DateTime.Now < pool.EndTime)
                        {
                            pool.State = PoolState.InUse;
                        }
                        pool.MinimumBuy = decimal.Parse(element[9].GetString());
                        pool.MaximumBuy = decimal.Parse(element[10].GetString());
                        JArray jsonArray = JArray.Parse(element[11].GetRawText());
                        var otherInfoRaw = jsonArray[2].ToString().Split(';');
                        var imageRaw = jsonArray[0].ToString().Split(';');
                        pool.LogoUrl = imageRaw[0].ToString();
                        pool.BannerUrl = imageRaw[1].ToString();
                        pool.Description = jsonArray[1].ToString();
                        pool.WebsiteLink = otherInfoRaw[0];
                        pool.FacebookLink = otherInfoRaw[1];
                        pool.TwitterLink = otherInfoRaw[2];
                        pool.GithubLink = otherInfoRaw[3];
                        pool.TelegramLink = otherInfoRaw[4];
                        pool.InstagramLink = otherInfoRaw[5];
                        pool.DiscordLink = otherInfoRaw[6];
                        pool.RedditLink = otherInfoRaw[7];
                        pool.YoutubeLink = otherInfoRaw[8];
                        pool.PoolTitle = otherInfoRaw[9];
                        if (pool.TotalRaised > 0)
                            pool.RaisedPercentage = (pool.TotalRaised / pool.SoftCap.Value) * 100;
                        pools.Add(pool);
                    }
                    return new() { Status = true, Message = result.ToString(), Result = pools };
                }
                return new() { Status = false, Message = result.ToString() };
            }
            catch (Exception ex)
            {
                return new() { Status = false, Message = ex.Message };
            }
        }
        public async Task<Response<List<Contributor>>> GetContributorsAsync(int poolId)
        {
            try
            {
                object[] args;
                args = new object[2] { _contractAddress,poolId};
                var result = await javascript.InvokeAsync<object>("getContributors", args);
                List<JsonElement> elements = new List<JsonElement>();
                if (result.GetType() == typeof(JsonElement))
                {
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(result);
                    var jsonElement = JsonDocument.Parse(jsonString).RootElement;
                    elements.AddRange(jsonElement.EnumerateArray());
                }
                List<Contributor> contributors = new List<Contributor>();
                if (elements != null)
                {
                    foreach ( var element in elements)
                    {
                        var contributor = new Contributor();
                        Token token = new(javascript, element[4].GetString(), _accountAddress);
                        var tokenInfo = await token.GetTokenInformation(element[4].GetString());
                        contributor.PoolId = int.Parse(element[0].GetString());
                        contributor.ContributorAddress = element[1].GetString();
                        contributor.Amount = Nethereum.Web3.Web3.Convert.FromWei(System.Numerics.BigInteger.Parse(element[2].GetString()),tokenInfo.Result.Decimals);
                        contributor.TimeStamp = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(element[3].GetString())).LocalDateTime);
                        contributor.CurrencyUsed = tokenInfo.Result.Symbol;
                        contributors.Add(contributor);
                    }
                    return new() { Status = true, Message = result.ToString(), Result = contributors };
                }
                return new() { Status = false, Message = result.ToString() };
            }
            catch (Exception ex)
            {
                return new() { Status = false, Message = ex.Message };
            }
        }

        public async Task<Response<ContractResponse>> CreatePoolAsync(string tokenAddress,string adminWallet, long startTime, long endTime, decimal softCap, decimal hardCap, decimal minContribution, decimal maxContribution, string[] otherInfo, string[] tokens, decimal[] rates, bool[] isTokens)
        {
            try
            {
                var convertedValue = "";
                object[] args;
                args = new object[14] { _contractAddress, _accountAddress, tokenAddress, adminWallet, startTime, endTime, softCap, hardCap, minContribution, maxContribution, otherInfo, tokens, rates, isTokens };
                var result = await javascript.InvokeAsync<string>("createPool", args);
                if (result.ToString().Contains("status"))
                {
                    var finalResult = JsonConvert.DeserializeObject<ContractResponse>(result.ToString());
                    return new() { Result = finalResult, Status = finalResult.status, Message = result.ToString() };
                }
                return new() { Status = false, Message = result.ToString() };
            }
            catch (Exception ex)
            {
                return new() { Status = false, Message = ex.Message };
            }
        }
    }
}
