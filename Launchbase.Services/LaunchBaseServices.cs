using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Launchbase.Services.Interfaces;
using Launchbase.Services.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchbase.Services
{
    public class LaunchBaseServices:ILaunchBaseServices
    {
        private readonly Lazy<IWallet> _lazyWalletServices;
        private readonly Lazy<IToken> _lazyTokenServices;
        private readonly Lazy<IPool> _lazyPoolServices;
        public LaunchBaseServices(IJSRuntime jSRuntime, string contractAddress, string account="")
        {
            _lazyTokenServices = new(()=>new Token(jSRuntime,contractAddress,account));
            _lazyPoolServices = new(() => new Pool(jSRuntime, contractAddress, account));
            _lazyWalletServices = new(() => new Wallet(jSRuntime));
        }
        public IWallet Wallet => _lazyWalletServices.Value;
        public IToken Token => _lazyTokenServices.Value;
        public IPool Pool => _lazyPoolServices.Value;
    }
}
