namespace sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo
{
    public sealed class ConversorDeBytes
    {
        public static double ConverterParaMegaBytes(long valor)
        {
            // Divide o valor em bytes por 1.048.576 para obter megabytes
            return (double)valor / 1048576;
        }

        public static bool VerificarTamanhoMaximoArquivo(long tamanhoEmBytes)
        {
            // Converte o tamanho em bytes para megabytes
            double tamanhoEmMegaBytes = ConverterParaMegaBytes(tamanhoEmBytes);

            // Verifica se o tamanho do arquivo é superior a 15MB
            if (tamanhoEmMegaBytes > 15.0)
            {
                return true;
            }

            return false;
        }
    }
}
