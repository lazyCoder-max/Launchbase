using Fluxor;

namespace Launchbase.Store.TokenUseCase
{
    [FeatureState]
    public record Token
    {
        public Token()
        {
            
        }
        public string? Address { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public decimal? TotalSupply { get; set; }
        public int? Decimals { get; set; }
        public List<Currency>? Currencies { get; set; }
        public TokenStatus<TaskStatus> FetchStatus { get; set; } = new() { Status = TaskStatus.Ideal };
        public TokenStatus<TaskStatus> ApproveStatus { get; set; } = new() { Status = TaskStatus.Ideal };
    }
    public class Currency
    {
        public bool IsPrimary { get; set; }
        public string ChainId { get; set; }
        public string ChainName { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }
        public bool IsSelected { get; set; }
    }
    public struct TokenStatus<T>
    {

        public T Status { get; set; }
        public string Message { get; set; }
    }
    public enum TaskStatus
    {
        Created = 0,
        Processing = 1,
        Ideal = 2,
        Failed = 3
    }
}
