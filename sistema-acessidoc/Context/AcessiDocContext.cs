using Microsoft.EntityFrameworkCore;
using SistemaAcessiDoc.Models;

namespace sistema_acessidoc.Context
{
    public class AcessiDocContext : DbContext
    {
        public AcessiDocContext(DbContextOptions<AcessiDocContext> options) : base(options) { }

        public DbSet<UsuarioCapModel> Usuarios { get; set; }
    }
}