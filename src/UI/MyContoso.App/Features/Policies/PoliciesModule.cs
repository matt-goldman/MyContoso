using MyContoso.App.Features.Policies.Pages;
using MyContoso.App.Features.Policies.Services;
using MyContoso.App.Features.Policies.ViewModels;

namespace MyContoso.App.Features.Policies;

public static class PoliciesModule
{
    public static MauiAppBuilder AddPoliciesModule(this MauiAppBuilder builder)
    {
        // Pages
        builder.Services.AddSingleton<PolicyListPage>();
        builder.Services.AddTransient<PolicyDetailPage>();
        
        // ViewModels
        builder.Services.AddSingleton<PolicyListViewModel>();
        builder.Services.AddTransient<PolicyDetailViewModel>();

        // Services
        builder.Services.AddSingleton<PoliciesService>();
        
        return builder;
    }
    
    public static void RegisterPoliciesRoutes()
    {
        Routing.RegisterRoute("Policies", typeof(PolicyDetailPage));
    }
}