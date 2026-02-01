using MyContoso.App.ViewModels;

namespace MyContoso.App.Pages;

public partial class AccreditationDetailPage : ContentPage
{
    public AccreditationDetailPage(AccreditationDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
