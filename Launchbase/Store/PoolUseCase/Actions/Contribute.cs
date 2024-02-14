using Fluxor;
using Launchbase.Services.Interfaces;
using Launchbase.Services;
using MetaMask.Blazor;
using Microsoft.JSInterop;

namespace Launchbase.Store.PoolUseCase.Actions
{
    public class Contribute
    {
        public record Action(IJSRuntime JSRuntime = null, IMetaMaskService metamask = null, Contribution pool = null)
        {
            [ReducerMethod]
            public static Contribution Reducer(Contribution state, Action action)
            {
                return state with
                {
                    CreateStatus = new ContributionStatus<TaskStatus>()
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
                    if (action.pool.Amount is null)
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Set Contribution Amount First!", Status = TaskStatus.Failed }));
                        return;
                    }
                    var address = await action.metamask.GetSelectedAddress();
                    var spenderAddress = Program.Configuration.GetRequiredSection("ContractAddress").GetValue<string>("Address");
                    ILaunchBaseServices services = new LaunchBaseServices(action.JSRuntime, spenderAddress, address);

                    if (action.pool.IsCurrencyToken.Value)
                    {
                        var isApproved = await services.Token.Approve(spenderAddress, action.pool.CurrencyAddress, action.pool.Amount.Value, action.pool.Decimals.Value);
                        if (isApproved.Status)
                        {
                            var isContributed = await services.Pool.ContributeAsync(address, action.pool.CurrencyAddress, action.pool.Amount.Value, action.pool.IsCurrencyToken.Value, action.pool.PoolId.Value);
                            if (isContributed.Status)
                            {

                                dispatcher.Dispatch(new ResultAction(new()
                                {
                                    Message = isContributed.Status == true ? "Contribution Minted!" : "Transaction Failed!",
                                    Status = isContributed.Status == true ? TaskStatus.Created : TaskStatus.Failed,
                                }
                                ));
                            }
                            else
                            {
                                dispatcher.Dispatch(new ResultAction(new() { Message = "Contribution Failed!", Status = TaskStatus.Failed }));
                            }
                        }
                        else
                        {
                            dispatcher.Dispatch(new ResultAction(new() { Message = "Contribution Failed!", Status = TaskStatus.Failed }));
                        }
                    }
                    else
                    {
                        var isContributed = await services.Pool.ContributeAsync(address, action.pool.CurrencyAddress, action.pool.Amount.Value, action.pool.IsCurrencyToken.Value, action.pool.PoolId.Value);
                        if (isContributed.Status)
                        {

                            dispatcher.Dispatch(new ResultAction(new()
                            {
                                Message = isContributed.Status == true ? "Contribution Minted!" : "Transaction Failed!",
                                Status = isContributed.Status == true ? TaskStatus.Created : TaskStatus.Failed,
                            }
                            ));
                        }
                        else
                        {
                            dispatcher.Dispatch(new ResultAction(new() { Message = "Contribution Failed!", Status = TaskStatus.Failed }));
                        }
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
        public record ResultAction(ContributionStatus<TaskStatus> ContributionStatus = new())
        {
            [ReducerMethod]
            public static Contribution Reducer(Contribution state, ResultAction action)
            {
                return state with
                {
                    CreateStatus = action.ContributionStatus
                };
            }
        }
    }
}
