using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Features.Employees.Services;
using Shared.Models;

namespace MyContoso.App.Features.Employees.ViewModels;

public partial class EmployeeDirectoryViewModel : ObservableObject
{
    private readonly EmployeeService _employeeService;

    [ObservableProperty]
    private List<Employee> _employees = new();

    [ObservableProperty]
    private bool _isLoading;

    public EmployeeDirectoryViewModel(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [RelayCommand]
    private async Task LoadEmployeesAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            Employees = await _employeeService.GetEmployeesAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToProfileAsync(Employee employee)
    {
        await Shell.Current.GoToAsync($"{Routes.EmployeeProfile}?employeeId={employee.EmployeeId}");
    }
}
