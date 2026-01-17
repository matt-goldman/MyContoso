using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class EmployeeDirectoryPage : ContentPage
{
    private readonly EmployeeDirectoryViewModel _viewModel;

    public EmployeeDirectoryPage(EmployeeDirectoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadEmployeesAsync();
    }
}
