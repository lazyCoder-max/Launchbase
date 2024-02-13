using Fluxor;
using Microsoft.AspNetCore.Components;

namespace Launchbase.Components.Pages
{
    public partial class Presale
    {
         int step = 1;
        [Inject] public IState<Store.PoolUseCase.PoolToken> PoolState { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public void JumpToNextStep()
        {
            step+=1;
            StateHasChanged();
        }
        public void BackToPreviousStep()
        {
            step -= 1;
            StateHasChanged();
        }
    }
}
