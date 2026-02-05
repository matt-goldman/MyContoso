using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Features.Employees.Services;
using Shared;

namespace MyContoso.App.Features.Employees.ViewModels;

public partial class EmployeeListViewModel(EmployeeService service) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string searchText = string.Empty;

    public ObservableCollection<Employee> Employees { get; } = [];

    [RelayCommand]
    public async Task LoadEmployeesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Employees.Clear();

            var employees = await service.GetEmployeesAsync();
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

            var employees = await service.GetEmployeesAsync();
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
