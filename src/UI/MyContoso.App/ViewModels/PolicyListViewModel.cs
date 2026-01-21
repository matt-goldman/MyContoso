using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class PolicyListViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    public ObservableCollection<Policy> Policies { get; } = [];

    public PolicyListViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    public async Task LoadPoliciesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Policies.Clear();

            var policies = await _apiClient.GetPoliciesAsync();
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
    public async Task RefreshAsync()
    {
        try
        {
            IsRefreshing = true;
            Policies.Clear();

            var policies = await _apiClient.GetPoliciesAsync();
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
    public async Task ViewPolicyAsync(Policy policy)
    {
        await Shell.Current.GoToAsync($"policy?id={policy.PolicyId}");
    }
}
