using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.Features.Accreditations.Services;

public class AccreditationService(IApiClient apiClient)
{
    public Task<IEnumerable<Accreditation>> GetAccreditationsAsync()
     => apiClient.GetAccreditationsAsync();

    public async Task<Accreditation?> GetAccreditationAsync(int id)
        =>await apiClient.GetAccreditationAsync(id);
}