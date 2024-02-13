using Microsoft.JSInterop;
using Newtonsoft.Json;
using Launchbase.Services.Interfaces;
using Launchbase.Services.Web3.Dtos;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Launchbase.Services.Web3
{
    public class Pool : IPool
    {
        private readonly IJSRuntime javascript;
        private readonly string _contractAddress;
        private readonly string _accountAddress;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public Pool(IJSRuntime javascript, string contractAddress, string accountAddress)
        {
            this.javascript = javascript;
            _accountAddress = accountAddress;
            _contractAddress = contractAddress;
        }
        public async Task<Response<ContractResponse>> Approve(string spenderAddress, string tokenAddress, decimal amount, decimal decimalResult)
        {
            try
            {
                double decimals = 0;
                string convertedValue = "";
                if (double.TryParse(decimalResult.ToString(), out decimals))
                {
                    var weiResult = double.Parse(amount.ToString()) * Math.Pow(10, decimals);
                    convertedValue = weiResult.ToString("F0");
                }
                object[] args = new object[] { tokenAddress, _accountAddress, spenderAddress, convertedValue.ToString() };
                var result = await javascript.InvokeAsync<object>("approvePayment", args);
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

        public async Task<Response<ContractResponse>> Exchange(string account, string tokenAddress, decimal paymentAmount, bool isToken)
        {
            try
            {
                var convertedValue = "";
                object[] args;
                if (isToken == false)
                {
                    double decimals = 18;
                    var weiResult = double.Parse(paymentAmount.ToString()) * Math.Pow(10, decimals);
                    convertedValue = weiResult.ToString("F0");
                    args = new object[6] { _contractAddress, _accountAddress, tokenAddress, convertedValue.ToString(), isToken, 16 };
                }
                else
                {
                    var decimalResult = await javascript.InvokeAsync<string>("GetDecimal", tokenAddress);
                    double decimals = 0;
                    if (double.TryParse(decimalResult, out decimals))
                    {
                        var weiResult = double.Parse(paymentAmount.ToString()) * Math.Pow(10, decimals);
                        convertedValue = weiResult.ToString("F0");
                    }
                    args = new object[6] { _contractAddress, _accountAddress, tokenAddress, convertedValue.ToString(), isToken, decimals };
                }
                var result = await javascript.InvokeAsync<string>("exchangeTokenForOBS", args);
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
        private async Task<string> ConvertToDecimal(string contractAddress, decimal amount)
        {
            try
            {
                var result = await javascript.InvokeAsync<string>("GetDecimal", contractAddress);
                decimal decimals = 0;
                if (decimal.TryParse(result, out decimals))
                {
                    var rawDecimals = amount.ToString().Split('.');
                    decimal totalZeros = rawDecimals.Length == 2 ? decimals - rawDecimals[1].Length : decimals;
                    string represented = amount.ToString().Replace(".", "");
                    for (int i = 0; i < totalZeros; i++)
                    {
                        represented += "0";
                    }
                    return represented;
                }
            }
            catch (Exception)
            {
                
            }
            return "0";
        }

        public async Task<Response<decimal>> GetExchangeRate(string tokenAddress)
        {
            object[] args = new object[] { _contractAddress, tokenAddress };
            var result = await javascript.InvokeAsync<object>("getExchangeRates", args);
            if (result.ToString() != null)
            {
                return new() { Result = decimal.Parse(result.ToString()), Status = true, Message = result.ToString() };
            }
            return new() { Status = false, Message = result.ToString() };
        }
        public async Task<Response<List<PoolInfo>>> GetAllPoolAsync()
        {
            try
            {
                object[] args;
                args = new object[2] { _contractAddress, _accountAddress};
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
                    foreach ( var element in elements)
                    {
                        var otherInfo = new string[3];
                        var pool = new PoolInfo();
                        Token token = new(javascript, element[0].GetString(), _accountAddress);
                        using (JsonDocument document = JsonDocument.Parse(element[11].GetRawText()))
                        {
                            pool.Currencies = new();
                            foreach (JsonElement currencyElement in document.RootElement.EnumerateArray())
                            {
                                pool.Currencies.Add(new()
                                {
                                    Address = currencyElement[0].GetString(),
                                    IsToken = currencyElement[1].GetBoolean(),
                                    Rate = decimal.Parse(currencyElement[2].GetString()),
                                });
                            }
                        }
                        var tokenInfo = await token.GetTokenInformation(element[0].GetString());
                        pool.Token = tokenInfo.Result;
                        pool.Token.Address = element[0].GetString();
                        pool.AdminWallet = element[1].GetString();
                        pool.StartTime = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(element[2].GetString())).LocalDateTime);
                        pool.EndTime = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(element[3].GetString())).LocalDateTime);
                        pool.TotalRaised = decimal.Parse(element[4].GetString());
                        pool.SoftCap = decimal.Parse(element[5].GetString());
                        pool.HardCap = decimal.Parse(element[6].GetString());
                        pool.State = (PoolState)int.Parse(element[7].GetString());
                        pool.MinimumBuy = decimal.Parse(element[8].GetString());
                        pool.MaximumBuy = decimal.Parse(element[9].GetString());
                        JArray jsonArray = JArray.Parse(element[10].GetRawText());
                        
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
                        if (pool.TotalRaised>0)
                            pool.RaisedPercentage = (pool.SoftCap.Value / pool.TotalRaised) * 100;
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

        public async Task<Response<ContractResponse>> CreatePoolAsync(string tokenAddress,string adminWallet, long startTime, long endTime, decimal softCap, decimal hardCap, decimal minContribution, decimal maxContribution, string[] otherInfo, string[] tokens, decimal[] rates)
        {
            try
            {
                var convertedValue = "";
                object[] args;
                args = new object[13] { _contractAddress, _accountAddress, tokenAddress, adminWallet, startTime, endTime, softCap, hardCap, minContribution, maxContribution, otherInfo, tokens, rates };
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
