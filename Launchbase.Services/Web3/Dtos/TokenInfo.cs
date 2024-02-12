using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchbase.Services.Web3.Dtos
{
    public class TokenInfo
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal TotalSupply { get; set; }
        public int Decimals { get; set; }
    }
}
