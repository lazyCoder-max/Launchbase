using Fluxor;
using Launchbase.Dtos;
using Launchbase.Services.Web3.Dtos;
using Launchbase.Store.PoolUseCase.Actions;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Text.Json;

namespace Launchbase.Components.Pages
{
    public partial class SaleList
    {
        [Inject] IJSRuntime javaScript { get; set; }
        [Inject] IMetaMaskService metamask { get; set; }
        [Inject] private ISnackbar Snackbr { get; set; }
        [Inject] private IDispatcher _dispatcher { get; set; }
        [Inject] private IActionSubscriber _actionSubscriber { get; set; }
        [Inject] private IState<Store.PoolUseCase.PoolToken> PoolState { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] private ChainStateContainer SelectedChain { get; set; }
        private bool IsLoading { get; set; } = true;
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                _dispatcher.Dispatch(new GetPool.Action(javaScript, metamask, SelectedChain.Value));
                _actionSubscriber.SubscribeToAction<GetPool.ResultAction>(this, action =>
                {
                    if (action.PoolStatus.Status == Store.PoolUseCase.TaskStatus.Created)
                    {
                        IsLoading = false;
                        StateHasChanged();
                    }
                });
            }
        }
        private void ViewDetail(PoolInfo pool)
        {
            var queryString = $"?pool={Uri.EscapeDataString(JsonSerializer.Serialize(pool))}";
            _navigationManager.NavigateTo("/presale-details" + queryString);
        }
    }
}
