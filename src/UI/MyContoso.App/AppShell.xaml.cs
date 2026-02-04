using MyContoso.App.Pages;
using MyContoso.App.Features.Accreditations;
using MyContoso.App.Features.Employees;

namespace MyContoso.App
{
    public partial class AppShell : Shell
    {
        private readonly LoginPage loginPage;

        public AppShell(LoginPage loginPage)
        {
            this.loginPage = loginPage;
            InitializeComponent();

            // Register routes for detail pages
            Routing.RegisterRoute("companyupdate", typeof(CompanyUpdateDetailPage));
            Routing.RegisterRoute("policy", typeof(PolicyDetailPage));
            
            AccreditationsModule.RegisterAccreditationRoutes();
            EmployeesModule.RegisterEmployeeRoutes();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        
            // If no user is logged in, show the login page
            if (App.CurrentUser == null)
            {
                await Navigation.PushModalAsync(loginPage);
            }
        }
    }
}
