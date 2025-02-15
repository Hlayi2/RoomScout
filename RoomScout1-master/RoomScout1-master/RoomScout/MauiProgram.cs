using Microsoft.Extensions.Logging;
using RoomScout.Interfaces;
using RoomScout.Services;
using RoomScout.Services.Auth;
using RoomScout.ViewModels.Auth;
using RoomScout.ViewModels.StudentSide;
using RoomScout.Views.Auth;
using RoomScout.Views.StudentSide;

namespace RoomScout
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IFirebaseAuthService, FirebaseAuthService>();
            builder.Services.AddSingleton<IFirebaseDataService, FirebaseDataService>();


            builder.Services.AddSingleton<BrowseListingsViewModel>();
            builder.Services.AddSingleton<BrowseListingsPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
