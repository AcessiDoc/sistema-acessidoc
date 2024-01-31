using System;
using System.Collections.Generic;

namespace sistema_acessidoc.Models;

public partial class Usuario
{
    public long UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;
}
