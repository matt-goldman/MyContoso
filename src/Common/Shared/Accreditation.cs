namespace Shared;

public record Accreditation(
    int AccreditationId,
    string Name,
    string Description,
    string Status,
    DateTime? ExpiryDate
);
