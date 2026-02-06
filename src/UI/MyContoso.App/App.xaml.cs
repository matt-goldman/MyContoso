using MyContoso.App.Pages;
using MyContoso.App.Services;
using MyContoso.App.ViewModels;

namespace MyContoso.App;

public partial class App : Application
{

    private AppShell shell;
    
    public App(
        IAuthenticationService authenticationService,
        LoginPage loginPage,
        TitleViewViewModel titleViewViewModel)
    {
        InitializeComponent();
        
        shell = new AppShell(authenticationService, loginPage, titleViewViewModel);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(shell);
    }
}
