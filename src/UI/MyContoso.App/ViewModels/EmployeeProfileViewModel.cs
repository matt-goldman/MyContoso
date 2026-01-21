using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(EmployeeId), "id")]
public partial class EmployeeProfileViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private int employeeId;

    [ObservableProperty]
    private Employee? employee;

    [ObservableProperty]
    private bool isLoading;

    public EmployeeProfileViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    partial void OnEmployeeIdChanged(int value)
    {
        _ = LoadEmployeeAsync();
    }

    private async Task LoadEmployeeAsync()
    {
        if (EmployeeId <= 0)
            return;

        try
        {
            IsLoading = true;
            Employee = await _apiClient.GetEmployeeAsync(EmployeeId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
