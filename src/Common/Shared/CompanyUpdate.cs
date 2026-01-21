namespace Shared;

public record CompanyUpdate(
    int UpdateId,
    string Title,
    string Body,
    DateTime PublishedDate,
    string Author
);
