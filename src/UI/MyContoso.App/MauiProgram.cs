using CommunityToolkit.Maui;
//using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                    fonts.AddFont("icofont.ttf", "IcoFont");
                })
                .UseMauiCommunityToolkit()
                .UseLucide();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Add service defaults
            //builder.AddServiceDefaults();

            // configure HTTP client with service discovery
            builder.Services.AddHttpClient<ApiClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5506");//"https+http://webapi");
            });

            // Register ViewModels
            builder.Services.AddSingleton<CompanyFeedViewModel>();
            builder.Services.AddTransient<CompanyUpdateDetailViewModel>();
            builder.Services.AddTransient<EmployeeListViewModel>();
            builder.Services.AddTransient<EmployeeProfileViewModel>();
            builder.Services.AddTransient<PolicyListViewModel>();
            builder.Services.AddTransient<PolicyDetailViewModel>();
            builder.Services.AddTransient<AccreditationListViewModel>();
            builder.Services.AddTransient<AccreditationDetailViewModel>();

            // Register Views
            builder.Services.AddSingleton<CompanyFeedPage>();
            builder.Services.AddTransient<CompanyUpdateDetailPage>();
            builder.Services.AddTransient<EmployeeListPage>();
            builder.Services.AddTransient<EmployeeProfilePage>();
            builder.Services.AddTransient<PolicyListPage>();
            builder.Services.AddTransient<PolicyDetailPage>();
            builder.Services.AddTransient<AccreditationListPage>();
            builder.Services.AddTransient<AccreditationDetailPage>();

            return builder.Build();
        }
    }
}
