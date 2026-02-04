using MyContoso.App.Features.Employees.ViewModels;

namespace MyContoso.App.Features.Employees.Pages;

public partial class EmployeeListPage : ContentPage
{
    public EmployeeListPage(EmployeeListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is EmployeeListViewModel { Employees.Count: 0 } vm)
        {
            await vm.LoadEmployeesCommand.ExecuteAsync(null);
        }
    }
}
