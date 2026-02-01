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
    private string daysUntilExpiry = string.Empty;

    [ObservableProperty]
    private bool isExpiringSoon;

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

            if (Accreditation?.ExpiryDate.HasValue == true)
            {
                var days = (Accreditation.ExpiryDate.Value - DateTime.Now).Days;
                IsExpiringSoon = days <= 60 && Accreditation.Status == "Valid";

                if (days < 0)
                {
                    DaysUntilExpiry = $"Expired {Math.Abs(days)} days ago";
                }
                else if (days == 0)
                {
                    DaysUntilExpiry = "Expires today";
                }
                else if (days == 1)
                {
                    DaysUntilExpiry = "Expires tomorrow";
                }
                else
                {
                    DaysUntilExpiry = $"Expires in {days} days";
                }
            }
            else
            {
                DaysUntilExpiry = "No expiry date";
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
