using MinhasFinancas.Pages.Categorias;

namespace MinhasFinancas;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Categorias/Categoria", typeof(CategoriaPage));
    }
}
