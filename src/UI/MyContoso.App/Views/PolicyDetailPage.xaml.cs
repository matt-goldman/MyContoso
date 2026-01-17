using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class PolicyDetailPage : ContentPage
{
    public PolicyDetailPage(PolicyDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
