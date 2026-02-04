using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.Features.Employees.Services;

public class EmployeeService(IApiClient apiClient)
{
    public Task<IEnumerable<Employee>> GetEmployeesAsync()
        => apiClient.GetEmployeesAsync();

    public Task<Employee?> GetEmployeeAsync(int id)
        => apiClient.GetEmployeeAsync(id);
}
