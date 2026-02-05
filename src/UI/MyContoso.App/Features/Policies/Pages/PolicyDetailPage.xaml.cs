using MyContoso.App.Features.Policies.ViewModels;

namespace MyContoso.App.Features.Policies.Pages;

public partial class PolicyDetailPage : ContentPage
{
    public PolicyDetailPage(PolicyDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
