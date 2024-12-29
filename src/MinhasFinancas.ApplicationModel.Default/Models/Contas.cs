using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinhasFinancas.Models;

public enum TipoConta
{
    Salario,
    VendaServico,
    VendaProduto,
    FaturaComum,
    FaturaCartaoCredito
}

public class Conta
{
    public Guid Id { get; set; }

    public TipoConta Tipo { get; set; }

    public bool Ativa { get; set; }

    public bool Paga { get; set; }

    public string Nome { get; set; }

    public string Numero { get; set; }

    public string Banco { get; set; }

    public string Documento { get; set; }

    public decimal Saldo { get; set; }

    public virtual ICollection<Financa> Financas { get; set; }

    public Conta(
        Guid id,
        TipoConta tipo,
        string nome,
        string numero,
        string banco,
        string documento,
        IContasRepository repository)
    {
        Id = id;

        Tipo = tipo;

        Nome = nome;

        Numero = numero;

        Banco = banco;

        Documento = documento;
    }

    public Conta()
    {

    }
}

public class FaturaComum : Conta
{

}

public class FaturaCartaoCredito : Conta
{
    public const decimal LIMITE_DE_SALDO = -20000;

    internal FaturaCartaoCredito(
        Guid id,
        TipoConta tipo,
        string nome,
        string numero,
        string banco,
        string documento,
        IContasRepository repository) : base(
            id,
            tipo,
            nome,
            numero,
            banco,
            documento,
            repository)
    {

    }

    internal void LancarCompra()
    {

    }

    internal async Task<Financa> Pagar(Pagamento pagamento)
    {
        if (Saldo - pagamento.Valor < LIMITE_DE_SALDO)
        {
            throw new ApplicationException("A conta não pode ficar mais de R$ 20.000,00 negativos");
        }

        Saldo = Saldo - pagamento.Valor;

        var financa = new Financa(
            Guid.NewGuid(),
            this,
            pagamento.Data,
            pagamento.Descricao,
            pagamento.Valor,
            TipoFinancaEnum.Despesa
        );

        Financas.Add(financa);

        return financa;
    }
}

public interface IContasRepository
{
    Task<Conta> ObtemConta(Guid id);

    Task Adiciona(Conta conta);

    Task Atualiza(Conta conta);
}
