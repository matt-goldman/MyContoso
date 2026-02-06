using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Models;
using MyContoso.App.Services;
using Plugin.Maui.Lucide;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(AccreditationId), "id")]
public partial class AccreditationDetailViewModel(IApiClient apiClient) : ObservableObject
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

    // 🚨 Anti-pattern: UI presentation properties in ViewModel
    // These should be ValueConverters in the View layer
    private static readonly Color ValidPrimary = Color.FromArgb("#7C3AED");
    private static readonly Color ValidBackground = Color.FromArgb("#EDE9FE");
    private static readonly Color ExpiredPrimary = Color.FromArgb("#EF4444");
    private static readonly Color ExpiredBackground = Color.FromArgb("#FEE2E2");
    private static readonly Color PendingPrimary = Color.FromArgb("#14B8A6");
    private static readonly Color PendingBackground = Color.FromArgb("#CCFBF1");

    public Color StatusIconColor => Accreditation?.Status switch
    {
        "Valid" => ValidPrimary,
        "Expired" or "Overdue" => ExpiredPrimary,
        "Pending" => PendingPrimary,
        _ => Colors.Gray
    };

    public Color StatusBackgroundColor => Accreditation?.Status switch
    {
        "Valid" => ValidBackground,
        "Expired" or "Overdue" => ExpiredBackground,
        "Pending" => PendingBackground,
        _ => Color.FromArgb("#F3F4F6")
    };

    public Color StatusBadgeColor => StatusIconColor;
    public Color StatusBadgeTextColor => Colors.White;

    public string StatusIcon => Accreditation?.Status switch
    {
        "Valid" => Icons.CircleCheck,
        "Expired" or "Overdue" => Icons.CircleAlert,
        "Pending" => Icons.Hourglass,
        _ => Icons.CircleQuestionMark
    };

    partial void OnAccreditationIdChanged(int value)
    {
        _ = LoadAccreditationAsync();
    }

    partial void OnAccreditationChanged(Accreditation? value)
    {
        // Notify UI properties that depend on Accreditation
        OnPropertyChanged(nameof(StatusIconColor));
        OnPropertyChanged(nameof(StatusBackgroundColor));
        OnPropertyChanged(nameof(StatusBadgeColor));
        OnPropertyChanged(nameof(StatusBadgeTextColor));
        OnPropertyChanged(nameof(StatusIcon));
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
