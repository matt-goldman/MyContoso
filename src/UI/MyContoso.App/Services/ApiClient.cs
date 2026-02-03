using System.Net.Http.Json;
using Shared;

namespace MyContoso.App.Services;

public class ApiClient(HttpClient httpClient)
{
    public async Task<IEnumerable<CompanyUpdate>> GetCompanyUpdatesAsync()
    {
        var updates = await httpClient.GetFromJsonAsync<IEnumerable<CompanyUpdate>>("/company-updates");
        return updates ?? [];
    }

    public async Task<CompanyUpdate?> GetCompanyUpdateAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<CompanyUpdate>($"/company-updates/{id}");
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var employees = await httpClient.GetFromJsonAsync<IEnumerable<Employee>>("/employees");
        return employees ?? [];
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<Employee>($"/employees/{id}");
    }

    public async Task<IEnumerable<Policy>> GetPoliciesAsync()
    {
        var policies = await httpClient.GetFromJsonAsync<IEnumerable<Policy>>("/policies");
        return policies ?? [];
    }

    public async Task<Policy?> GetPolicyAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<Policy>($"/policies/{id}");
    }

    public async Task<IEnumerable<Accreditation>> GetAccreditationsAsync()
    {
        var accreditations = await httpClient.GetFromJsonAsync<IEnumerable<Accreditation>>("/accreditations");
        return accreditations ?? [];
    }

    public async Task<Accreditation?> GetAccreditationAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<Accreditation>($"/accreditations/{id}");
    }
}
