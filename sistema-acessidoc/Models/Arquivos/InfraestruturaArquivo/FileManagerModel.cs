using sistema_acessidoc;
using sistema_acessidoc.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo
{
    public class FileManagerModel
    {
        public static int FontSize { get; set; }
        public IFormFile IFormFile { get; set; }
        public FileInfo[] Files { get; set; }
        public List<IFormFile> IFormFiles { get; set; }
        public string PathDocuments { get; set; }
    }
}