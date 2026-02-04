using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Models;
using MyContoso.App.Services;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(UpdateId), "id")]
public partial class CompanyUpdateDetailViewModel(UpdatesService updatesService) : ObservableObject
{
    [ObservableProperty]
    private int updateId;

    [ObservableProperty]
    private CompanyUpdate? update;

    [ObservableProperty]
    private bool isLoading;

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
            Update = await updatesService.GetUpdateAsync(UpdateId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
