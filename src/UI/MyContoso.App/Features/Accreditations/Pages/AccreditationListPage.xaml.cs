using MyContoso.App.Features.Accreditations.ViewModels;

namespace MyContoso.App.Features.Accreditations.Pages;

public partial class AccreditationListPage : ContentPage
{
    public AccreditationListPage(AccreditationListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AccreditationListViewModel vm && vm.Accreditations.Count == 0)
        {
            await vm.LoadAccreditationsCommand.ExecuteAsync(null);
        }
    }
}
