using MinhasFinancas.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MinhasFinancas.Models;

namespace MinhasFinancas.Pages.Categorias;

public partial class CadastroCategoriasPage : ContentPage
{
    private readonly AppDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Categoria> Categorias { get; set; }

    public CadastroCategoriasPage(AppDbContext db)
    {
        InitializeComponent();

        _db = db;

        Categorias = _db.Categorias.Local.ToObservableCollection();

        categoriasSearchHandler.Categorias = Categorias;

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Categorias
            .LoadAsync();

        //Categorias = _db.Categorias.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Categoria");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Categorias
            .LoadAsync();

        var total = Categorias.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var id = (e.SelectedItem as Categoria).Id;

        var empregador = await _db.Categorias.FirstOrDefaultAsync(x => x.Id == id);

        await Shell.Current.GoToAsync("Categoria", new Dictionary<string, object> { { "Categoria", empregador } });
    }
}