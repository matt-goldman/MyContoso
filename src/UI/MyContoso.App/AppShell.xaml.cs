using MyContoso.App.Pages;
using MyContoso.App.Features.Accreditations;

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
            Routing.RegisterRoute("employee", typeof(EmployeeProfilePage));
            Routing.RegisterRoute("policy", typeof(PolicyDetailPage));
            
            AccreditationsModule.RegisterAccreditationRoutes();
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
