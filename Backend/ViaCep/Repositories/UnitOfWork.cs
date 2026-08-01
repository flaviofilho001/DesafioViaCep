using ViaCep.Data;

namespace ViaCep.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IUsuarioRepository? _usuarios;
        private IEnderecoRepository? _enderecos;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUsuarioRepository Usuarios => 
            _usuarios ??= new UsuarioRepository(_context);

        public IEnderecoRepository Enderecos => 
            _enderecos ??= new EnderecoRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
