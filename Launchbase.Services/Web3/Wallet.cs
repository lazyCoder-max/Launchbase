using Microsoft.JSInterop;
using Launchbase.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Launchbase.Services.Web3
{
    public class Wallet : IWallet
    {
        private readonly IJSRuntime jsScript;

        public Wallet(IJSRuntime jsScript)
        {
            this.jsScript = jsScript;
        }

        public async Task<bool> ConnectWalletAsync()
        {
            var result = await jsScript.InvokeAsync<object>("connectWallet");
            if (typeof(JsonElement) == result.GetType())
            {
                var jsonElement = (JsonElement)result;
                if(jsonElement.ValueKind == JsonValueKind.String)
                {
                    var resultValue = (string)jsonElement.ValueKind.ToString();
                    if (resultValue.Contains("x"))
                        return true;
                }
            }
            return false;
        }

        public async Task<bool> IsConnected()
        {
            var result  = await jsScript.InvokeAsync<object>("isConnected");
            if(typeof(JsonElement)== result.GetType())
            {
                var jsonElement = (JsonElement)result;
                if(jsonElement.ValueKind == JsonValueKind.False || jsonElement.ValueKind == JsonValueKind.True)
                {
                    return bool.Parse(jsonElement.ValueKind.ToString());
                }
            }
            if(result!=null)
            {
                if (result.ToString().Contains("true"))
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<string> SignPersonal(string message)
        {
            var result =  await jsScript.InvokeAsync<object>("signAccount",message);
            if (typeof(JsonElement) == result.GetType())
            {
                var jsonElement = (JsonElement)result;
                if (jsonElement.ValueKind == JsonValueKind.String)
                {
                    var resultValue = (string)jsonElement.ValueKind.ToString();
                    return resultValue;
                }
            }
            return "Access Denied";
        }
    }
}
