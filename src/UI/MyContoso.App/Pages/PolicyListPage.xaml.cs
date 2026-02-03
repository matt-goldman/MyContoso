using MyContoso.App.ViewModels;

namespace MyContoso.App.Pages;

public partial class PolicyListPage : ContentPage
{
    public PolicyListPage(PolicyListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PolicyListViewModel vm && vm.Policies.Count == 0)
        {
            await vm.LoadPoliciesCommand.ExecuteAsync(null);
        }
    }
}
