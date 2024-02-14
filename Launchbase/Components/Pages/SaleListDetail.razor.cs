using Fluxor;
using Launchbase.Services.Web3.Dtos;
using Launchbase.Store.PoolUseCase;
using Launchbase.Store.PoolUseCase.Actions;
using Launchbase.Store.TokenUseCase;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using MudBlazor;
using Newtonsoft.Json;

namespace Launchbase.Components.Pages
{
    public partial class SaleListDetail
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject] IJSRuntime javaScript { get; set; }
        [Inject] IMetaMaskService metamask { get; set; }
        [Inject] private ISnackbar Snackbr { get; set; }
        [Inject] private IDispatcher _dispatcher { get; set; }
        [Inject] private IState<Contribution> _ContributionState { get; set; }
        [Inject] private IActionSubscriber _actionSubscriber { get; set; }
        [Parameter]
        public string TransactionAddress { get; set; }
        [Parameter]
        public string ChainId { get; set; }
        [Parameter]
        public PoolInfo Pool { get; set; }
        Contribution Contribution { get; set; }
        private TokenInfo SelectedCurrency { get; set; }
        private string SelectedCurrencySymbol { get; set; }

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("pool", out var poolValue))
            {
                Pool = JsonConvert.DeserializeObject<PoolInfo>(poolValue);
            }
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("chainid", out var chainIdValue))
            {
                ChainId = chainIdValue;
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("transactionId", out var transactionIdValue))
            {
                TransactionAddress = transactionIdValue;
            }
            Contribution = new();
            _dispatcher.Dispatch(new GetContributors.Action(javaScript, metamask, Pool.PoolId));
            _actionSubscriber.SubscribeToAction<GetContributors.ResultAction>(this, action =>
            {
                StateHasChanged();
            });
            
        }
        private async Task SelectionChanged(string value)
        {
            SelectedCurrency = Pool.Currencies.FirstOrDefault(currency => currency.Address == value);
            if (value == "0x0000000000000000000000000000000000001010")
                SelectedCurrencySymbol = "MATIC";
            else
                SelectedCurrencySymbol = SelectedCurrency.Symbol;
        }
        private void SetMaximumContribution()
        {
            Contribution.Amount = Pool.MaximumBuy;

        }
        private void Contribute() 
        {
            Contribution.PoolId = Pool.PoolId;
            Contribution.CurrencyAddress = SelectedCurrency.Address;
            Contribution.IsCurrencyToken = Pool.Token.IsToken;
            _dispatcher.Dispatch(new Contribute.Action(javaScript, metamask, Contribution));
            _actionSubscriber.SubscribeToAction<Contribute.ResultAction>(this, action =>
            {
                if (action.ContributionStatus.Status == Store.PoolUseCase.TaskStatus.Created)
                {
                    Snackbr.Add(action.ContributionStatus.Message, Severity.Success);
                }
                else if (action.ContributionStatus.Status == Store.PoolUseCase.TaskStatus.Failed)
                {
                    Snackbr.Add(action.ContributionStatus.Message, Severity.Warning);
                    StateHasChanged();
                }
            });
        }
    }
}
