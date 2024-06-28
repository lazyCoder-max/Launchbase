using Fluxor;
using MetaMask.Blazor;
using Microsoft.JSInterop;
using Launchbase.Services.Interfaces;
using Launchbase.Services;

namespace Launchbase.Store.TokenUseCase.Actions
{
    public class ApproveToken
    {
        public record Action(IJSRuntime JSRuntime = null, IMetaMaskService metamask = null, Token token = null)
        {
            [ReducerMethod]
            public static Token Reducer(Token state, Action action)
            {
                return state with
                {
                    Address = action.token.Address,
                    Decimals = action.token.Decimals,
                    Name = action.token.Name,
                    Symbol = action.token.Symbol,
                    TotalSupply = action.token.TotalSupply,
                    ApproveStatus = new TokenStatus<TaskStatus>()
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
                    if (action.token.Address is null)
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Set Token Address First!", Status = TaskStatus.Failed }, action.token));
                        return;
                    }
                    var address = await action.metamask.GetSelectedAddress();
                    var chain = await action.metamask.GetSelectedChain();
                    ILaunchBaseServices services = new LaunchBaseServices(action.JSRuntime, action.token.Address, address);
                    var spenderAddress = Program.Configuration.GetRequiredSection($"ContractAddress:{chain.chainId.ToString()}").GetValue<string>("Address");
                    var isApproved = await services.Token.Approve(spenderAddress,action.token.Address, action.token.TotalSupply.Value, action.token.Decimals.Value);
                    if (isApproved.Status)
                    {

                        dispatcher.Dispatch(new ResultAction(new()
                        {
                            Message = isApproved.Status == true ? "Token Approved!" : "Transaction Failed!",
                            Status = isApproved.Status == true ? TaskStatus.Created : TaskStatus.Failed,
                        }, 
                            action.token
                        ));
                    }
                    else
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Approval Failed!", Status = TaskStatus.Failed }, action.token));
                    }
                }
                catch (Exception e)
                {

                    dispatcher.Dispatch(new ResultAction(new()
                    {
                        Message = e.Message,
                        Status = TaskStatus.Failed
                    }, action.token));
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
                    FetchStatus = action.Token.FetchStatus,
                    ApproveStatus = action.TokenStatus,
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
