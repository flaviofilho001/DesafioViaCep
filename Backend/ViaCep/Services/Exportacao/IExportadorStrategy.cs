using ViaCep.Models;

namespace ViaCep.Services.Exportacao
{
    public interface IExportadorStrategy
    {
        string Formato { get; }
        string ContentType { get; }
        string ObterNomeArquivo(string prefixo = "enderecos");
        byte[] Exportar(List<Endereco> enderecos);
    }
}
