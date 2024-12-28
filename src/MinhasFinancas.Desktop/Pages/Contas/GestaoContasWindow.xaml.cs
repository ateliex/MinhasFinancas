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

namespace MinhasFinancas.Pages.Contas;

public partial class GestaoContasWindow : Window
{
    private readonly IServiceScope _scope;

    private readonly AppDbContext _db;

    private CollectionViewSource _contasViewSource;

    private ObservableCollection<Conta> _contas;

    public GestaoContasWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _scope = serviceProvider.CreateScope();

        _db = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Cursor = Cursors.Wait;

        _contasViewSource = ((CollectionViewSource)(this.FindResource("contasViewSource")));

        await _db.Contas
            .LoadAsync();

        _contas = _db.Contas.Local.ToObservableCollection();

        _contas.CollectionChanged += Contas_CollectionChanged;

        _contasViewSource.Source = _contas;

        Cursor = null;
    }

    private void Contas_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            var conta = e.NewItems[0] as Conta;
        }
    }

    private void SetStatusBar(string value)
    {
        statusBarLabel.Content = value;

        //statusBarTimer.Enabled = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        //CollectionViewSource contasViewSource = ((CollectionViewSource)(this.FindResource("contasViewSource")));

        //var observableCollection = (ContasCollection)contasViewSource.Source;

        contaViewModelDataGrid.CommitEdit();

        try
        {
            await _db.SaveChangesAsync();

            SetStatusBar("Contas salvos com sucesso.");
        }
        catch (Exception ex)
        {
            SetStatusBar(ex.Message);
        }
    }

    private void novoContaButton_Click(object sender, RoutedEventArgs e)
    {
        var conta = new Conta
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

        //    conta.JornadaTrabalhoSemanalPrevista.Semana.Add(jornadaTrabalhoDiaria);
        //}

        _contas.Add(conta);
    }

    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
        //_db.Database.CloseConnection();

        _db.Dispose();

        _scope.Dispose();
    }
}

