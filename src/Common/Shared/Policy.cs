namespace Shared;

public record Policy(
    int PolicyId,
    string Title,
    string Category,
    DateTime LastUpdated,
    string Content
);
