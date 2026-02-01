using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class PolicyListViewModel(ApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<Policy> Policies { get; } = [];

    [RelayCommand]
    private async Task LoadPoliciesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Policies.Clear();

            var policies = await apiClient.GetPoliciesAsync();
            foreach (var policy in policies.OrderBy(p => p.Category).ThenBy(p => p.Title))
            {
                Policies.Add(policy);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        try
        {
            IsRefreshing = true;
            Policies.Clear();

            var policies = await apiClient.GetPoliciesAsync();
            foreach (var policy in policies.OrderBy(p => p.Category).ThenBy(p => p.Title))
            {
                Policies.Add(policy);
            }
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private static Task ViewPolicyAsync(Policy policy)
        => Shell.Current.GoToAsync($"policy?id={policy.PolicyId}");
}
