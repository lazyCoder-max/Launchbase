using Fluxor;
using Launchbase.Store.PresaleUseCase.Actions;
using Launchbase.Store.TokenUseCase.Actions;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Launchbase.Components.Presale
{
    public partial class Step4
    {
        [Parameter]
        public Pages.Presale Presale { get; set; }
        [Inject] IJSRuntime javaScript { get; set; }
        [Inject] IMetaMaskService metamask { get; set; }
        [Inject] private ISnackbar Snackbr { get; set; }
        [Inject] private IDispatcher _dispatcher { get; set; }
        [Inject] private IActionSubscriber _actionSubscriber { get; set; }
        public void Submit()
        {
            _dispatcher.Dispatch(new CreatePoolToken.Action(javaScript, metamask, Presale.PresaleState.Value));
            _actionSubscriber.SubscribeToAction<CreatePoolToken.ResultAction>(this, action =>
            {
                if (action.PoolStatus.Status == Store.PresaleUseCase.TaskStatus.Created)
                {
                    Snackbr.Add(action.PoolStatus.Message, Severity.Success);

                    StateHasChanged();
                }
                else if (action.PoolStatus.Status == Store.PresaleUseCase.TaskStatus.Failed)
                {
                    Snackbr.Add(action.PoolStatus.Message, Severity.Warning);
                    StateHasChanged();
                }
            });
        }
    }
}
