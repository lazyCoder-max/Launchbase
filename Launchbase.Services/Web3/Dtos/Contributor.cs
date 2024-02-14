namespace Launchbase.Services.Web3.Dtos
{
    public class Contributor
    {
        public int? PoolId { get; set; }
        public string? CurrencyUsed { get; set; }
        public string? ContributorAddress { get; set; }
        public DateTime? TimeStamp { get; set; }
        public decimal? Amount { get; set; }
    }
}
