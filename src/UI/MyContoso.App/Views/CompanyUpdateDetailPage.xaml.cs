using MyContoso.App.ViewModels;

namespace MyContoso.App.Views;

public partial class CompanyUpdateDetailPage : ContentPage
{
    public CompanyUpdateDetailPage(CompanyUpdateDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
