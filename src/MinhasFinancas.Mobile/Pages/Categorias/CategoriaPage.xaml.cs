using MinhasFinancas.Data;
using System.Windows.Input;
using MinhasFinancas.Models;

namespace MinhasFinancas.Pages.Categorias;

[QueryProperty(nameof(Categoria), "Categoria")]
public partial class CategoriaPage : ContentPage
{
    private readonly AppDbContext _db;

    public ICommand ExcluirCommand { get; set; }

    private Categoria _empregador;
    public Categoria Categoria
    {
        get => _empregador;
        set
        {
            _empregador = value;
            OnPropertyChanged();
        }
    }

    public ICommand SalvarCommand { get; set; }

    public CategoriaPage(AppDbContext db)
    {
        InitializeComponent();

        _db = db;

        ExcluirCommand = new Command(Excluir);

        Categoria = new Categoria
        {
            
        };

        SalvarCommand = new Command(Salvar);

        BindingContext = this;
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {

    }

    private async void Salvar()
    {
        try
        {
            _db.Categorias.Add(Categoria);
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
        var yes = await DisplayAlert("Excluir Categoria", "Tem certeza que deseja excluir isso?", "Sim", "Não");

        if (yes)
        {
            try
            {
                var empregador = await _db.Categorias.FindAsync(Categoria.Id);

                if (empregador != null)
                {
                    _db.Categorias.Remove(empregador);
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