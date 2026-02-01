using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class EmployeeListViewModel(ApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<Employee> Employees { get; } = [];

    [RelayCommand]
    private async Task LoadEmployeesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Employees.Clear();

            var employees = await apiClient.GetEmployeesAsync();
            foreach (var employee in employees.OrderBy(e => e.Name))
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
    private async Task RefreshAsync()
    {
        try
        {
            IsRefreshing = true;
            Employees.Clear();

            var employees = await apiClient.GetEmployeesAsync();
            foreach (var employee in employees.OrderBy(e => e.Name))
            {
                Employees.Add(employee);
            }
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private static Task ViewEmployeeAsync(Employee employee)
        => Shell.Current.GoToAsync($"employee?id={employee.EmployeeId}");
}
