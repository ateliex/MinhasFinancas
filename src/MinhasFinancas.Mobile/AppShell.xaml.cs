using MinhasFinancas.Pages.Categorias;
using MinhasFinancas.Pages.Contas;

namespace MinhasFinancas;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Contas/Conta", typeof(ContaPage));
        Routing.RegisterRoute("Categorias/Categoria", typeof(CategoriaPage));
    }
}
