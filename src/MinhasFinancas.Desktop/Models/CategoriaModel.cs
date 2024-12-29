using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhasFinancas.Models;

public class CategoriaModel
{
    public string? Id { get; set; }

    public string? Nome { get; set; }

    public bool? Ativa { get; set; }

    public TipoFinancaEnum? TipoId { get; set; }
}
