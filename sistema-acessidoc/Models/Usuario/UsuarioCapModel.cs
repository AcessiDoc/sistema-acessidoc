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
        [Required(ErrorMessage = "O campo de e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, digite um endereço de e-mail válido.")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo senha é obrigatório.")]
        public string Senha { get; set; }
    }
}