using MyContoso.App.Pages;

namespace MyContoso.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for detail pages
            Routing.RegisterRoute("companyupdate", typeof(CompanyUpdateDetailPage));
            Routing.RegisterRoute("employee", typeof(EmployeeProfilePage));
            Routing.RegisterRoute("policy", typeof(PolicyDetailPage));
            Routing.RegisterRoute("accreditation", typeof(AccreditationDetailPage));
        }
    }
}
