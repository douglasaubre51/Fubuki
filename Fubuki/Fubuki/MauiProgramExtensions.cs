using Fubuki.Services.RestServices;
using Fubuki.ViewModels;
using Microsoft.Extensions.Logging;

namespace Fubuki
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Add Services
            builder.Services.AddTransient<RenderClients>();

            builder.Services.AddTransient<MainPageViewModel>();

            return builder;
        }
    }
}
