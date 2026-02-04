using MyContoso.App.Features.Employees.Pages;
using MyContoso.App.Features.Employees.Services;
using MyContoso.App.Features.Employees.ViewModels;

namespace MyContoso.App.Features.Employees;

public static class EmployeesModule
{
    public static MauiAppBuilder AddEmployeesModule(this MauiAppBuilder builder)
    {
        // Pages
        builder.Services.AddSingleton<EmployeeListPage>();
        builder.Services.AddTransient<EmployeeProfilePage>();
        
        // ViewModels
        builder.Services.AddSingleton<EmployeeListViewModel>();
        builder.Services.AddTransient<EmployeeProfileViewModel>();

        // Services
        builder.Services.AddSingleton<EmployeeService>();
        
        return builder;
    }
    
    public static void RegisterEmployeeRoutes()
    {
        Routing.RegisterRoute("employeeProfile", typeof(EmployeeProfilePage));
    }
}