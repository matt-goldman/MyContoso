using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Features.Policies.Services;
using Shared.Models;

namespace MyContoso.App.Features.Policies.ViewModels;

public partial class PoliciesViewModel : ObservableObject
{
    private readonly PolicyService _policyService;

    [ObservableProperty]
    private List<Policy> _policies = new();

    [ObservableProperty]
    private bool _isLoading;

    public PoliciesViewModel(PolicyService policyService)
    {
        _policyService = policyService;
    }

    [RelayCommand]
    private async Task LoadPoliciesAsync()
    {
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            Policies = await _policyService.GetPoliciesAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToPolicyDetailAsync(Policy policy)
    {
        await Shell.Current.GoToAsync($"{Routes.PolicyDetail}?policyId={policy.PolicyId}");
    }
}
