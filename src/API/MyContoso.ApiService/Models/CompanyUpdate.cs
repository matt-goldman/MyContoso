namespace MyContoso.ApiService.Models;

public record CompanyUpdate(
    int UpdateId,
    string Title,
    string Body,
    DateTime PublishedDate,
    Employee Author,
    int Likes,
    int Comments,
    bool IsLiked
);
