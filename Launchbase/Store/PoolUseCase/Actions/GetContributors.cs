using Fluxor;
using Launchbase.Services.Interfaces;
using Launchbase.Services;
using MetaMask.Blazor;
using Microsoft.JSInterop;
using Launchbase.Services.Web3.Dtos;
using Launchbase.Dtos;

namespace Launchbase.Store.PoolUseCase.Actions
{
    public class GetContributors
    {
        public record Action(IJSRuntime JSRuntime = null, IMetaMaskService metamask = null, int? poolId =null,Chain selectedChain=null)
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
                    if (action.poolId is null)
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Select Pool First!", Status = TaskStatus.Failed }));
                        return;
                    }
                    var address = await action.metamask.GetSelectedAddress();
                    var spenderAddress = action.selectedChain.Address;
                    ILaunchBaseServices services = new LaunchBaseServices(action.JSRuntime, spenderAddress, address, action.selectedChain.GasAPI);

                    var isApproved = await services.Pool.GetContributorsAsync(action.poolId.Value);
                    if (isApproved.Status)
                    {

                        dispatcher.Dispatch(new ResultAction(new()
                        {
                            Message = isApproved.Status == true ? "Contribution Minted!" : "Transaction Failed!",
                            Status = isApproved.Status == true ? TaskStatus.Created : TaskStatus.Failed,
                        }, isApproved.Result
                        ));
                    }
                    else
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Contribution Failed!", Status = TaskStatus.Failed }));
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
        public record ResultAction(ContributionStatus<TaskStatus> ContributionStatus = new(), List<Contributor> Contributors = null)
        {
            [ReducerMethod]
            public static Contribution Reducer(Contribution state, ResultAction action)
            {
                return state with
                {
                    CreateStatus = action.ContributionStatus,
                    Contributors = action.Contributors
                };
            }
        }
    }
}
