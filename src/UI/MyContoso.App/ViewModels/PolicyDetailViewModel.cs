using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using MyContoso.Shared.Models;

namespace MyContoso.App.ViewModels;

public partial class PolicyDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private Policy? _policy;

    public PolicyDetailViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) && idObj is string idString && int.TryParse(idString, out var policyId))
        {
            _ = LoadPolicyAsync(policyId);
        }
    }

    private async Task LoadPolicyAsync(int policyId)
    {
        IsLoading = true;
        try
        {
            Policy = await _apiClient.GetPolicyAsync(policyId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
