using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using sistema_acessidoc.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace sistema_acessidoc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvaController : ControllerBase
    {
        private readonly FileUploadService _fileUploadService;

        public ProvaController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("EnviarParaNode")]
        public async Task<IActionResult> EnviarParaNode([FromForm] IFormFile document, [FromForm] string fontSize)
        {
            if (document == null || document.Length == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            try
            {
                // Converte o arquivo enviado para bytes
                using (var stream = document.OpenReadStream())
                {
                    byte[] fileBytes = new byte[stream.Length];
                    stream.Read(fileBytes, 0, (int)stream.Length);

                    // Envia o arquivo para o servidor Node.js
                    using (var client = new HttpClient())
                    {
                        using (var content = new MultipartFormDataContent())
                        {
                            content.Add(new ByteArrayContent(fileBytes), "document", document.FileName);
                            content.Add(new StringContent(fontSize), "fontSize");

                            var response = await client.PostAsync("http://localhost:3000/upload", content);

                            if (!response.IsSuccessStatusCode)
                            {
                                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                            }
                            return Ok("Arquivo enviado com sucesso para o servidor Node.js");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }
    }
}
