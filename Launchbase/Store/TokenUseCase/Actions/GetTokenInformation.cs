using Fluxor;
using MetaMask.Blazor;
using Microsoft.JSInterop;
using Launchbase.Services.Interfaces;
using Launchbase.Services;

namespace Launchbase.Store.TokenUseCase.Actions
{
    public class GetTokenInformation
    {
        public record Action(IJSRuntime JSRuntime = null, IMetaMaskService metamask = null, string tokenAddress = null)
        {
            [ReducerMethod]
            public static Token Reducer(Token state, Action action)
            {
                return state with
                {
                    Decimals = 0,
                    Name = "",
                    Symbol = "",
                    TotalSupply = 0,
                    FetchStatus = new TokenStatus<TaskStatus>()
                    {
                        Status = TaskStatus.Processing
                    },
                };
            }
        }
        private record Effects()
        {
            [EffectMethod]
            public async Task HandlerAsync(Action action, IDispatcher dispatcher)
            {
                try
                {
                    await Task.Yield();
                    if (action.tokenAddress is null)
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Input Token First!", Status = TaskStatus.Failed }));
                        return;
                    }
                    var address = await action.metamask.GetSelectedAddress();
                    ILaunchBaseServices services = new LaunchBaseServices(action.JSRuntime, action.tokenAddress, address);
                    var isFetched = await services.Token.GetTokenInformation(action.tokenAddress);
                    if (isFetched.Status)
                    {
                        dispatcher.Dispatch(new ResultAction(new()
                        {
                            Message = isFetched.Status == true ? "Transaction Created!" : "Transaction Failed!",
                            Status = isFetched.Status == true ? TaskStatus.Created : TaskStatus.Failed,
                        }, new()
                        {
                            Address = action.tokenAddress,
                            Decimals = isFetched.Result.Decimals,
                            Name = isFetched.Result.Name,
                            Symbol = isFetched.Result.Symbol,
                            TotalSupply = isFetched.Result.TotalSupply,
                        }
                        ));
                    }
                    else
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Transaction Failed!", Status = TaskStatus.Failed }));
                    }
                }
                catch (Exception e)
                {

                    dispatcher.Dispatch(new ResultAction(new()
                    {
                        Message = e.Message,
                        Status = TaskStatus.Failed
                    }));
                }
            }
        }
        public record ResultAction(TokenStatus<TaskStatus> TokenStatus = new(), Token? Token = null)
        {
            [ReducerMethod]
            public static Token Reducer(Token state, ResultAction action)
            {
                return state with
                {
                    FetchStatus = action.TokenStatus,
                    Address = action.Token?.Address,
                    Name = action.Token?.Name,
                    Symbol = action.Token?.Symbol,
                    TotalSupply = action.Token?.TotalSupply,
                    Decimals = action.Token?.Decimals
                };
            }
        }
    }
}
