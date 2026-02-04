using CommunityToolkit.Mvvm.Messaging;
using MyContoso.App.Messages;
using MyContoso.App.Models;
using MyContoso.App.Pages;

namespace MyContoso.App
{
    public partial class App : Application
    {
        private readonly LoginPage loginPage;

        public static Employee? CurrentUser
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(field!));
            }
        }

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
