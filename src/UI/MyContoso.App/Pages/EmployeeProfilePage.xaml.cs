using MyContoso.App.ViewModels;

namespace MyContoso.App.Pages;

public partial class EmployeeProfilePage : ContentPage
{
    public EmployeeProfilePage(EmployeeProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
