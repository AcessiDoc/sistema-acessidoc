using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo;
using System.Reflection;
using System.Linq;
using Humanizer.Bytes;

namespace sistema_acessidoc.Controllers
{
    /// <summary>
    /// Acessidoc - 2024
    /// 
    /// Controlador responsável pelo upload de arquivos vindo do cliente,
    /// os arquivos são enviados para a pasta wwwroot/arquivosPDF. Será
    /// utilizado propriedades somente leitura para fazer a injetar os dados
    /// vindo da camada cliente para o servidor (dados da pasta onde será salvo os
    /// arquivos estão na classe ConfigurationFiles e no appsettings.json.
    /// 
    /// Seu método UploadFile recebe como parâmetro um arquivo word ou pdf
    /// e um tamanho de fonte do tipo inteiro 18 ou 24, após as verificações
    /// de segurança (quantidade, tipo, nullable e tamanho máximo) para validar
    /// as informações enviadas ao servidor, será aceito arquivos de até 15MB
    /// para que não haja sobrecarga do mesmo.
    /// 
    /// Quando o envio é feito com sucesso ou um erro ocorre as mensagens
    /// no ViewData são enviadas a view UploadFile, informando o usuário.
    /// 
    /// **Verificar -> View/UploadFile**
    /// 
    /// </summary>
    
    public class HomeController : Controller
    {
        private readonly ConfigurationFiles _myConfigurationFiles;
        private readonly ConfigurationFiles _myConfigurationToSavedFiles;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EditorArquivos _editorArquivos; 

        public HomeController(IWebHostEnvironment webHostEnvironment,
                              IOptions<ConfigurationFiles> myConfigurationFiles,
                              IOptions<ConfigurationFiles> myConfigurationToSavedFiles,
                              EditorArquivos editorArquivos)
        {
            _myConfigurationFiles = myConfigurationFiles.Value;
            _myConfigurationToSavedFiles = myConfigurationToSavedFiles.Value;
            _webHostEnvironment = webHostEnvironment;
            _editorArquivos = editorArquivos;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UploadFile(List<IFormFile> files, int fontSize)
        {           
            if (files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Arquivo não selecionado!";
                return View(ViewData);
            }

            if (files.Count > 1)
            {
                ViewData["Erro"] = "Quantidade de arquivos excedeu o limite," +
                                   " selecione apenas 1 para efetuar o upload!";

                return View(ViewData);
            }

            var font = fontSize;
            FileManagerModel.FontSize = font;

            if (font != 18 && font != 24)
            {
                ViewData["Erro"] = "Tamanho da fonte não foi selecionado!";
                return View(ViewData);
            }

            // Valida tamanho máximo do arquivo que poderá ser enviado ao servidor
            long tamanhoDoArquivoEmBytes = files.Sum(file => file.Length);
            var tamanhoDoArquivoEmMegaBytes = ConversorDeBytes.ConverterParaMegaBytes(tamanhoDoArquivoEmBytes);
            var verificaTamanhoMaximoArquivo = ConversorDeBytes.VerificarTamanhoMaximoArquivo(tamanhoDoArquivoEmBytes);

            if (verificaTamanhoMaximoArquivo == true)
            {
                ViewData["Erro"] = "Apenas arquivos com tamanho inferior a 15MB poderão ser editados!";
                return View(ViewData);
            }

            var filePathName = new List<string>();

            // Cria o caminho do arquivo que será salvo no wwwroot/arquivosPDF
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                                        _myConfigurationFiles.DocumentsFolderName);

            foreach (var file in files)
            {
                if (file.FileName.Contains(".pdf") || file.FileName.Contains(".doc") || file.FileName.Contains(".docx"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", file.FileName);
                    filePathName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        // Salva o stream de dados do arquivo na pasta arquivosPDF
                        await file.CopyToAsync(stream);
                    }
                }
                else
                {
                    ViewData["Erro"] = "Formato de arquivo não compatível, anexe apenas PDF ou Word!";
                    return View(ViewData);
                }
            }

            ViewData["Resultado"] = $"{files.Count()} arquivo foi enviado ao servidor, " +
                                    $"com tamanho total de: {tamanhoDoArquivoEmMegaBytes.ToString("N3")} MB";

            // Chamada ao método que processa os arquivos pdf, 
            // como parametro recebe os dados do arquivo como
            // um stram e o tamnho da fonte enviada pelo usuário.
            foreach (var file in filePathName)
            {
                try
                {
                    await _editorArquivos.ProcessarArquivoPDF(file, fontSize);
                }
                catch (FileNotFoundException ex)
                {
                    ViewData["Erro"] = $"{ex}, verifique o arquivo e tente novamente!";
                    return View(ViewData);
                }
                catch (IOException ex)
                {
                    ViewData["Erro"] = $"{ex}";
                    return View(ViewData);
                }
                catch (Exception ex)
                {
                    ViewData["Erro"] = $"{ex}";
                    return View(ViewData);
                }
            }

            // Retorna a view com as informações ao usuário
            ViewBag.Arquivos = filePathName;
            var fileProcessedPath = Path.Combine(_webHostEnvironment.WebRootPath, _myConfigurationFiles.DocumentsSavedFolderName, "documento_processado.pdf");
            ViewData["FileProcessedPath"] = fileProcessedPath;
            return View("UploadFile");
        }
    }
}