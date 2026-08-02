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

            // Verifica com BCrypt
            try
            {
                if (BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
                {
                    return usuario;
                }
            }
            catch
            {
                // Fallback para texto plano se o registro foi inserido manualmente sem hash
                if (usuario.SenhaHash == senha)
                {
                    return usuario;
                }
            }

            return null;
        }

        public async Task<Usuario> RegistrarAsync(string nome, string nomeUsuario, string senha)
        {
            var usuarioExistente = await _unitOfWork.Usuarios.GetByNomeUsuarioAsync(nomeUsuario);
            if (usuarioExistente is not null)
            {
                throw new ArgumentException("Este nome de usuário já está em uso por outra conta.");
            }

            // Criptografa a senha com BCrypt (salting + hashing seguro)
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(senha);

            var novoUsuario = new Usuario
            {
                Nome = nome.Trim(),
                NomeUsuario = nomeUsuario.Trim(),
                SenhaHash = senhaCriptografada
            };

            await _unitOfWork.Usuarios.AddAsync(novoUsuario);
            await _unitOfWork.SaveChangesAsync();

            return novoUsuario;
        }
    }
}
