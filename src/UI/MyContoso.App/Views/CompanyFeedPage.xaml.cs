using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class CompanyFeedPage : ContentPage
{
    public CompanyFeedPage(CompanyFeedViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CompanyFeedViewModel vm && vm.Updates.Count == 0)
        {
            await vm.LoadUpdatesCommand.ExecuteAsync(null);
        }
    }
}
