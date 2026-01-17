using MyContoso.Shared.Models;
using System.Net.Http.Json;

namespace MyContoso.App.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CompanyUpdate>> GetCompanyUpdatesAsync()
    {
        try
        {
            var updates = await _httpClient.GetFromJsonAsync<List<CompanyUpdate>>("company-updates");
            return updates ?? new List<CompanyUpdate>();
        }
        catch
        {
            // Return empty list if API is not available
            return new List<CompanyUpdate>();
        }
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        try
        {
            var employees = await _httpClient.GetFromJsonAsync<List<Employee>>("employees");
            return employees ?? new List<Employee>();
        }
        catch
        {
            return new List<Employee>();
        }
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Employee>($"employees/{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<Policy>> GetPoliciesAsync()
    {
        try
        {
            var policies = await _httpClient.GetFromJsonAsync<List<Policy>>("policies");
            return policies ?? new List<Policy>();
        }
        catch
        {
            return new List<Policy>();
        }
    }

    public async Task<Policy?> GetPolicyAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Policy>($"policies/{id}");
        }
        catch
        {
            return null;
        }
    }
}
