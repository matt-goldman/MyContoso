using MyContoso.App.ViewModels;

namespace MyContoso.App.Pages;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;
    
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _viewModel.Navigation = Navigation;
        BindingContext = _viewModel;
    }
}