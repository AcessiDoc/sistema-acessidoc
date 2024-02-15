using Humanizer;
using iText.Forms.Form.Element;
using iText.IO.Font;
using iText.Kernel.Font;

namespace sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo
{
    public sealed class ABNTConfiguration
    {
        // Configuração das margens do arquivo PDF
        public static float ABNT_MARGIN_TOP { get; } = 3 * 28.35f;
        public static float ABNT_MARGIN_LEFT { get; } = 3 * 28.35f;
        public static float ABNT_MARGIN_RIGHT { get; } = 2 * 28.35f;
        public static float ABNT_MARGIN_BOTTOM { get; } = 2 * 28.35f;

        // Configuração do tamanho da fonte
        public static int ABNT_FONT_SIZE_STANDARD { get; } = 18;
        public static int ABNT_FONT_SIZE_ENLARGED { get; } = 24;

        //Configuração do espaçamento do texto
        public static float ABNT_LINE_SPACING { get; } = 1.5f;
        public static float ABNT_PARAGRAPH_INDENT { get; } = 1.25f * 28.35f;
    }
}