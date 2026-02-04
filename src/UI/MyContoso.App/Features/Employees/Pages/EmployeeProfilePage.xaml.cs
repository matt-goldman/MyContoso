
using MyContoso.App.Features.Employees.ViewModels;

namespace MyContoso.App.Features.Employees.Pages;

public partial class EmployeeProfilePage : ContentPage
{
    public EmployeeProfilePage(EmployeeProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
