using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

/// <summary>
/// Grouping class for policies by category
/// </summary>
public class PolicyGroup(string category, IEnumerable<Policy> policies) : ObservableCollection<Policy>(policies)
{
    public string Category { get; } = category;
}

public partial class PolicyListViewModel(ApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string searchText = string.Empty;

    public ObservableCollection<PolicyGroup> Policies { get; } = [];

    [RelayCommand]
    public async Task LoadPoliciesAsync()
    {
        if (IsLoading)
            return;

        try
        {
            IsLoading = true;
            Policies.Clear();

            var policies = await apiClient.GetPoliciesAsync();
            var grouped = policies
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .Select(g => new PolicyGroup(g.Key, g.OrderBy(p => p.Title)));

            foreach (var group in grouped)
            {
                Policies.Add(group);
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

            var policies = await apiClient.GetPoliciesAsync();
            var grouped = policies
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .Select(g => new PolicyGroup(g.Key, g.OrderBy(p => p.Title)));

            foreach (var group in grouped)
            {
                Policies.Add(group);
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
