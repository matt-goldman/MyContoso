using System.Net.Http.Json;
using Shared;

namespace MyContoso.App.Services;

internal class ApiClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<CompanyUpdate>> GetCompanyUpdatesAsync()
    {
        var updates = await _httpClient.GetFromJsonAsync<IEnumerable<CompanyUpdate>>("/company-updates");
        return updates ?? [];
    }

    public async Task<CompanyUpdate?> GetCompanyUpdateAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CompanyUpdate>($"/company-updates/{id}");
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var employees = await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>("/employees");
        return employees ?? [];
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Employee>($"/employees/{id}");
    }

    public async Task<IEnumerable<Policy>> GetPoliciesAsync()
    {
        var policies = await _httpClient.GetFromJsonAsync<IEnumerable<Policy>>("/policies");
        return policies ?? [];
    }

    public async Task<Policy?> GetPolicyAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Policy>($"/policies/{id}");
    }
}
