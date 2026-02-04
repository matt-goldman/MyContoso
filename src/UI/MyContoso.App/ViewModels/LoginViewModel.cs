using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;

namespace MyContoso.App.ViewModels;

public partial class LoginViewModel(EmployeeService employeeService) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    public INavigation? Navigation { get; set; }

    [RelayCommand]
    public async Task Login()
    {
        IsLoading = true;

        // simulate logging in - fetch employees from the API, pick one at random, set as current user
        var employees = await employeeService.GetEmployeesAsync();

        var employeeList = employees?.ToList();
        if (employeeList == null || employeeList.Count == 0)
        {
            IsLoading = false;
            return;
        }

        var random = new Random();
        var randomEmployee = employeeList[random.Next(employeeList.Count)];
        App.CurrentUser = randomEmployee;

        IsLoading = false;
        await Navigation?.PopModalAsync();
    }
}
