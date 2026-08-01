using ViaCep.Models;

namespace ViaCep.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByNomeUsuarioAsync(string nomeUsuario);
        Task AddAsync(Usuario usuario);
    }
}
