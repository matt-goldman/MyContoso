using MyContoso.App.Models;
using ApiAccreditation = MyContoso.App.ApiModels.Accreditation;

namespace MyContoso.App.Services;

public class AccreditationService(IApiClient apiClient)
{
    public async Task<IEnumerable<Accreditation>> GetAccreditationsAsync()
    {
        var apiAccreditations = await apiClient.GetAccreditationsAsync();
        return apiAccreditations.Select(MapToModel);
    }

    public async Task<Accreditation?> GetAccreditationAsync(int id)
    {
        var apiAccreditation = await apiClient.GetAccreditationAsync(id);
        return apiAccreditation is null ? null : MapToModel(apiAccreditation);
    }

    private static Accreditation MapToModel(ApiAccreditation api)
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
}
