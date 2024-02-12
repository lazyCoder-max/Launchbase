using Fluxor;
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
        [Inject] public IState<Store.TokenUseCase.Token> _tokenState { get; set; }
        [Inject] private IDispatcher _dispatcher { get; set; }
        [Inject] private IActionSubscriber _actionSubscriber { get; set; }
        public void Submit()
        {

        }
    }
}
