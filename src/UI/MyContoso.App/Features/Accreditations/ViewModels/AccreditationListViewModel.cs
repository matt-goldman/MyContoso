using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.Features.Accreditations.ViewModels;

public partial class AccreditationListViewModel(ApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private int activeCount;

    [ObservableProperty]
    private int expiringSoonCount;

    [ObservableProperty]
    private int pendingCount;

    public ObservableCollection<Accreditation> Accreditations { get; } = [];

    [RelayCommand]
    private async Task LoadAccreditationsAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Accreditations.Clear();

            var accreditations = await apiClient.GetAccreditationsAsync();
            var accreditationList = accreditations.ToList();

            // Update counts
            ActiveCount = accreditationList.Count(a => a.Status == "Valid");
            ExpiringSoonCount = accreditationList.Count(a =>
                a is { Status: "Valid", ExpiryDate: not null } &&
                (a.ExpiryDate.Value - DateTime.Now).TotalDays <= 60);
            PendingCount = accreditationList.Count(a => a.Status == "Pending");

            foreach (var accreditation in accreditationList.OrderBy(a => a.Name))
            {
                Accreditations.Add(accreditation);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        try
        {
            IsRefreshing = true;
            Accreditations.Clear();

            var accreditations = await apiClient.GetAccreditationsAsync();
            var accreditationList = accreditations.ToList();

            // Update counts
            ActiveCount = accreditationList.Count(a => a.Status == "Valid");
            ExpiringSoonCount = accreditationList.Count(a =>
                a is { Status: "Valid", ExpiryDate: not null } &&
                (a.ExpiryDate.Value - DateTime.Now).TotalDays <= 60);
            PendingCount = accreditationList.Count(a => a.Status == "Pending");

            foreach (var accreditation in accreditationList.OrderBy(a => a.Name))
            {
                Accreditations.Add(accreditation);
            }
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private static Task ViewAccreditationAsync(Accreditation accreditation)
        => Shell.Current.GoToAsync($"accreditation?id={accreditation.AccreditationId}");
}
