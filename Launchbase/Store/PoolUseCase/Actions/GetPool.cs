using Fluxor;
using Launchbase.Services.Interfaces;
using Launchbase.Services;
using Launchbase.Store.PoolUseCase;
using MetaMask.Blazor;
using Microsoft.JSInterop;
using Launchbase.Services.Web3.Dtos;
using Launchbase.Dtos;

namespace Launchbase.Store.PoolUseCase.Actions
{
    public class GetPool
    {
        public record Action(IJSRuntime JSRuntime = null, IMetaMaskService metamask = null, Chain selectedChain=null)
        {
            [ReducerMethod]
            public static PoolToken Reducer(PoolToken state, Action action)
            {
                return state with
                {
                   
                    FetchStatus = new()
                    {
                        Status = PoolUseCase.TaskStatus.Processing
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
                    var address = await action.metamask.GetSelectedAddress();
                    var spenderAddress = action.selectedChain.Address;
                    ILaunchBaseServices services = new LaunchBaseServices(action.JSRuntime, spenderAddress, address,action.selectedChain.GasAPI);

                    var isApproved = await services.Pool.GetAllPoolAsync();
                    if (isApproved.Status)
                    {

                        dispatcher.Dispatch(new ResultAction(new()
                        {
                            Message = isApproved.Status == true ? "Pool Created!" : "Transaction Failed!",
                            Status = isApproved.Status == true ? PoolUseCase.TaskStatus.Created : PoolUseCase.TaskStatus.Failed,
                        },isApproved.Result));
                    }
                    else
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Pool Creation Failed!", Status = PoolUseCase.TaskStatus.Failed }));
                    }
                }
                catch (Exception e)
                {

                    dispatcher.Dispatch(new ResultAction(new()
                    {
                        Message = e.Message,
                        Status = PoolUseCase.TaskStatus.Failed
                    }));
                }
            }
        }
        public record ResultAction(PoolStatus<PoolUseCase.TaskStatus> PoolStatus = new(), List<PoolInfo>? Pools = null)
        {
            [ReducerMethod]
            public static PoolToken Reducer(PoolToken state, ResultAction action)
            {
                return state with
                {
                    CreateStatus = action.PoolStatus,
                    Pools = action.Pools
                };
            }
        }
    }
}
