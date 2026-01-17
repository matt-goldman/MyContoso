using MyContoso.App.Features.CompanyFeed.ViewModels;

namespace MyContoso.App.Features.CompanyFeed.Views;

public partial class CompanyFeedPage : ContentPage
{
    private readonly CompanyFeedViewModel _viewModel;

    public CompanyFeedPage(CompanyFeedViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadUpdatesCommand.ExecuteAsync(null);
    }
}
