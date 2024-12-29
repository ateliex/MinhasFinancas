using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinhasFinancas.Models;

public class CartaoCredito
{
    public const decimal LIMITE_DE_SALDO = -20000;

    public Guid Id { get; set; }

    public TipoConta Tipo { get; set; }

    public bool Ativa { get; set; }

    public bool Paga { get; set; }

    public string Nome { get; set; }

    public string Numero { get; set; }

    public string Banco { get; set; }

    public string Documento { get; set; }

    public virtual ICollection<Saldo> Saldos { get; set; }

    public virtual ICollection<Financa> Financas { get; set; }

    internal ICartoesRepository _repository;

    public CartaoCredito(
        Guid id,
        TipoConta tipo,
        string nome,
        string numero,
        string banco,
        string documento,
        ICartoesRepository repository)
    {
        Id = id;

        Tipo = tipo;

        Nome = nome;

        Numero = numero;

        Banco = banco;

        Documento = documento;

        //Saldos = new HashSet<Saldo>();

        //Saldos.Add(new Saldo(this, DateTime.Today, saldo));

        _repository = repository;
    }

    internal void LancarCompra()
    {

    }

    internal async Task Pagar(Pagamento pagamento)
    {
        var saldo = await ObtemSaldoNaData(pagamento.Data);

        if (saldo.Valor - pagamento.Valor < LIMITE_DE_SALDO)
        {
            throw new ApplicationException("A conta não pode ficar mais de R$ 20.000,00 negativos");
        }

        saldo.Valor = saldo.Valor - pagamento.Valor;

        //var financa = new Financa(
        //    Guid.NewGuid(),
        //    this,
        //    pagamento.Data,
        //    pagamento.Descricao,
        //    pagamento.Valor,
        //    TipoFinanca.Despesa
        //);

        //await _repository.Adiciona(financa);

        //return financa;
    }

    public async Task<Saldo> ObtemSaldoNaData(DateTime data)
    {
        var saldo = await _repository.ObtemSaldoDaContaNaDataOrDefault(this, data);

        if (saldo == default)
        {
            var saldoNovo = await CriaSaldoNovoNaData(data);

            return saldoNovo;
        }
        else
        {
            return saldo;
        }
    }

    private async Task<Saldo> CriaSaldoNovoNaData(DateTime data)
    {
        var saldoNovo = new Saldo(this, data, valor: 0);

        await _repository.Adiciona(saldoNovo);

        return saldoNovo;
    }

    public CartaoCredito()
    {
        Saldos = new HashSet<Saldo>();
    }
}

public class Saldo
{
    [JsonIgnore]
    public virtual CartaoCredito Cartao { get; set; }

    public Guid CartaoId { get; set; }

    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public Saldo(CartaoCredito cartao, DateTime data, decimal valor)
    {
        Cartao = cartao;

        CartaoId = cartao.Id;

        Data = data.Date;

        Valor = valor;
    }

    public Saldo()
    {

    }
}

public interface ICartoesRepository
{
    Task<Conta> ObtemCartao(string numeroDaConta, string numeroDoBanco, TipoConta tipoConta, string numeroDoCpfOuCnpj);

    //decimal ObtemSaldoDaContaNaData(Conta conta, DateTime data);

    Task<Saldo> ObtemSaldoDaContaNaDataOrDefault(CartaoCredito cartao, DateTime data);

    Task Atualiza(Conta conta);

    Task Adiciona(Saldo saldo);

    Task Adiciona(Financa financa);
}
