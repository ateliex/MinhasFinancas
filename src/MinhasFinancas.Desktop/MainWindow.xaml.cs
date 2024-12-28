using Microsoft.Extensions.DependencyInjection;
using MinhasFinancas.Pages.Categorias;
using MinhasFinancas.Pages.Contas;
using System;
using System.Windows;

namespace MinhasFinancas;

public partial class MainWindow : Window
{
    public IServiceProvider ServiceProvider { get; }

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        ServiceProvider = serviceProvider;
    }

    private void CadastroCategoriasMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var cadastroCategoriasWindow = ServiceProvider.GetRequiredService<CadastroCategoriasWindow>();

        cadastroCategoriasWindow.Show();
    }

    private void GestaoContasMenuItem_Click(object sender, RoutedEventArgs e)
    {
        var gestaoContasWindow = ServiceProvider.GetRequiredService<GestaoContasWindow>();

        gestaoContasWindow.Show();
    }

    private void configuracoesMenuItem_Click(object sender, RoutedEventArgs e)
    {

    }
}
