using MyContoso.App.Features.Employees.Views;
using MyContoso.App.Features.Policies.Views;

namespace MyContoso.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register transient routes
            Routing.RegisterRoute(Routes.EmployeeProfile, typeof(EmployeeProfilePage));
            Routing.RegisterRoute(Routes.PolicyDetail, typeof(PoliciesPage)); // TODO: Create PolicyDetailPage
        }
    }
}
