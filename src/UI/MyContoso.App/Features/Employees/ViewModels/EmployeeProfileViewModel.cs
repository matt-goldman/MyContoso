using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Features.Employees.Services;
using Shared;

namespace MyContoso.App.Features.Employees.ViewModels;

[QueryProperty(nameof(EmployeeId), "id")]
public partial class EmployeeProfileViewModel(EmployeeService service) : ObservableObject
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
    
    [ObservableProperty]
    private bool isMyProfile = false;

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
            Employee = await service.GetEmployeeAsync(EmployeeId);

            foreach (var accreditation in Employee?.Accreditations??[])
            {
                if (accreditation.Status == "Valid") ActiveAccreditations++;

                if (accreditation.ExpiryDate < DateTime.Now.AddMonths(6)) ExpiringAccreditations++;
            }

            if (!IsMyProfile)
            {
                IsMyProfile = App.CurrentUser?.EmployeeId == EmployeeId;
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
}
