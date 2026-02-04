using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Models;
using MyContoso.App.Services;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class CompanyFeedViewModel(UpdatesService updatesService) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<CompanyUpdate> Updates { get; } = [];

    [RelayCommand]
    public async Task LoadUpdatesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Updates.Clear();

            var updates = await updatesService.GetUpdatesAsync();
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

            var updates = await updatesService.GetUpdatesAsync();
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
