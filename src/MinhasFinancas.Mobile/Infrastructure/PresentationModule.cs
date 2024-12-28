using MinhasFinancas.Pages.Categorias;
using MinhasFinancas.Pages.Contas;

namespace MinhasFinancas.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainPage));

        services.AddTransient(typeof(CadastroCategoriasPage));
        services.AddTransient(typeof(CategoriaPage));

        services.AddTransient(typeof(GestaoContasPage));
        //services.AddTransient(typeof(ContaPage));

        return services;
    }
}
