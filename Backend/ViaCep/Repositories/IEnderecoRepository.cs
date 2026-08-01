using ViaCep.Models;

namespace ViaCep.Repositories
{
    public interface IEnderecoRepository
    {
        Task<List<Endereco>> GetAllByUsuarioAsync(int usuarioId);
        Task<Endereco?> GetByIdAsync(int id, int usuarioId);
        Task AddAsync(Endereco endereco);
        void Update(Endereco endereco);
        void Remove(Endereco endereco);
    }
}
