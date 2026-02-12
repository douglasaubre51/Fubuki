using Fubuki.ViewModels;

namespace Fubuki
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var context = BindingContext as MainPageViewModel;
            context!.IsPageLoading = true;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            
        }
    }

}
