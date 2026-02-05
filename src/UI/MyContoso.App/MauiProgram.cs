using CommunityToolkit.Maui;
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

            // Register API client
            builder.Services.AddSingleton<IApiClient, MockApiClient>();

            // Register ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<CompanyFeedViewModel>();
            builder.Services.AddSingleton<CompanyUpdateDetailViewModel>();
            builder.Services.AddSingleton<EmployeeListViewModel>();
            builder.Services.AddSingleton<EmployeeProfileViewModel>();
            builder.Services.AddSingleton<PolicyListViewModel>();
            builder.Services.AddSingleton<PolicyDetailViewModel>();
            builder.Services.AddSingleton<AccreditationListViewModel>();
            builder.Services.AddSingleton<AccreditationDetailViewModel>();

            // Register Views
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<CompanyFeedPage>();
            builder.Services.AddSingleton<CompanyUpdateDetailPage>();
            builder.Services.AddSingleton<EmployeeListPage>();
            builder.Services.AddSingleton<EmployeeProfilePage>();
            builder.Services.AddSingleton<PolicyListPage>();
            builder.Services.AddSingleton<PolicyDetailPage>();
            builder.Services.AddSingleton<AccreditationListPage>();
            builder.Services.AddSingleton<AccreditationDetailPage>();

            return builder.Build();
        }
    }
}
