using Fluxor;
using MetaMask.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Net;

namespace Launchbase.Components.Pages
{
    public partial class Home
    {
        [Inject] IJSRuntime javaScript { get; set; }
        public async Task PlaySoundAsync()
        {
            await javaScript.InvokeVoidAsync("playSound");
        }
    }
}
