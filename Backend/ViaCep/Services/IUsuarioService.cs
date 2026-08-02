using ViaCep.Models;

namespace ViaCep.Services
{
    public interface IUsuarioService
    {
        Task<Usuario?> ValidarLoginAsync(string nomeUsuario, string senha);
        Task<Usuario> RegistrarAsync(string nome, string nomeUsuario, string senha);
    }
}
