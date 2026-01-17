using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Features.Employees.Services;
using Shared.Models;

namespace MyContoso.App.Features.Employees.ViewModels;

[QueryProperty(nameof(EmployeeId), "employeeId")]
public partial class EmployeeProfileViewModel : ObservableObject
{
    private readonly EmployeeService _employeeService;

    [ObservableProperty]
    private int _employeeId;

    [ObservableProperty]
    private Employee? _employee;

    [ObservableProperty]
    private bool _isLoading;

    public EmployeeProfileViewModel(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    partial void OnEmployeeIdChanged(int value)
    {
        _ = LoadEmployeeAsync();
    }

    [RelayCommand]
    private async Task LoadEmployeeAsync()
    {
        if (IsLoading || EmployeeId == 0) return;

        try
        {
            IsLoading = true;
            Employee = await _employeeService.GetEmployeeByIdAsync(EmployeeId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
