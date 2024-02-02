using sistema_acessidoc.Models.Arquivos.Formulario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcessiDoc.Models
{
    [Table("usuarios")]
    public class UsuarioCapModel
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Digite um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; }
    }
}