using Microsoft.Extensions.Logging;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IctOSDatabase, ctOsDatabase>();
            builder.Services.AddTransient<ClientViewModel>();
            builder.Services.AddTransient<ClientPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AdminViewModel>();
            builder.Services.AddTransient<AdminPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
