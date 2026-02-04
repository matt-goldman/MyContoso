using MyContoso.App.ApiModels;

namespace MyContoso.App.Services;

public interface IApiClient
{
    Task<IEnumerable<CompanyUpdate>> GetCompanyUpdatesAsync();
    Task<CompanyUpdate?> GetCompanyUpdateAsync(int id);
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee?> GetEmployeeAsync(int id);
    Task<IEnumerable<Policy>> GetPoliciesAsync();
    Task<Policy?> GetPolicyAsync(int id);
    Task<IEnumerable<Accreditation>> GetAccreditationsAsync();
    Task<Accreditation?> GetAccreditationAsync(int id);
}
