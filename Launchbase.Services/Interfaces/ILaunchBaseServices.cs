using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchbase.Services.Interfaces
{
    public interface ILaunchBaseServices
    {
        IToken Token { get; }
        IWallet Wallet { get; }
        public IPool Pool { get; }
    }
}
