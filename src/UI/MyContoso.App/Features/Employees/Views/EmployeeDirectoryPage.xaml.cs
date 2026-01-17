using MyContoso.App.Features.Employees.ViewModels;

namespace MyContoso.App.Features.Employees.Views;

public partial class EmployeeDirectoryPage : ContentPage
{
    private readonly EmployeeDirectoryViewModel _viewModel;

    public EmployeeDirectoryPage(EmployeeDirectoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadEmployeesCommand.ExecuteAsync(null);
    }
}
