using ViaCep.Models;

namespace ViaCep.Services
{
    public interface IEnderecoService
    {
        Task<List<Endereco>> ListarPorUsuarioAsync(int usuarioId);
        Task<Endereco?> ObterPorIdAsync(int id, int usuarioId);
        Task<Endereco> AdicionarAsync(Endereco endereco);
        Task AtualizarAsync(Endereco endereco);
        Task ExcluirAsync(int id, int usuarioId);
    }
}
