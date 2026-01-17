using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using MyContoso.Shared.Models;

namespace MyContoso.App.ViewModels;

public partial class EmployeeProfileViewModel : ObservableObject, IQueryAttributable
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private Employee? _employee;

    public EmployeeProfileViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) && idObj is string idString && int.TryParse(idString, out var employeeId))
        {
            _ = LoadEmployeeAsync(employeeId).ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    // Log the exception (in a real app, you'd use proper logging)
                    System.Diagnostics.Debug.WriteLine($"Error loading employee: {task.Exception}");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

    private async Task LoadEmployeeAsync(int employeeId)
    {
        IsLoading = true;
        try
        {
            Employee = await _apiClient.GetEmployeeAsync(employeeId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
