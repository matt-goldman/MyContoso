using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class EmployeeListViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<Employee> Employees { get; } = [];

    public EmployeeListViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    public async Task LoadEmployeesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Employees.Clear();

            var employees = await _apiClient.GetEmployeesAsync();
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
    public async Task RefreshAsync()
    {
        try
        {
            IsRefreshing = true;
            Employees.Clear();

            var employees = await _apiClient.GetEmployeesAsync();
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
    public async Task ViewEmployeeAsync(Employee employee)
    {
        await Shell.Current.GoToAsync($"employee?id={employee.EmployeeId}");
    }
}
