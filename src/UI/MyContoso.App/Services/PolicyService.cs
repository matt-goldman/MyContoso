using MyContoso.App.Models;
using ApiPolicy = MyContoso.App.ApiModels.Policy;

namespace MyContoso.App.Services;

public class PolicyService(IApiClient apiClient)
{
    public async Task<IEnumerable<Policy>> GetPoliciesAsync()
    {
        var apiPolicies = await apiClient.GetPoliciesAsync();
        return apiPolicies.Select(MapToModel);
    }

    public async Task<Policy?> GetPolicyAsync(int id)
    {
        var apiPolicy = await apiClient.GetPolicyAsync(id);
        return apiPolicy is null ? null : MapToModel(apiPolicy);
    }

    private static Policy MapToModel(ApiPolicy api)
    {
        return new Policy(
            api.PolicyId,
            api.Title,
            api.Category,
            api.LastUpdated,
            api.UpdatedBy,
            api.Description,
            api.Content
        );
    }
}
