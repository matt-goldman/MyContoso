using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

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

        if (BindingContext is EmployeeListViewModel vm && vm.Employees.Count == 0)
        {
            await vm.LoadEmployeesCommand.ExecuteAsync(null);
        }
    }
}
