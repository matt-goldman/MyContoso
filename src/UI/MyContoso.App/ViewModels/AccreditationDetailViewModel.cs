using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(AccreditationId), "id")]
public partial class AccreditationDetailViewModel(ApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private Accreditation? accreditation;

    [ObservableProperty]
    private int accreditationId;

    [ObservableProperty]
    private string expiryComment = string.Empty;

    [ObservableProperty]
    private bool isExpiringSoon;
    
    [ObservableProperty]
    private bool isExpired;
    
    [ObservableProperty]
    private bool isShareable;
    
    partial void OnAccreditationIdChanged(int value)
    {
        _ = LoadAccreditationAsync();
    }

    [RelayCommand]
    private async Task LoadAccreditationAsync()
    {
        if (IsLoading || AccreditationId <= 0)
            return;

        try
        {
            IsLoading = true;
            Accreditation = await apiClient.GetAccreditationAsync(AccreditationId);
            
            IsShareable = Accreditation?.Status is not "Expired" and not "Overdue";

            if (Accreditation?.ExpiryDate.HasValue == true)
            {
                var daysUntilExpiry = (Accreditation.ExpiryDate.Value - DateTime.Now).Days;
                IsExpiringSoon = daysUntilExpiry <= 60 || Accreditation.Status is "Expired" or "Overdue";
                
                IsExpired = daysUntilExpiry < 0 || Accreditation.Status is "Expired" or "Overdue";

                ExpiryComment = daysUntilExpiry switch
                {
                    < 0 => $"Expired {Math.Abs(daysUntilExpiry)} days ago",
                    0 => "Expires today",
                    1 => "Expires tomorrow",
                    _ => $"Expires in {daysUntilExpiry} days"
                };
            }
            else
            {
                ExpiryComment = "No expiry date";
                IsExpiringSoon = false;
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private static  Task GoBackAsync()
        => Shell.Current.GoToAsync("..");
    
}
