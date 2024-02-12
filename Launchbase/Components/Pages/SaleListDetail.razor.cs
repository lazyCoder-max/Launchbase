using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Launchbase.Components.Pages
{
    public partial class SaleListDetail
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string TransactionAddress { get; set; }
        [Parameter]
        public string ChainId { get; set; }

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("chainid", out var chainIdValue))
            {
                ChainId = chainIdValue;
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("transactionId", out var transactionIdValue))
            {
                TransactionAddress = transactionIdValue;
            }
        }
    }
}
