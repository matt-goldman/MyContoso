namespace Shared;

public record Policy(
    int PolicyId,
    string Title,
    string Category,
    DateTime LastUpdated,
    string UpdatedBy,
    string Description,
    string Content
);
