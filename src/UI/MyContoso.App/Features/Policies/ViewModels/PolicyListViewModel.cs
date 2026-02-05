using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.Features.Policies.ViewModels;

/// <summary>
/// Grouping class for Policy by category
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
    private async Task LoadPolicy()
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
        await Shell.Current.GoToAsync($"PolicyId={policy.PolicyId}");
    }
}
