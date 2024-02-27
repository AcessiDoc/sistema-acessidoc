using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo
{
    public sealed class EditorArquivos
    {
        // Injeta as dependencias necessárias para processar
        // o arquivo vindo da camada cliente e salvar na pasta
        // após o processamento.
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _myConfigurationFiles;

        public EditorArquivos(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _myConfigurationFiles = configuration;
        }

        public async Task<ActionResult> ProcessarArquivoPDF(string filePath, int fontSize)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException($"O arquivo '{filePath}' não foi encontrado!");

            // Valida o tamanho da fonte de acordo as normas da ABNT -> verificar classe ABNTConfiguration e métodos estáticos
            if (fontSize == 18) fontSize = ABNTConfiguration.ABNT_FONT_SIZE_STANDARD;
            if (fontSize == 24) fontSize = ABNTConfiguration.ABNT_FONT_SIZE_ENLARGED;

            // o using é usado para abrir, fechar e criar arquivos com segurança
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (var pdfReader = new PdfReader(fileStream))
                {
                    using (var documentPDF = new PdfDocument(pdfReader))
                    {
                        using (var document = new Document(documentPDF))
                        {
                            // Adiciona as margens ao arquivo criado
                            document.SetTopMargin(ABNTConfiguration.ABNT_MARGIN_TOP);
                            document.SetBottomMargin(ABNTConfiguration.ABNT_MARGIN_BOTTOM);
                            document.SetLeftMargin(ABNTConfiguration.ABNT_MARGIN_LEFT);
                            document.SetRightMargin(ABNTConfiguration.ABNT_MARGIN_RIGHT);
                            document.SetWordSpacing(ABNTConfiguration.ABNT_LINE_SPACING);

                            try
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    // Valida os dados da fonte verdana
                                    PdfFont font = null;
                                    if (!PdfFontFactory.IsRegistered("Verdana"))
                                    {
                                        // Carrega a fonte Verdana a partir do arquivo TTF
                                        var fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "verdana.ttf");
                                        font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
                                    }
                                    else
                                    {
                                        // Joga uma exceção ->
                                        throw new FileNotFoundException("Tipo de fonte Verdana não foi encontrada no diretório!");
                                    }

                                    // Loop naspáginas do arquivo
                                    for (int i = 0; i < documentPDF.GetNumberOfPages(); i++)
                                    {
                                        var page = documentPDF.GetPage(i);

                                        // Cria um novo conteúdo para a página
                                        var contentStream = new PdfStream();
                                        var pageResources = new PdfResources();

                                        // Define um novo canvas para desenhar na página
                                        var pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), documentPDF);
                                        var canvas = new Canvas(pdfCanvas, page.GetPageSize());

                                        // Cria um novo parágrafo com as configurações desejadas
                                        var paragraph = new Paragraph()
                                                            .SetFontSize(fontSize)
                                                            .SetTextAlignment(TextAlignment.JUSTIFIED)
                                                            .SetFont(font);

                                        // Adiciona o parágrafo ao canvas
                                        canvas.Add(paragraph);

                                        // Fecha o canvas
                                        canvas.Close();
                                    }

                                    // Salva o PDF no memory stream
                                    document.Close();

                                    // Salva o PDF processado em um arquivo físico
                                    var pdfBytes = memoryStream.ToArray();
                                    var fileProcessedPath = System.IO.Path.Combine(_webHostEnvironment.WebRootPath, _myConfigurationFiles["DocumentsSavedFolderName"], "documento_processado.pdf");
                                    await File.WriteAllBytesAsync(fileProcessedPath, pdfBytes);

                                    // Retorna o arquivo processado
                                    return new PhysicalFileResult(fileProcessedPath, "application/pdf");
                                }
                            }
                            catch (IOException ex)
                            {
                                // Joga uma exceção ->
                                throw new IOException($"Não foi possível editar o arquivo, erro: {ex}");
                            }
                        }
                    }
                }
            }
        }
    }
}
