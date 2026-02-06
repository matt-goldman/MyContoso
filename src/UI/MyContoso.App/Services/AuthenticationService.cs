using Shared;

namespace MyContoso.App.Services;

public class AuthenticationService(IApiClient client) : IAuthenticationService
{
    public async Task<bool> LoginAsync(string username, string password)
    {
        // simulate logging in - fetch employees from the API, pick one at random, set as current user
        var employees = await client.GetEmployeesAsync();
        
        var employeeList = employees?.ToList();
        if (employeeList == null || employeeList.Count == 0)
        {
            return false;
        }
        
        var random = new Random();
        var randomEmployee = employeeList[random.Next(employeeList.Count)];
        
        AuthenticationStateChanged?.Invoke(this, randomEmployee);
        CurrentUser = randomEmployee;
        
        return true;
    }

    public Employee? CurrentUser { get; private set; }

    public event EventHandler<Employee>? AuthenticationStateChanged;
}