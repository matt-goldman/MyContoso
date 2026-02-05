using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Features.Policies.Models;
using MyContoso.App.Features.Policies.Services;
using Shared;

namespace MyContoso.App.Features.Policies.ViewModels;


public partial class PolicyListViewModel(PoliciesService policyService) : ObservableObject
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

            var grouped = await policyService.GetAllPoliciesAsync();
            
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

            var grouped = await policyService.GetAllPoliciesAsync();

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