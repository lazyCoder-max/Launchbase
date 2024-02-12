using Fluxor;
using Launchbase.Store.TokenUseCase;

namespace Launchbase.Store.PresaleUseCase
{
    [FeatureState]
    public record PresaleToken
    {
        public PresaleToken()
        {
            
        }
        public Token Token { get; set; }
        public decimal? SoftCap { get; set; } = 0;
        public decimal? HardCap { get; set; } = 0;
        public decimal? MinimumBuy { get; set; } = 0;
        public decimal? MaximumBuy { get; set; } = 0;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? WebsiteLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? GithubLink { get; set; }
        public string? TelegramLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? DiscordLink { get; set; }
        public string? RedditLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? Description { get; set; }
        public TokenStatus<TaskStatus> FetchStatus { get; set; } = new() { Status = TaskStatus.Ideal };
        public TokenStatus<TaskStatus> CreateStatus { get; set; } = new() { Status = TaskStatus.Ideal };
        public TokenStatus<TaskStatus> ContributeStatus { get; set; } = new() { Status = TaskStatus.Ideal };
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
