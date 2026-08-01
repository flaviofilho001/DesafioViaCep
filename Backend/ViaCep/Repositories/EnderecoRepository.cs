using Microsoft.EntityFrameworkCore;
using ViaCep.Data;
using ViaCep.Models;

namespace ViaCep.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ApplicationDbContext _context;

        public EnderecoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Endereco>> GetAllByUsuarioAsync(int usuarioId)
        {
            return await _context.Enderecos
                .Where(e => e.UsuarioId == usuarioId)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

        public async Task<Endereco?> GetByIdAsync(int id, int usuarioId)
        {
            return await _context.Enderecos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == usuarioId);
        }

        public async Task AddAsync(Endereco endereco)
        {
            await _context.Enderecos.AddAsync(endereco);
        }

        public void Update(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
        }

        public void Remove(Endereco endereco)
        {
            _context.Enderecos.Remove(endereco);
        }
    }
}
