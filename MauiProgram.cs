using AppSquiliteS5.Repositories;
using Microsoft.Extensions.Logging;

namespace AppSquiliteS5;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "personas.db3");
        builder.Services.AddSingleton(new PersonRepository(databasePath));
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
