using System.Security.Cryptography;
using System.Text;
using ViaCep.Models;
using ViaCep.Repositories;

namespace ViaCep.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Usuario?> ValidarLoginAsync(string nomeUsuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(nomeUsuario) || string.IsNullOrWhiteSpace(senha))
                return null;

            var usuario = await _unitOfWork.Usuarios.GetByNomeUsuarioAsync(nomeUsuario);
            if (usuario is null)
                return null;

            // Se o hash no banco for a senha em texto plano (para dados mockados) ou hash SHA256
            var hashSenhaDigita = GerarHashSenha(senha);
            if (usuario.SenhaHash == senha || usuario.SenhaHash == hashSenhaDigita)
            {
                return usuario;
            }

            return null;
        }

        public static string GerarHashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }
    }
}
