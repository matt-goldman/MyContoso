using MyContoso.App.Pages;
using Shared;

namespace MyContoso.App
{
    public partial class App : Application
    {
        private readonly LoginPage loginPage;
        
        private static Employee? _currentUser;

        public static Employee? CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser == value) return;
                _currentUser = value;
                CurrentUserChanged?.Invoke(Current, EventArgs.Empty);
            }
        }
        public static event EventHandler? CurrentUserChanged;
        
        public App(LoginPage loginPage)
        {
            this.loginPage = loginPage;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(loginPage));
        }
    }
}