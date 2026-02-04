namespace MyContoso.App.ApiModels;

public record CompanyUpdate(
    int UpdateId,
    string Title,
    string Body,
    DateTime PublishedDate,
    int AuthorId,
    int Likes,
    int Comments,
    bool IsLiked
);
