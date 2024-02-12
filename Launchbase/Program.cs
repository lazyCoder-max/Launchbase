using MudBlazor.Services;
using Launchbase.Components;
using MudBlazor.Services;
using MetaMask.Blazor;
using Launchbase.Services;
using Fluxor;
using Launchbase.Store.TokenUseCase;
using MudBlazor;
namespace Launchbase
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents(options =>
                {
                    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(10);
                });
            if (builder.Environment.IsDevelopment())
            {
                Configuration = builder.Configuration.AddJsonFile("appsettings.Development.json").Build();
            }
            else
            {
                Configuration = builder.Configuration.AddJsonFile("appsettings.json").Build();
            }
            builder.Services.AddMudServices();
            builder.Services.AddMetaMaskBlazor();
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            builder.Services.AddFluxor(options =>
            {
                options.ScanAssemblies(typeof(Program).Assembly);
            });
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();

        }
    }
}