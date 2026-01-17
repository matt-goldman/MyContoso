using MyContoso.App.Services;
using Shared.Models;

namespace MyContoso.App.Features.CompanyFeed.Services;

/// <summary>
/// Service for managing company feed data.
/// This is a singleton service that owns the authoritative state for company updates.
/// </summary>
public class CompanyFeedService
{
    private readonly ApiClient _apiClient;

    public CompanyFeedService(ApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<CompanyUpdate>> GetCompanyUpdatesAsync()
    {
        // TODO: Implement actual API call via ApiClient
        // For now, return stub data
        await Task.Delay(500); // Simulate network delay

        return new List<CompanyUpdate>
        {
            new CompanyUpdate
            {
                UpdateId = 1,
                Title = "Welcome to MyContoso",
                Body = "We're excited to launch our new company app. Stay tuned for updates and important announcements.",
                PublishedDate = DateTime.Now.AddDays(-7),
                Author = "CEO Team"
            },
            new CompanyUpdate
            {
                UpdateId = 2,
                Title = "Q4 Results",
                Body = "Great news! Our Q4 results exceeded expectations. Thank you all for your hard work and dedication.",
                PublishedDate = DateTime.Now.AddDays(-3),
                Author = "Finance Team"
            },
            new CompanyUpdate
            {
                UpdateId = 3,
                Title = "New Office Opening",
                Body = "We're opening a new office in Seattle next month. Congratulations to everyone involved in making this happen!",
                PublishedDate = DateTime.Now.AddDays(-1),
                Author = "HR Team"
            }
        };
    }
}
