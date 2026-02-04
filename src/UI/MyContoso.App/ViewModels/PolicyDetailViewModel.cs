using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Models;
using MyContoso.App.Services;

namespace MyContoso.App.ViewModels;

[QueryProperty(nameof(PolicyId), "id")]
public partial class PolicyDetailViewModel(IApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private int policyId;

    [ObservableProperty]
    private Policy? policy;

    [ObservableProperty]
    private bool isLoading;

    partial void OnPolicyIdChanged(int value)
    {
        _ = LoadPolicyAsync();
    }

    private async Task LoadPolicyAsync()
    {
        if (PolicyId <= 0)
            return;

        try
        {
            IsLoading = true;
            Policy = await apiClient.GetPolicyAsync(PolicyId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
