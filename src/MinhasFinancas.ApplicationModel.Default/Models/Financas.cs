using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinhasFinancas.Models;

public enum TipoFinancaEnum
{
    Receita,
    Despesa
}

public class Financa
{
    public Guid Id { get; internal set; }

    public virtual Conta Conta { get; internal set; }

    public Guid? ContaId { get; internal set; }

    public DateTime Data { get; internal set; }

    public bool Paga { get; internal set; }

    public string Descricao { get; internal set; }

    public decimal Valor { get; internal set; }

    public TipoFinancaEnum TipoId { get; internal set; }

    public Categoria Categoria { get; internal set; }

    public string CategoriaId { get; internal set; }

    public Financa(Guid id, Conta conta, DateTime data, string descricao, decimal valor, TipoFinancaEnum tipoId)
    {
        Id = id;

        Conta = conta;

        ContaId = conta?.Id;

        Data = data;

        Descricao = descricao;

        Valor = valor;

        TipoId = tipoId;
    }

    public Financa()
    {

    }
}

public class FinancaProcessadaEvent : INotification
{
    public Financa Financa { get; internal set; }
}

public interface IFinancasRepository
{
    Task<Financa> ObtemFinanca(Guid id);

    Task Adiciona(Financa financa);

    Task Atualiza(Financa financa);
}
