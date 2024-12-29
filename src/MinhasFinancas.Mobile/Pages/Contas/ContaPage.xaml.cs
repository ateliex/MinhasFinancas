using MinhasFinancas.Data;
using MinhasFinancas.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using MinhasFinancas.Models;

namespace MinhasFinancas.Pages.Contas;

[QueryProperty(nameof(Conta), "Conta")]
public partial class ContaPage : ContentPage
{
    private readonly AppDbContext _db;

    public ICommand ExcluirCommand { get; set; }

    private Conta _conta;
    public Conta Conta
    {
        get => _conta;
        set
        {
            _conta = value;
            OnPropertyChanged();
        }
    }

    public IEnumerable<Categoria> Categorias { get; set; }

    public ICommand SalvarCommand { get; set; }

    public ContaPage(AppDbContext db)
    {
        InitializeComponent();

        _db = db;

        ExcluirCommand = new Command(Excluir);

        Conta = new Conta
        {
            Id = Guid.NewGuid()
        };

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        var empregadores = await _db.Categorias.ToListAsync();

        empregadores.Insert(0, new Categoria() );

        Categorias = empregadores;
    }

    private async void Salvar()
    {
        try
        {
            _db.Contas.Add(Conta);
            await _db.SaveChangesAsync();

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception _)
        {
            throw;
        }
    }

    private async void Excluir()
    {
        var yes = await DisplayAlert("Excluir Conta", "Tem certeza que deseja excluir isso?", "Sim", "Não");

        if (yes)
        {
            try
            {
                var conta = await _db.Contas.FindAsync(Conta.Id);

                if (conta != null)
                {
                    _db.Contas.Remove(conta);
                    await _db.SaveChangesAsync();
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception _)
            {
                throw;
            }

            await Shell.Current.GoToAsync("..");
        }
    }
}