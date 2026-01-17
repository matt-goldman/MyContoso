using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using MyContoso.Shared.Models;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class EmployeeDirectoryViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private ObservableCollection<Employee> _employees = new();

    public EmployeeDirectoryViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task LoadEmployeesAsync()
    {
        IsLoading = true;
        try
        {
            var employees = await _apiClient.GetEmployeesAsync();
            Employees.Clear();
            foreach (var employee in employees)
            {
                Employees.Add(employee);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToEmployeeAsync(int employeeId)
    {
        await Shell.Current.GoToAsync($"employee/{employeeId}");
    }
}
