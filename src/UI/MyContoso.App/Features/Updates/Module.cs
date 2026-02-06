using MyContoso.App.Features.Updates.Services;
using MyContoso.App.Pages;
using MyContoso.App.ViewModels;

namespace MyContoso.App.Features.Updates;

public static class UpdatesModule
{
    public static MauiAppBuilder AddUpdatesModule(this MauiAppBuilder builder)
    {
        // Pages
        builder.Services.AddSingleton<CompanyFeedPage>();
        builder.Services.AddTransient<CompanyUpdateDetailPage>();
        
        // ViewModels
        builder.Services.AddSingleton<CompanyFeedViewModel>();
        builder.Services.AddTransient<CompanyUpdateDetailViewModel>();

        // Services
        builder.Services.AddSingleton<UpdatesService>();
        
        return builder;
    }
    
    public static void RegisterUpdatesRoutes()
    {
        Routing.RegisterRoute("companyupdate", typeof(CompanyUpdateDetailPage));
    }
}