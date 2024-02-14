using Fluxor;
using Launchbase.Services.Web3.Dtos;

namespace Launchbase.Store.PoolUseCase
{
    [FeatureState]
    public record Contribution
    {
        public Contribution()
        {

        }
        public List<Contributor>? Contributors { get; set; } = new();
        public int? PoolId { get; set; }
        public string? CurrencyAddress { get; set; }
        public bool? IsCurrencyToken { get; set; }
        public string? ContributorAddress { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Decimals { get; set; }
        public ContributionStatus<TaskStatus> FetchStatus { get; set; } = new() { Status = TaskStatus.Ideal };
        public ContributionStatus<TaskStatus> CreateStatus { get; set; } = new() { Status = TaskStatus.Ideal };
    }

    
    public struct ContributionStatus<T>
    {

        public T Status { get; set; }
        public string Message { get; set; }
    }
}
