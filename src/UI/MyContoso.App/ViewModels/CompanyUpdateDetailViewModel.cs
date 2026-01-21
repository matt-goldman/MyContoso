using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(UpdateId), "id")]
public partial class CompanyUpdateDetailViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private int updateId;

    [ObservableProperty]
    private CompanyUpdate? update;

    [ObservableProperty]
    private bool isLoading;

    public CompanyUpdateDetailViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    partial void OnUpdateIdChanged(int value)
    {
        _ = LoadUpdateAsync();
    }

    private async Task LoadUpdateAsync()
    {
        if (UpdateId <= 0)
            return;

        try
        {
            IsLoading = true;
            Update = await _apiClient.GetCompanyUpdateAsync(UpdateId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
