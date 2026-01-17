using MyContoso.App.Services;
using Shared.Models;

namespace MyContoso.App.Features.Employees.Services;

/// <summary>
/// Service for managing employee data.
/// </summary>
public class EmployeeService
{
    private readonly ApiClient _apiClient;

    public EmployeeService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        // TODO: Implement actual API call via ApiClient
        await Task.Delay(500); // Simulate network delay

        return new List<Employee>
        {
            new Employee
            {
                EmployeeId = 1,
                Name = "Alice Johnson",
                Role = "Senior Software Engineer",
                Department = "Engineering",
                ProfileSummary = "10+ years of experience in building scalable applications.",
                AccreditationStatus = "Valid"
            },
            new Employee
            {
                EmployeeId = 2,
                Name = "Bob Smith",
                Role = "Product Manager",
                Department = "Product",
                ProfileSummary = "Leading product strategy and roadmap planning.",
                AccreditationStatus = "Valid"
            },
            new Employee
            {
                EmployeeId = 3,
                Name = "Carol Williams",
                Role = "UX Designer",
                Department = "Design",
                ProfileSummary = "Passionate about creating delightful user experiences.",
                AccreditationStatus = "Pending"
            },
            new Employee
            {
                EmployeeId = 4,
                Name = "David Brown",
                Role = "DevOps Engineer",
                Department = "Engineering",
                ProfileSummary = "Ensuring smooth deployments and infrastructure reliability.",
                AccreditationStatus = "Valid"
            }
        };
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
    {
        // TODO: Implement actual API call via ApiClient
        await Task.Delay(300);

        var employees = await GetEmployeesAsync();
        return employees.FirstOrDefault(e => e.EmployeeId == employeeId);
    }
}
