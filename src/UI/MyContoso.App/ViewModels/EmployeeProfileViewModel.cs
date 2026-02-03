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
    
    [ObservableProperty]
    private int activeAccreditations;
    
    [ObservableProperty]
    private int expiringAccreditations;

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

            foreach (var accreditation in Employee?.Accreditations??[])
            {
                if (accreditation.Status == "Valid") ActiveAccreditations++;

                if (accreditation.ExpiryDate < DateTime.Now.AddMonths(6)) ExpiringAccreditations++;
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
}
