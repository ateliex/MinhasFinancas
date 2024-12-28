using MinhasFinancas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MinhasFinancas.Models;

namespace MinhasFinancas.Pages.Categorias;

public partial class CadastroCategoriasWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly AppDbContext _db;

    private CollectionViewSource _categoriasViewSource;

    private ObservableCollection<Categoria> _categorias;

    public CadastroCategoriasWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        _categoriasViewSource = ((CollectionViewSource)(this.FindResource("categoriasViewSource")));

        await _db.Categorias
            .LoadAsync();

        _categorias = _db.Categorias.Local.ToObservableCollection();

        _categorias.CollectionChanged += Categorias_CollectionChanged;

        _categoriasViewSource.Source = _categorias;

        Cursor = null;
    }

    private void Categorias_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var categoria = e.NewItems[0] as Categoria;
        }
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource categoriasViewSource = ((CollectionViewSource)(this.FindResource("categoriasViewSource")));

        //var observableCollection = (CategoriasCollection)categoriasViewSource.Source;

        categoriaViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Categorias salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoCategoriaButton_Click(object sender, RoutedEventArgs e)
    {
        var categoria = new Categoria
        {
            //CreationDate = DateTime.Now,
        };

        //var daysOfWeek = Enum.GetValues<DayOfWeek>();

        //foreach (var dayOfWeek in daysOfWeek)
        //{
        //    var jornadaTrabalhoDiaria = new JornadaTrabalhoDiaria
        //    {
        //        DiaSemana = dayOfWeek,
        //        Tempo = new TimeSpan(8, 0, 0)
        //    };

        //    categoria.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _categorias.Add(categoria);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

