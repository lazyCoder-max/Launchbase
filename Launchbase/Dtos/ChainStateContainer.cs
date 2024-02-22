namespace Launchbase.Dtos
{
    public class ChainStateContainer
    {
        public Chain Value { get; set; }
        public event Action OnStateChange;
        public void SetValue(Chain value)
        {
            this.Value = value;
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
