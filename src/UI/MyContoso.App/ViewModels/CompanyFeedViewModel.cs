using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Models;
using MyContoso.App.Services;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class CompanyFeedViewModel(IApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<CompanyUpdateItemViewModel> Updates { get; } = [];

    [RelayCommand]
    public async Task LoadUpdatesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Updates.Clear();

            var updates = await apiClient.GetCompanyUpdatesAsync();
            foreach (var update in updates.OrderByDescending(u => u.PublishedDate))
            {
                Updates.Add(new CompanyUpdateItemViewModel(update));
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

            var updates = await apiClient.GetCompanyUpdatesAsync();
            foreach (var update in updates.OrderByDescending(u => u.PublishedDate))
            {
                Updates.Add(new CompanyUpdateItemViewModel(update));
            }
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task ViewUpdateAsync(CompanyUpdateItemViewModel item)
    {
        await Shell.Current.GoToAsync($"companyupdate?id={item.UpdateId}");
    }
}
