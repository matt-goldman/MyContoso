using CommunityToolkit.Maui;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyContoso.App.Services;
using MyContoso.App.Views;
using MyContoso.App.ViewModels;

namespace MyContoso.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("icofont.ttf", "IcoFont");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Add service defaults
            builder.AddServiceDefaults();

            // configure HTTP client with service discovery
            builder.Services.AddHttpClient<ApiClient>(client =>
            {
                client.BaseAddress = new Uri("https+http://webapi");
            });

            // Register ViewModels
            builder.Services.AddTransient<CompanyFeedViewModel>();
            builder.Services.AddTransient<EmployeeDirectoryViewModel>();
            builder.Services.AddTransient<EmployeeProfileViewModel>();
            builder.Services.AddTransient<PoliciesViewModel>();
            builder.Services.AddTransient<PolicyDetailViewModel>();

            // Register Pages
            builder.Services.AddTransient<CompanyFeedPage>();
            builder.Services.AddTransient<EmployeeDirectoryPage>();
            builder.Services.AddTransient<EmployeeProfilePage>();
            builder.Services.AddTransient<PoliciesPage>();
            builder.Services.AddTransient<PolicyDetailPage>();

            return builder.Build();
        }
    }
}
