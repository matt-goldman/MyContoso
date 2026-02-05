using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Features.Policies.Services;
using Shared;

namespace MyContoso.App.Features.Policies.ViewModels;

[QueryProperty(nameof(PolicyId), "id")]
public partial class PolicyDetailViewModel(PoliciesService service) : ObservableObject
{
    [ObservableProperty]
    private int policyId;

    [ObservableProperty]
    private Policy? policy;

    [ObservableProperty]
    private bool isLoading;

    partial void OnPolicyIdChanged(int value)
    {
        _ = LoadPoliciesAsync();
    }

    private async Task LoadPoliciesAsync()
    {
        if (PolicyId <= 0)
            return;

        try
        {
            IsLoading = true;
            Policy = await service.GetPolicyAsync(PolicyId);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
