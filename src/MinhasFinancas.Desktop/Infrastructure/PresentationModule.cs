using Microsoft.Extensions.DependencyInjection;
using MinhasFinancas.Pages.Categorias;
using MinhasFinancas.Pages.Contas;

namespace MinhasFinancas.Infrastructure;

public static class PresentationModule
{
    public static IServiceCollection AddWindows(this IServiceCollection services)
    {
        services.AddTransient(typeof(MainWindow));
        
        services.AddTransient(typeof(CadastroCategoriasWindow));
        services.AddTransient(typeof(GestaoContasWindow));

        return services;
    }
}
