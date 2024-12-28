using MinhasFinancas.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MinhasFinancas.Models;

namespace MinhasFinancas.Pages.Contas;

public partial class GestaoContasPage : ContentPage
{
    private readonly AppDbContext _db;

    public ICommand CriarNovoCommand { get; set; }

    public ObservableCollection<Conta> Contas { get; set; }

    public GestaoContasPage(AppDbContext db)
    {
        InitializeComponent();

        _db = db;

        Contas = _db.Contas.Local.ToObservableCollection();

        contasSearchHandler.Contas = Contas;

        CriarNovoCommand = new Command(CriarNovo);

        BindingContext = this;
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        await _db.Contas
            .LoadAsync();

        //Contas = _db.Contas.Local.ToObservableCollection();
    }

    private async void CriarNovo()
    {
        await Shell.Current.GoToAsync($"Conta");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _db.Contas
            .LoadAsync();

        var total = Contas.Count;

        await DisplayAlert("Pronto", $"Carregado {total} registro(s)!", "OK");
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var id = (e.SelectedItem as Conta).Id;

        var conta = await _db.Contas.FirstOrDefaultAsync(x => x.Id == id);

        await Shell.Current.GoToAsync("Conta", new Dictionary<string, object> { { "Conta", conta } });
    }
}