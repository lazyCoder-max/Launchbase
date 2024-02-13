using Fluxor;
using Launchbase.Store.TokenUseCase.Actions;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Launchbase.Components.Presale
{
    public partial class Step1
    {
        [Inject] IMetaMaskService metamask { get; set; }
        [Inject] private ISnackbar Snackbr { get; set; }
        [Inject] private IDispatcher _dispatcher { get; set; }
        [Inject] private IActionSubscriber _actionSubscriber { get; set; }
        [Inject] IJSRuntime javaScript { get; set; }
        [Inject] public IState<Store.TokenUseCase.Token> _tokenState { get; set; }
        bool isApproved = false;

        [Parameter]
        public Pages.Presale Presale { get; set; }
        private async Task FetchTokenInformationAsync(ChangeEventArgs args)
        {
            if (args.Value?.ToString() != null)
            {
                _dispatcher.Dispatch(new GetTokenInformation.Action(javaScript, metamask, args.Value.ToString()));

                // Subscribe to the action

                _actionSubscriber.SubscribeToAction<GetTokenInformation.ResultAction>(this, action =>
                {
                    StateHasChanged();
                });
            }
            else
            {
                // Handle invalid input if needed
            }
        }
        private void ApproveToken()
        {
            _dispatcher.Dispatch(new ApproveToken.Action(javaScript, metamask, _tokenState.Value));
            _actionSubscriber.SubscribeToAction<ApproveToken.ResultAction>(this, action =>
            {
                if(action.TokenStatus.Status == Store.TokenUseCase.TaskStatus.Created)
                {
                    Snackbr.Add(action.TokenStatus.Message, Severity.Success);
                    isApproved = true;
                    StateHasChanged();
                }
                else if(action.TokenStatus.Status == Store.TokenUseCase.TaskStatus.Failed)
                {
                    Snackbr.Add(action.TokenStatus.Message, Severity.Warning);
                    isApproved = false;
                    StateHasChanged();
                }
            });
        }
        private void MoveToNextStep()
        {
            Presale.PoolState.Value.Token = _tokenState.Value;
            Presale.JumpToNextStep();
        }
    }
}
