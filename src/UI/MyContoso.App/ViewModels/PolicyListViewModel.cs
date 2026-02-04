using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Models;
using MyContoso.App.Services;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

/// <summary>
/// Grouping class for policies by category
/// </summary>
public class PolicyGroup(string category, IEnumerable<Policy> policies) : ObservableCollection<Policy>(policies)
{
    public string Category { get; } = category;
}

public partial class PolicyListViewModel(IApiClient apiClient) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isRefreshing;

    [ObservableProperty]
    private string searchText = string.Empty;

    public ObservableCollection<PolicyGroup> Policies { get; } = [];

    [RelayCommand]
    private async Task LoadPolicies()
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
    private async Task Refresh()
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
    private async Task ViewPolicy(Policy policy)
    {
        await Shell.Current.GoToAsync($"policy?id={policy.PolicyId}");
    }
}
