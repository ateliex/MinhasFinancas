using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhasFinancas.Models;

public class Categoria
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public Categoria(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
    public Categoria()
    {
    }
}
