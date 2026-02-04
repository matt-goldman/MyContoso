using MyContoso.App.Converters;

namespace MyContoso.App.Controls;

public partial class TitleView : ContentView
{
    public TitleView()
    {
        InitializeComponent();
        
        App.CurrentUserChanged += OnCurrentUserChanged;
    }

    private void OnCurrentUserChanged(object? sender, EventArgs e)
    {
        var user = App.CurrentUser;
        if (user is null) return;

        Avatar.Text = InitialsConverter.GetInitials(user.Name);

        Avatar.ImageSource = user.AvatarUrl;
    }
}