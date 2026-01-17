using MyContoso.App.Views;

namespace MyContoso.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Register routes for detail pages
            Routing.RegisterRoute("employee", typeof(EmployeeProfilePage));
            Routing.RegisterRoute("policy", typeof(PolicyDetailPage));
        }
    }
}
