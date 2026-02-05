using MyContoso.App.Features.Policies.Models;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.Features.Policies.Services;

public class PoliciesService(IApiClient apiClient)
{
    public async Task<IEnumerable<PolicyGroup>> GetAllPoliciesAsync()
    {
        var policies = await apiClient.GetPoliciesAsync();
        var grouped = policies
            .GroupBy(p => p.Category)
            .OrderBy(g => g.Key)
            .Select(g => new PolicyGroup(g.Key, g.OrderBy(p => p.Title)));
        
        return grouped;
    }
    
    public Task<Policy?> GetPolicyAsync(int id) 
        => apiClient.GetPolicyAsync(id);
}