using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Features.CompanyFeed.Services;
using Shared.Models;

namespace MyContoso.App.Features.CompanyFeed.ViewModels;

public partial class CompanyFeedViewModel : ObservableObject
{
    private readonly CompanyFeedService _feedService;

    [ObservableProperty]
    private List<CompanyUpdate> _updates = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isRefreshing;

    public CompanyFeedViewModel(CompanyFeedService feedService)
    {
        _feedService = feedService;
    }

    [RelayCommand]
    private async Task LoadUpdatesAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            Updates = await _feedService.GetCompanyUpdatesAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        if (IsRefreshing) return;

        try
        {
            IsRefreshing = true;
            Updates = await _feedService.GetCompanyUpdatesAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}
