using CommunityToolkit.Mvvm.Messaging;
using MyContoso.App.Messages;

namespace MyContoso.App.Controls;

public partial class TitleView : ContentView
{
    public TitleView()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, (r, m) =>
        {
            Avatar.Text = m.Value.Initials;
            Avatar.ImageSource = m.Value.AvatarUrl;
        });
    }
}
