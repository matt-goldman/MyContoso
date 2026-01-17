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
            // TODO: Add service defaults once MauiServiceDefaults build issues are resolved
            // Issue: IMauiInitializeService interface is not available in .NET 10 MAUI
            // builder.AddServiceDefaults();

            // configure HTTP client
            builder.Services.AddHttpClient<ApiClient>(client =>
            {
                // TODO: Use service discovery once Aspire integration is fixed
                client.BaseAddress = new Uri("http://localhost:5000");
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
