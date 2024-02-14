using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launchbase.Services.Web3.Dtos
{
    public class PoolInfo
    {
        public TokenInfo Token { get; set; }
        public int? PoolId { get; set; }
        public string? PoolTitle { get; set; } = "";
        public string? AdminWallet { get; set; }
        public decimal? SoftCap { get; set; } = 0;
        public decimal? HardCap { get; set; } = 0;
        public decimal? MinimumBuy { get; set; } = 0;
        public decimal? MaximumBuy { get; set; } = 0;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? WebsiteLink { get; set; }
        public string? FacebookLink { get; set; } = "";
        public string? TwitterLink { get; set; } = "";
        public string? GithubLink { get; set; } = "";
        public string? TelegramLink { get; set; } = "";
        public string? InstagramLink { get; set; } = "";
        public string? DiscordLink { get; set; } = "";
        public string? RedditLink { get; set; } = "";
        public string? YoutubeLink { get; set; } = "";
        public string? Description { get; set; }
        public decimal TotalRaised { get; set; }
        public decimal RaisedPercentage { get; set; } = 0;
        public List<TokenInfo> Currencies { get; set; }
        public PoolState State { get; set; }

    }
    public enum PoolState
    {
        InUse = 0,
        Completed = 1,
        Cancelled = 2,
        NotStarted = 3
    }
}
