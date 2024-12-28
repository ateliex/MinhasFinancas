using MinhasFinancas.Infrastructure;

namespace MinhasFinancas;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseSharedMauiApp();


        var app = builder.Build();

        DbModule.EnsureDatabaseExists(app.Services);

        return app;
    }
}
