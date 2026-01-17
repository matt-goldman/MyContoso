using MyContoso.App.Features.Policies.ViewModels;

namespace MyContoso.App.Features.Policies.Views;

public partial class PoliciesPage : ContentPage
{
    private readonly PoliciesViewModel _viewModel;

    public PoliciesPage(PoliciesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPoliciesCommand.ExecuteAsync(null);
    }
}
