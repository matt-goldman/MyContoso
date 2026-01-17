using CommunityToolkit.Maui;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyContoso.App.Features.CompanyFeed.Services;
using MyContoso.App.Features.CompanyFeed.ViewModels;
using MyContoso.App.Features.CompanyFeed.Views;
using MyContoso.App.Features.Employees.Services;
using MyContoso.App.Features.Employees.ViewModels;
using MyContoso.App.Features.Employees.Views;
using MyContoso.App.Features.Policies.Services;
using MyContoso.App.Features.Policies.ViewModels;
using MyContoso.App.Features.Policies.Views;
using MyContoso.App.Services;

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

            // Register services
            builder.Services.AddSingleton<CompanyFeedService>();
            builder.Services.AddSingleton<EmployeeService>();
            builder.Services.AddSingleton<PolicyService>();

            // Register ViewModels
            builder.Services.AddSingleton<CompanyFeedViewModel>();
            builder.Services.AddTransient<EmployeeDirectoryViewModel>();
            builder.Services.AddTransient<EmployeeProfileViewModel>(); // Transient to avoid state leakage
            builder.Services.AddTransient<PoliciesViewModel>();

            // Register Views
            builder.Services.AddSingleton<CompanyFeedPage>();
            builder.Services.AddTransient<EmployeeDirectoryPage>();
            builder.Services.AddTransient<EmployeeProfilePage>(); // Transient to avoid state leakage
            builder.Services.AddTransient<PoliciesPage>();

            return builder.Build();
        }
    }
}
