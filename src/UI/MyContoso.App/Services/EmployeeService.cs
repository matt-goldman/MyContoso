using MyContoso.App.ApiModels;
using ApiEmployee = MyContoso.App.ApiModels.Employee;
using ApiAccreditation = MyContoso.App.ApiModels.Accreditation;
using Employee = MyContoso.App.Models.Employee;
using Accreditation = MyContoso.App.Models.Accreditation;

namespace MyContoso.App.Services;

public class EmployeeService(IApiClient apiClient)
{
    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var apiEmployees = await apiClient.GetEmployeesAsync();
        return apiEmployees.Select(MapToModel);
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        var apiEmployee = await apiClient.GetEmployeeAsync(id);
        return apiEmployee is null ? null : MapToModel(apiEmployee);
    }

    private static Employee MapToModel(ApiEmployee api)
    {
        return new Employee(
            api.EmployeeId,
            api.Name,
            GetInitials(api.Name),
            api.Role,
            api.Department,
            api.ProfileSummary,
            api.ContactInfo.Email,
            api.ContactInfo.Phone,
            api.ContactInfo.Address,
            api.ContactInfo.DateOfBirth,
            api.Accreditations.Select(MapAccreditation).ToList(),
            api.AvatarUrl
        );
    }

    private static Accreditation MapAccreditation(ApiAccreditation api)
    {
        return new Accreditation(
            api.AccreditationId,
            api.Name,
            api.Description,
            api.Status,
            api.Category,
            api.ExpiryDate
        );
    }

    private static string GetInitials(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "?";

        var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return parts.Length switch
        {
            0 => "?",
            1 => parts[0][..Math.Min(2, parts[0].Length)].ToUpperInvariant(),
            _ => $"{char.ToUpperInvariant(parts[0][0])}{char.ToUpperInvariant(parts[^1][0])}"
        };
    }
}
