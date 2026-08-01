using ViaCep.Models;

namespace ViaCep.Services
{
    public interface IExportadorEnderecos
    {
        byte[] Exportar(List<Endereco> enderecos);

    }
}
