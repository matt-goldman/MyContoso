using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class CompanyFeedViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<CompanyUpdate> Updates { get; } = [];

    public CompanyFeedViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    public async Task LoadUpdatesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Updates.Clear();

            var updates = await _apiClient.GetCompanyUpdatesAsync();
            foreach (var update in updates.OrderByDescending(u => u.PublishedDate))
            {
                Updates.Add(update);
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
            Updates.Clear();

            var updates = await _apiClient.GetCompanyUpdatesAsync();
            foreach (var update in updates.OrderByDescending(u => u.PublishedDate))
            {
                Updates.Add(update);
            }
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task ViewUpdateAsync(CompanyUpdate update)
    {
        await Shell.Current.GoToAsync($"companyupdate?id={update.UpdateId}");
    }
}
