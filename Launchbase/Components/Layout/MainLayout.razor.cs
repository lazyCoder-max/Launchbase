using MetaMask.Blazor;
using MetaMask.Blazor.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using Launchbase.Services;
using Launchbase.Services.Interfaces;
using Launchbase.Dtos;

namespace Launchbase.Components.Layout
{
    public partial class MainLayout
    {
        private bool isConnected { get; set; } = false;
        private string getAddress { get; set; } = "Connect";
        private string signature { get; set; } = "";
        private string chainLogoUrl { get; set; }
        private string chainName { get; set; }
        [Inject] IJSRuntime javaScript { get; set; }
        [Inject] IMetaMaskService metamask { get; set; }
        [Inject] private ISnackbar Snackbr { get; set; }
        [Inject] private ChainStateContainer Container { get; set; }
        private Chain SelectedChain { get; set; }
        public List<Chain> Chains { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            Program.Configuration.GetSection("ContractAddress").Bind(Chains);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                IMetaMaskService.AccountChangedEvent += IMetaMaskService_AccountChangedEvent;
                IMetaMaskService.ChainChangedEvent += IMetaMaskService_ChainChangedEvent;
                IMetaMaskService.OnDisconnectEvent += IMetaMaskService_OnDisconnectEvent;
                ILaunchBaseServices service = new LaunchBaseServices(javaScript, "");
                var boolHasMetamask = await metamask.HasMetaMask();
                if (boolHasMetamask)
                {
                    await metamask.ListenToEvents();
                }
            }
        }
        private async void ConnectWalletAsync()
        {
            try
            {
                isConnected = await metamask.IsSiteConnected();
                if (!isConnected)
                {
                    if (await metamask.HasMetaMask())
                    {
                        await metamask.ConnectMetaMask();
                        isConnected = await metamask.IsSiteConnected();
                        if (isConnected)
                            signature = await metamask.PersonalSign("The information provided shall not in any way constitute a recommendation as to whether you should invest in any product discussed. We accept no liability for any loss occasioned to any person acting or refraining from action as a result of any material provided or published.");
                        var connectedChain = await metamask.GetSelectedChain();
                        CheckConnectedChainCompatability(connectedChain.chainId);
                    }
                    else
                    {
                        Snackbr.Add("Metamask is not installed!", Severity.Warning);
                    }

                }
                else
                {
                    isConnected = true;

                    var connectedChain = await metamask.GetSelectedChain();
                    CheckConnectedChainCompatability(connectedChain.chainId);
                }
            }
            catch (UserDeniedException deniedEx)
            {
                Snackbr.Add($"{deniedEx.Message}", Severity.Warning);
            }
            catch (Exception)
            {
            }
            StateHasChanged();
        }

        private void IMetaMaskService_OnDisconnectEvent()
        {
            DisconnectWalletAsync();
        }

        private async Task IMetaMaskService_ChainChangedEvent((long, MetaMask.Blazor.Enums.Chain) arg)
        {
            var result = CheckConnectedChainCompatability(arg.Item1);
            if (!result.Item1)
                Snackbr.Add(result.Item2, Severity.Warning);
            StateHasChanged();
        }

        private async Task IMetaMaskService_AccountChangedEvent(string arg)
        {
        }

        private void DisconnectWalletAsync()
        {
            isConnected = false;
            StateHasChanged();
        }
        private (bool, string) CheckConnectedChainCompatability(long chain)
        {
            if (!Chains.Any(x => x.ActiveChainId == chain))
            {
                string chains = $"";
                foreach (var item in Chains.Select(x => x.DeployedChain))
                {
                    chains += $"{item} ,";
                }
                chainName = $"Chain Id: {chain} is not acceptable, please use only {chains}";
                chainLogoUrl = "";
                return (true, chainName);
            }
            else
            {
                SelectedChain = Chains.FirstOrDefault(x => x.ActiveChainId == chain);
                chainLogoUrl = SelectedChain.ChainLogoUrl;
                chainName = SelectedChain.DeployedChain;
                Container.SetValue(SelectedChain);
            }
            return (true, null);
        }
    }
}
