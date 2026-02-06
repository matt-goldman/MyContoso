using MyContoso.App.Models;

namespace MyContoso.App.ViewModels;

public class CompanyUpdateItemViewModel(CompanyUpdate update)
{
    // The underlying model
    public CompanyUpdate Update { get; } = update;

    // Pass-through properties for convenience
    public int UpdateId => Update.UpdateId;
    public string Title => Update.Title;
    public string Body => Update.Body;
    public DateTime PublishedDate => Update.PublishedDate;
    public string AuthorName => Update.AuthorName;
    public string AuthorInitials => Update.AuthorInitials;
    public string AuthorRole => Update.AuthorRole;
    public string? AuthorAvatarUrl => Update.AuthorAvatarUrl;
    public int Likes => Update.Likes;
    public int Comments => Update.Comments;
    public bool IsLiked => Update.IsLiked;

    public Color LikeButtonBackgroundColor => IsLiked
        ? Color.FromArgb("#1477ee") 
        : Colors.Transparent;

    public Color LikeButtonTextColor => IsLiked
        ? Colors.White
        : Colors.Black;
}
