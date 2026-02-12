using Fubuki.Views;

namespace Fubuki
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Deploys", typeof(Deploys));
        }
    }
}
