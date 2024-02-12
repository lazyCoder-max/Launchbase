using Fluxor;
using Launchbase.Store.TokenUseCase;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Nethereum.JsonRpc.Client;
using Newtonsoft.Json;

namespace Launchbase.Components.Presale
{
    public partial class Step2
    {
        [Parameter]
        public Pages.Presale Presale { get; set; }
        [Inject] IJSRuntime javaScript { get; set; }
        [Inject] IMetaMaskService metamask { get; set; }
        [Inject] private ISnackbar Snackbr { get; set; }
        [Inject] public IState<Store.TokenUseCase.Token> _tokenState { get; set; }
        [Inject] private IDispatcher _dispatcher { get; set; }
        [Inject] private IActionSubscriber _actionSubscriber { get; set; }

        public List<Currency> Currencies { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Currencies = new();
            Program.Configuration.GetSection("Currencies").Bind(Currencies);
            
        }
        private void ContinueBtn()
        {
            var selectedCurrency = Currencies.Where(x=>x.IsSelected==true).ToList();
            if(selectedCurrency.Count>=1 )
            {
                if (ValidateData())
                    return;
                Presale.JumpToNextStep();
            }
            else 
            {
                Snackbr.Add("Please select atleast one currency!", Severity.Warning);
            }
            
        }
        private bool ValidateData()
        {
            if (Presale.PresaleState.Value.SoftCap <= 1)
            {
                Snackbr.Add("Softcap must be >=1", Severity.Warning);
                return true;
            }
            var hardCap = Presale.PresaleState.Value.SoftCap * 15 / 100;
            if (Presale.PresaleState.Value.HardCap <= hardCap)
            {
                Snackbr.Add("Hardcap must be less than the softcap", Severity.Warning);
                return true;
            }
            if (Presale.PresaleState.Value.MinimumBuy <= 0)
            {
                Snackbr.Add("Minimum buy must be >=1", Severity.Warning);
                return true;
            }
            if (Presale.PresaleState.Value.MaximumBuy <= 0)
            {
                Snackbr.Add("Maximum buy must be >=1", Severity.Warning);
                return true;
            }
            if (Presale.PresaleState.Value.MaximumBuy <= Presale.PresaleState.Value.MinimumBuy)
            {
                Snackbr.Add("Maximum buy must be >= Minimum Buy", Severity.Warning);
                return true;
            }
            if (Presale.PresaleState.Value.StartTime.HasValue== false)
            {
                Snackbr.Add("Start time needs to be filled", Severity.Warning);
                return true;
            }
            if (Presale.PresaleState.Value.EndTime.HasValue == false)
            {
                Snackbr.Add("End time needs to be filled", Severity.Warning);
                return true;
            }
            if (Presale.PresaleState.Value.EndTime.Value <= Presale.PresaleState.Value.StartTime.Value)
            {
                Snackbr.Add("Start time needs to be < End time", Severity.Warning);
                return true;
            }
            return false;
        }
    }
}
