using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyContoso.App.Services;

namespace MyContoso.App.ViewModels;

public partial class LoginViewModel(IAuthenticationService service) : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string username;
    
    [ObservableProperty]
    private string password;
    
    public INavigation? Navigation { get; set; }

    [RelayCommand]
    public async Task Login()
    {
        IsLoading = true;
        
        var isLoggedIn = await service.LoginAsync(Username, Password);
        
        IsLoading = false;
        
        if (!isLoggedIn) return;
        
        await Navigation?.PopModalAsync();
    }
}