using Microsoft.JSInterop;
using Launchbase.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Launchbase.Services.Interfaces
{
    public interface IWallet
    {
        public static event Func<string, Task>? AccountChangedEvent;
        public static event Func<(long, Chain), Task>? ChainChangedEvent;

        /// <summary>
        /// https://docs.metamask.io/guide/ethereum-provider.html#connect
        /// </summary>
        public static event Action? OnConnectEvent;

        /// <summary>
        /// https://docs.metamask.io/guide/ethereum-provider.html#disconnect
        /// </summary>
        public static event Action? OnDisconnectEvent;
        Task<bool> IsConnected();
        Task<bool> ConnectWalletAsync();
        Task<string> SignPersonal(string message);

        [JSInvokable()]
        static void OnConnect()
        {
            if (OnConnectEvent != null)
            {
                OnConnectEvent.Invoke();
            }
        }

        [JSInvokable()]
        static void OnDisconnect()
        {
            if (OnDisconnectEvent != null)
            {
                OnDisconnectEvent.Invoke();
            }
        }
        [JSInvokable()]
        static async Task OnAccountsChanged(string selectedAccount)
        {
            if (AccountChangedEvent != null)
            {
                await AccountChangedEvent.Invoke(selectedAccount);
            }
        }

    }
}
