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
            builder.Services.AddSingleton<IUserContext, UserContext>();
            builder.Services.AddTransient<ClientViewModel>();
            builder.Services.AddTransient<ClientPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AdminViewModel>();
            builder.Services.AddTransient<AdminPage>();
            builder.Services.AddTransient<AddClientPage>();
            builder.Services.AddTransient<AddNewClientViewModel>();
            builder.Services.AddTransient<ClientDetailViewModel>();
            builder.Services.AddTransient<ClientDetailPage>();
            builder.Services.AddTransient<DeviceDetailPage>();
            builder.Services.AddTransient<DeviceDetailViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
