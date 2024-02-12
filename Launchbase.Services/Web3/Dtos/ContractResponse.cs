using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchbase.Services.Web3.Dtos
{
    public class ContractResponse
    {
        public string blockHash { get; set; }
        public bool status { get; set; }
        public string from { get; set; }
        public string transactionHash { get; set; }
        public string type { get; set; }
    }
}
