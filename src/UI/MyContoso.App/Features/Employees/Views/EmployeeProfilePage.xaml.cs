using MyContoso.App.Features.Employees.ViewModels;

namespace MyContoso.App.Features.Employees.Views;

public partial class EmployeeProfilePage : ContentPage
{
    public EmployeeProfilePage(EmployeeProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
