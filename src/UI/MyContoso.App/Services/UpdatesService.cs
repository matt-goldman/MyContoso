using MyContoso.App.Models;
using ApiCompanyUpdate = MyContoso.App.ApiModels.CompanyUpdate;

namespace MyContoso.App.Services;

public class UpdatesService(IApiClient apiClient, EmployeeService employeeService)
{
    // Cache employees to avoid repeated calls (makes the problem worse over time)
    private Dictionary<int, Employee>? _employeeCache;

    public async Task<IEnumerable<CompanyUpdate>> GetUpdatesAsync()
    {
        var apiUpdates = await apiClient.GetCompanyUpdatesAsync();
        var updates = new List<CompanyUpdate>();

        foreach (var apiUpdate in apiUpdates)
        {
            var update = await MapToModelAsync(apiUpdate);
            updates.Add(update);
        }

        return updates;
    }

    public async Task<CompanyUpdate?> GetUpdateAsync(int id)
    {
        var apiUpdate = await apiClient.GetCompanyUpdateAsync(id);
        return apiUpdate is null ? null : await MapToModelAsync(apiUpdate);
    }

    private async Task<CompanyUpdate> MapToModelAsync(ApiCompanyUpdate api)
    {
        // 🚨 Cross-domain call: Updates service reaching into Employees domain
        var author = await GetAuthorAsync(api.AuthorId);

        return new CompanyUpdate(
            api.UpdateId,
            api.Title,
            api.Body,
            api.PublishedDate,
            author?.Name ?? "Unknown",
            author?.Initials ?? "?",
            author?.Role ?? "Unknown",
            author?.AvatarUrl,
            api.Likes,
            api.Comments,
            api.IsLiked
        );
    }

    private async Task<Employee?> GetAuthorAsync(int authorId)
    {
        // Build cache if not present
        if (_employeeCache is null)
        {
            var employees = await employeeService.GetEmployeesAsync();
            _employeeCache = employees.ToDictionary(e => e.EmployeeId);
        }

        return _employeeCache.GetValueOrDefault(authorId);
    }
}
