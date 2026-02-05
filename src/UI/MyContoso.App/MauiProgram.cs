using CommunityToolkit.Maui;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyContoso.App.Features.Accreditations;
using MyContoso.App.Features.Employees;
using MyContoso.App.Features.Policies;
using MyContoso.App.Services;
using MyContoso.App.ViewModels;
using MyContoso.App.Pages;
using Plugin.Maui.Lucide;

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
                })
                .UseMauiCommunityToolkit()
                .UseLucide();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Add service defaults
            //builder.AddServiceDefaults();

            // configure HTTP client with service discovery
            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5506");//"https+http://webapi");
            });

            builder
                .AddAccreditationsModule()
                .AddEmployeesModule()
                .AddPoliciesModule();

            // Register ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<CompanyFeedViewModel>();
            builder.Services.AddSingleton<CompanyUpdateDetailViewModel>();
            

            // Register Views
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<CompanyFeedPage>();
            builder.Services.AddSingleton<CompanyUpdateDetailPage>();
            

            return builder.Build();
        }
    }
}
