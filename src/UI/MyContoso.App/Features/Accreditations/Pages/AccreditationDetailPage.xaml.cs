using MyContoso.App.Features.Accreditations.ViewModels;

namespace MyContoso.App.Features.Accreditations.Pages;

public partial class AccreditationDetailPage : ContentPage
{
    public AccreditationDetailPage(AccreditationDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
