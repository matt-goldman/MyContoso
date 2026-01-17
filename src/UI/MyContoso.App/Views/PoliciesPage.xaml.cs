using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class PoliciesPage : ContentPage
{
    private readonly PoliciesViewModel _viewModel;

    public PoliciesPage(PoliciesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadPoliciesAsync();
    }
}
