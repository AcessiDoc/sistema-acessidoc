using sistema_acessidoc;
using sistema_acessidoc.Models;
using SistemaAcessiDoc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace sistema_acessidoc.Models.Arquivos.Formulario
{
    public class Arquivo
    {
        public string Nome { get; set; }
        public string Extensao { get; set; }

        [Required(ErrorMessage = "Por favor, selecione um tamanho de fonte.")]
        [Range(18, 24, ErrorMessage = "Escolha o tamanho 18 ou 24.")]
        public int TamanhoFonte { get; set; }

        [FileExtensions(Extensions = ".doc,.docx,.pdf", 
        ErrorMessage = "Por favor, selecione um arquivo Word ou PDF.")]
        [NotMapped]
        public IFormFile ArquivoUpload { get; set; }
    }
}

