using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class EmployeeProfilePage : ContentPage
{
    public EmployeeProfilePage(EmployeeProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
