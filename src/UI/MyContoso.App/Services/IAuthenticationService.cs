using Shared;

namespace MyContoso.App.Services;

public interface IAuthenticationService
{
    Task<bool> LoginAsync(string username, string password);
    
    Employee? CurrentUser { get; }
    
    event EventHandler<Employee> AuthenticationStateChanged;
}