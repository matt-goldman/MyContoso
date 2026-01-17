using MyContoso.App.Services;
using Shared.Models;

namespace MyContoso.App.Features.Policies.Services;

/// <summary>
/// Service for managing policy data.
/// </summary>
public class PolicyService
{
    private readonly ApiClient _apiClient;

    public PolicyService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Policy>> GetPoliciesAsync()
    {
        // TODO: Implement actual API call via ApiClient
        await Task.Delay(500);

        return new List<Policy>
        {
            new Policy
            {
                PolicyId = 1,
                Title = "Code of Conduct",
                Category = "Workplace Behavior",
                LastUpdated = DateTime.Now.AddMonths(-2),
                Content = "All employees are expected to maintain professional conduct at all times..."
            },
            new Policy
            {
                PolicyId = 2,
                Title = "Remote Work Policy",
                Category = "Work Arrangements",
                LastUpdated = DateTime.Now.AddMonths(-1),
                Content = "Employees may work remotely up to 3 days per week with manager approval..."
            },
            new Policy
            {
                PolicyId = 3,
                Title = "Data Privacy Policy",
                Category = "Security & Compliance",
                LastUpdated = DateTime.Now.AddDays(-15),
                Content = "All company and customer data must be handled in accordance with GDPR..."
            },
            new Policy
            {
                PolicyId = 4,
                Title = "Time Off Policy",
                Category = "Benefits",
                LastUpdated = DateTime.Now.AddMonths(-3),
                Content = "Employees receive 20 days of paid time off per year..."
            }
        };
    }

    public async Task<Policy?> GetPolicyByIdAsync(int policyId)
    {
        await Task.Delay(300);
        var policies = await GetPoliciesAsync();
        return policies.FirstOrDefault(p => p.PolicyId == policyId);
    }
}
