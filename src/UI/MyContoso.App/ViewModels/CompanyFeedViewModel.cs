using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using MyContoso.Shared.Models;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class CompanyFeedViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private ObservableCollection<CompanyUpdate> _updates = new();

    public CompanyFeedViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task LoadUpdatesAsync()
    {
        IsLoading = true;
        try
        {
            var updates = await _apiClient.GetCompanyUpdatesAsync();
            Updates.Clear();
            foreach (var update in updates)
            {
                Updates.Add(update);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
}
