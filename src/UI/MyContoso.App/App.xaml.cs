namespace MyContoso.App;

public partial class App : Application
{
    private readonly AppShell shell;

    public App(AppShell shell)
    {
        this.shell = shell;
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(shell);
    }
}
