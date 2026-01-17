using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;
using MyContoso.Shared.Models;
using System.Collections.ObjectModel;

namespace MyContoso.App.ViewModels;

public partial class PoliciesViewModel : ObservableObject
{
    private readonly ApiClient _apiClient;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private ObservableCollection<Policy> _policies = new();

    public PoliciesViewModel(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task LoadPoliciesAsync()
    {
        IsLoading = true;
        try
        {
            var policies = await _apiClient.GetPoliciesAsync();
            Policies.Clear();
            foreach (var policy in policies)
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
    private async Task NavigateToPolicyAsync(int policyId)
    {
        await Shell.Current.GoToAsync($"policy/{policyId}");
    }
}
