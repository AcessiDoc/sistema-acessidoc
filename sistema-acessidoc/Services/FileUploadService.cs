using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http; 

namespace sistema_acessidoc.Services
{
    public class FileUploadService
    {
        public FileUploadResult UploadFile(IFormFile file)
        {
         
            var result = new FileUploadResult { Success = false, Message = "Um erro inesperado aconteceu" };

            if (ValidateFileFormat(file))
            {
                try
                {
                    var filePath = Path.Combine("C:\\Users\\cwbma\\Downloads\\teste", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    result.Success = true;
                    result.Message = "Arquivo carregado com sucesso";
                }
                catch (Exception ex)
                {
                    result.Message = $"Falha no carregamento do arquivo: {ex.Message}";
                }
            }
            else
            {
                result.Message = "Arquivo com formato invalido.Somente permitdo .doc ou .pdf";
            }

            return result;
        }

        private bool ValidateFileFormat(IFormFile file)
        {
            string[] validFormats = { ".pdf", ".doc", ".docx" };
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            return validFormats.Contains(fileExtension);
        }

    }

}

