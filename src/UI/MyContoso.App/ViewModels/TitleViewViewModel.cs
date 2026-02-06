using CommunityToolkit.Mvvm.ComponentModel;
using MyContoso.App.Services;
using Shared;

namespace MyContoso.App.ViewModels;

public partial class TitleViewViewModel : ObservableObject
{
    [ObservableProperty]
    private Employee? employee;

    public TitleViewViewModel(IAuthenticationService authService)
    {
        authService.AuthenticationStateChanged += (sender, loggedInUser) => Employee = loggedInUser;
    }
}