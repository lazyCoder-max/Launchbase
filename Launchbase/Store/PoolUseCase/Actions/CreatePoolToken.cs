using Fluxor;
using MetaMask.Blazor;
using Microsoft.JSInterop;
using Launchbase.Services.Interfaces;
using Launchbase.Services;

namespace Launchbase.Store.PoolUseCase.Actions
{
    public class CreatePoolToken
    {
        public record Action(IJSRuntime JSRuntime = null, IMetaMaskService metamask = null, PoolToken pool = null)
        {
            [ReducerMethod]
            public static PoolToken Reducer(PoolToken state, Action action)
            {
                return state with
                {
                    Token = action.pool.Token,
                    SoftCap = action.pool.SoftCap,
                    HardCap = action.pool.HardCap,
                    MinimumBuy = action.pool.MinimumBuy,
                    MaximumBuy = action.pool.MaximumBuy,
                    StartTime = action.pool.StartTime,
                    EndTime = action.pool.EndTime,
                    LogoUrl = action.pool.LogoUrl,
                    BannerUrl = action.pool.BannerUrl,
                    WebsiteLink = action.pool.WebsiteLink,
                    FacebookLink = action.pool.FacebookLink,
                    TwitterLink = action.pool.TwitterLink,
                    GithubLink = action.pool.GithubLink,
                    TelegramLink = action.pool.TelegramLink,
                    InstagramLink = action.pool.InstagramLink,
                    DiscordLink = action.pool.DiscordLink,
                    RedditLink = action.pool.RedditLink,
                    YoutubeLink = action.pool.YoutubeLink,
                    Description = action.pool.Description,
                    CreateStatus = new PoolStatus<TaskStatus>()
                    {
                        Status = TaskStatus.Processing
                    },
                };
            }
        }
        private record Effects()
        {
            [EffectMethod]
            public async Task HandlerAsync(Action action, IDispatcher dispatcher)
            {
                try
                {
                    await Task.Yield();
                    if (action.pool.Token.Address is null)
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Set Token Address First!", Status = TaskStatus.Failed }, action.pool));
                        return;
                    }
                    var address = await action.metamask.GetSelectedAddress();
                    var spenderAddress = Program.Configuration.GetRequiredSection("ContractAddress").GetValue<string>("Address");
                    ILaunchBaseServices services = new LaunchBaseServices(action.JSRuntime, spenderAddress, address);
                    
                    string[] otherInfo = {
                                        $"{action.pool.LogoUrl};{action.pool.BannerUrl}",
                                        $"{action.pool.Description}",
                                        $"{action.pool.WebsiteLink};" +
                                        $"{action.pool.FacebookLink};" +
                                        $"{action.pool.TwitterLink};" +
                                        $"{action.pool.GithubLink};" +
                                        $"{action.pool.TelegramLink};" +
                                        $"{action.pool.InstagramLink};" +
                                        $"{action.pool.DiscordLink};" +
                                        $"{action.pool.RedditLink};" +
                                        $"{action.pool.YoutubeLink};" +
                                        $"{action.pool.PoolTitle}" 
                                         };
                    string[] tokens = action.pool.Token.Currencies.Select(x=>x.Address).ToArray();
                    bool[] isTokens = action.pool.Token.Currencies.Select(x => x.IsToken).ToArray();
                    decimal[] rates = action.pool.Token.Currencies.Select(x=>x.Rate).ToArray();
                    var isApproved = await services.Pool.CreatePoolAsync(action.pool.Token.Address,address,((DateTimeOffset)action.pool.StartTime).ToUnixTimeSeconds(), ((DateTimeOffset)action.pool.EndTime).ToUnixTimeSeconds(), action.pool.SoftCap.Value, action.pool.HardCap.Value, action.pool.MinimumBuy.Value, action.pool.MaximumBuy.Value, otherInfo, tokens, rates, isTokens);
                    if (isApproved.Status)
                    {

                        dispatcher.Dispatch(new ResultAction(new()
                        {
                            Message = isApproved.Status == true ? "Pool Created!" : "Transaction Failed!",
                            Status = isApproved.Status == true ? TaskStatus.Created : TaskStatus.Failed,
                        }, 
                            action.pool
                        ));
                    }
                    else
                    {
                        dispatcher.Dispatch(new ResultAction(new() { Message = "Pool Creation Failed!", Status = TaskStatus.Failed }, action.pool));
                    }
                }
                catch (Exception e)
                {

                    dispatcher.Dispatch(new ResultAction(new()
                    {
                        Message = e.Message,
                        Status = TaskStatus.Failed
                    }, action.pool));
                }
            }
        }
        public record ResultAction(PoolStatus<TaskStatus> PoolStatus = new(), PoolToken? Pool = null)
        {
            [ReducerMethod]
            public static PoolToken Reducer(PoolToken state, ResultAction action)
            {
                return state with
                {
                    Token = action.Pool.Token,
                    SoftCap = action.Pool.SoftCap,
                    HardCap = action.Pool.HardCap,
                    MinimumBuy = action.Pool.MinimumBuy,
                    MaximumBuy = action.Pool.MaximumBuy,
                    StartTime = action.Pool.StartTime,
                    EndTime = action.Pool.EndTime,
                    LogoUrl = action.Pool.LogoUrl,
                    BannerUrl = action.Pool.BannerUrl,
                    WebsiteLink = action.Pool.WebsiteLink,
                    FacebookLink = action.Pool.FacebookLink,
                    TwitterLink = action.Pool.TwitterLink,
                    GithubLink = action.Pool.GithubLink,
                    TelegramLink = action.Pool.TelegramLink,
                    InstagramLink = action.Pool.InstagramLink,
                    DiscordLink = action.Pool.DiscordLink,
                    RedditLink = action.Pool.RedditLink,
                    YoutubeLink = action.Pool.YoutubeLink,
                    Description = action.Pool.Description,
                    CreateStatus = action.PoolStatus,
                };  
            }
        }
    }
}
