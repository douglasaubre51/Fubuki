using Fubuki.ViewModels;

namespace Fubuki.Views;

public partial class Deploys : ContentPage
{
    public Deploys(DeploysViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var context = BindingContext as DeploysViewModel;
        context!.IsPageLoading = true;
    }
}