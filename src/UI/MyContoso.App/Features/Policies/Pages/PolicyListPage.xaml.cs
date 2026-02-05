using MyContoso.App.Features.Policies.ViewModels;

namespace MyContoso.App.Features.Policies.Pages;

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
