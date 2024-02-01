using sistema_acessidoc;
using sistema_acessidoc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace sistema_acessidoc.Models.Arquivos.Formulario
{
    public class Arquivo
    {
        [Required(ErrorMessage = "Selecione um tamanho de fonte.")]
        [Range(18, 24, ErrorMessage = "Escolha o tamanho 18 ou 24.")]
        public int TamanhoFonte { get; set; }

        [Required(ErrorMessage = "Selecione um arquivo Word ou PDF.")]
        [NotMapped]
        public IFormFile ArquivoUpload { get; set; }
    }
}