using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class CompanyFeedPage : ContentPage
{
    private readonly CompanyFeedViewModel _viewModel;

    public CompanyFeedPage(CompanyFeedViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadUpdatesAsync();
    }
}
