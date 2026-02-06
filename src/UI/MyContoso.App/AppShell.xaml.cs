using MyContoso.App.Controls;
using MyContoso.App.Pages;
using MyContoso.App.Features.Accreditations;
using MyContoso.App.Features.Employees;
using MyContoso.App.Features.Policies;
using MyContoso.App.Features.Updates;
using MyContoso.App.Services;
using MyContoso.App.ViewModels;

namespace MyContoso.App
{
    public partial class AppShell : Shell
    {
        private readonly IAuthenticationService authService;
        private readonly LoginPage loginPage;

        public AppShell(
            IAuthenticationService authService,
            LoginPage loginPage,
            TitleViewViewModel titleViewViewModel)
        {
            InitializeComponent();
            
            this.authService = authService;
            this.loginPage = loginPage;
            
            titleView.BindingContext = titleViewViewModel;

            // Register routes for detail pages
            AccreditationsModule.RegisterAccreditationRoutes();
            EmployeesModule.RegisterEmployeeRoutes();
            PoliciesModule.RegisterPoliciesRoutes();
            UpdatesModule.RegisterUpdatesRoutes();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        
            // If no user is logged in, show the login page
            if (authService.CurrentUser == null)
            {
                await Navigation.PushModalAsync(loginPage);
            }
        }
    }
}
