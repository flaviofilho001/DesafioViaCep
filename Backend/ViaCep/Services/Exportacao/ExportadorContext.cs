using ViaCep.Models;

namespace ViaCep.Services.Exportacao
{
    public interface IExportadorContext
    {
        IExportadorStrategy ObterEstrategia(string formato);
        byte[] Exportar(string formato, List<Endereco> enderecos, out string contentType, out string nomeArquivo);
    }

    public class ExportadorContext : IExportadorContext
    {
        private readonly IEnumerable<IExportadorStrategy> _estrategias;

        public ExportadorContext(IEnumerable<IExportadorStrategy> estrategias)
        {
            _estrategias = estrategias;
        }

        public IExportadorStrategy ObterEstrategia(string formato)
        {
            var formatoNormalizado = (formato ?? "csv").Trim().ToLowerInvariant();
            var estrategia = _estrategias.FirstOrDefault(e => e.Formato.Equals(formatoNormalizado, StringComparison.OrdinalIgnoreCase));

            if (estrategia is null)
            {
                // Fallback padrão para CSV se a estratégia não for encontrada
                return _estrategias.First(e => e.Formato == "csv");
            }

            return estrategia;
        }

        public byte[] Exportar(string formato, List<Endereco> enderecos, out string contentType, out string nomeArquivo)
        {
            var estrategia = ObterEstrategia(formato);
            contentType = estrategia.ContentType;
            nomeArquivo = estrategia.ObterNomeArquivo();
            return estrategia.Exportar(enderecos);
        }
    }
}
