namespace MyContoso.App.Models;

public record CompanyUpdate(
    int UpdateId,
    string Title,
    string Body,
    DateTime PublishedDate,
    string AuthorName,
    string AuthorInitials,
    string AuthorRole,
    string? AuthorAvatarUrl,
    int Likes,
    int Comments,
    bool IsLiked
);
