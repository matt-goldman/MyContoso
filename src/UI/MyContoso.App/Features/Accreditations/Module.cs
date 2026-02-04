using MyContoso.App.Features.Accreditations.Pages;
using MyContoso.App.Features.Accreditations.ViewModels;

namespace MyContoso.App.Features.Accreditations;

public static class AccreditationsModule
{
    public static MauiAppBuilder AddAccreditationsModule(this MauiAppBuilder builder)
    {
        // Pages
        builder.Services.AddSingleton<AccreditationListPage>();
        builder.Services.AddSingleton<AccreditationDetailPage>();
        
        // ViewModels
        builder.Services.AddSingleton<AccreditationListViewModel>();
        builder.Services.AddSingleton<AccreditationDetailViewModel>();

        // Services
        
        
        return builder;
    }

    public static void RegisterAccreditationRoutes()
    {
        Routing.RegisterRoute("accreditation", typeof(AccreditationDetailPage));
    }
}