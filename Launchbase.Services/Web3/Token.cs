using Microsoft.JSInterop;
using Newtonsoft.Json;
using Launchbase.Services.Interfaces;
using Launchbase.Services.Web3.Dtos;

namespace Launchbase.Services.Web3
{
    public class Token : IToken
    {
        private readonly IJSRuntime javascript;
        private readonly string _contractAddress;
        private readonly string _accountAddress;
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        public Token(IJSRuntime javascript, string contractAddress, string accountAddress)
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

        public async Task<Response<int>> GetTokenDecimal(string contractAddress)
        {
            try
            {
                var result = await javascript.InvokeAsync<string>("GetTokenDecimal", contractAddress);
                int decimals = 0;
                if (int.TryParse(result, out decimals))
                {
                    return new Response<int>
                    {
                        Status = true,
                        Result = decimals
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<int>
                {
                    Status = false,
                    Result = -1,
                    Message = ex.Message
                };
            }
            return new Response<int>
            {
                Status = false,
                Result = -1,
            };
        }

        public async Task<Response<string>> GetTokenName(string contractAddress)
        {
            string message = "";
            try
            {
                var result = await javascript.InvokeAsync<string>("GetTokenName", contractAddress);
                if (result.Length >= 1)
                {
                    return new Response<string>
                    {
                        Status = true,
                        Result = result
                    };
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new Response<string>
            {
                Status = false,
                Message = message
            };
        }

        public async Task<Response<string>> GetTokenSymbol(string contractAddress)
        {
            string message = "";
            try
            {
                var result = await javascript.InvokeAsync<string>("GetTokenSymbol", contractAddress);
                if (result.Length >= 1)
                {
                    return new Response<string>
                    {
                        Status = true,
                        Result = result
                    };
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new Response<string>
            {
                Status = false,
                Message = message
            };
        }

        public async Task<Response<long>> GetTokenTotalSupply(string contractAddress)
        {
            string message = "";
            try
            {
                var result = await javascript.InvokeAsync<string>("GetTokenTotalSupply", contractAddress);
                long decimals = 0;
                if (long.TryParse(result, out decimals))
                {
                    return new Response<long>
                    {
                        Status = true,
                        Result = decimals
                    };
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new Response<long>
            {
                Status = false,
                Message = message
            };
        }

        public async Task<Response<TokenInfo>> GetTokenInformation(string contractAddress)
        {
            string message = "";
            try
            {
                var result = await javascript.InvokeAsync<string>("GetTokenInformation", contractAddress);

                if (result.Length >= 1)
                {
                    var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(result);
                    if (tokenInfo != null)
                    {
                        tokenInfo.TotalSupply = Nethereum.Web3.Web3.Convert.FromWei(System.Numerics.BigInteger.Parse(tokenInfo.TotalSupply.ToString()));
                        return new Response<TokenInfo>
                        {
                            Status = true,
                            Result = tokenInfo
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new Response<TokenInfo>
            {
                Status = false,
                Message = message
            };
        }
    }
}
