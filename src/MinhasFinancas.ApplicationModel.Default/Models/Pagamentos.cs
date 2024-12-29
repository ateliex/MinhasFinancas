using System;
using System.Collections.Generic;

namespace MinhasFinancas.Models;

public class Pagamento : ValueObject
{
    public decimal Valor { get; set; }

    public string Descricao { get; set; }

    public DateTime Data { get; set; }

    public Pagamento(decimal valor, string descricao, DateTime data)
    {
        Descricao = descricao;

        Valor = valor;

        Data = data;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Valor;

        yield return Descricao;

        yield return Data;
    }

    public Pagamento()
    {

    }
}