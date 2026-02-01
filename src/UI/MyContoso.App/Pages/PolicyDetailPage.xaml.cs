using MyContoso.App.ViewModels;

namespace MyContoso.App.Pages;

public partial class PolicyDetailPage : ContentPage
{
    public PolicyDetailPage(PolicyDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
