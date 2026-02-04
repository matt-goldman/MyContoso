using CommunityToolkit.Mvvm.Messaging;
using MyContoso.App.Converters;
using MyContoso.App.Messages;

namespace MyContoso.App.Controls;

public partial class TitleView : ContentView
{
    public TitleView()
    {
        InitializeComponent();
        
        WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, (r, m) =>
        {
            Avatar.Text = InitialsConverter.GetInitials(m.Value.Name);
            Avatar.ImageSource = m.Value.AvatarUrl;
        });
    }
}