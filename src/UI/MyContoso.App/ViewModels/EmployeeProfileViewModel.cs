using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(EmployeeId), "id")]
public partial class EmployeeProfileViewModel(ApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private int employeeId;

    [ObservableProperty]
    private Employee? employee;

    [ObservableProperty]
    private bool isLoading;

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
            Employee = await apiClient.GetEmployeeAsync(EmployeeId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
