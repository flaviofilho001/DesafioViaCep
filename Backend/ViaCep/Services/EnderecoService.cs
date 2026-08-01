using ViaCep.Exceptions;
using ViaCep.Models;
using ViaCep.Repositories;

namespace ViaCep.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnderecoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Endereco>> ListarPorUsuarioAsync(int usuarioId)
        {
            return await _unitOfWork.Enderecos.GetAllByUsuarioAsync(usuarioId);
        }

        public async Task<Endereco?> ObterPorIdAsync(int id, int usuarioId)
        {
            var endereco = await _unitOfWork.Enderecos.GetByIdAsync(id, usuarioId);
            if (endereco is null)
            {
                throw new NotFoundException("Endereço não encontrado.");
            }

            return endereco;
        }

        public async Task<Endereco> AdicionarAsync(Endereco endereco)
        {
            await _unitOfWork.Enderecos.AddAsync(endereco);
            await _unitOfWork.SaveChangesAsync();
            return endereco;
        }

        public async Task AtualizarAsync(Endereco endereco)
        {
            var existente = await _unitOfWork.Enderecos.GetByIdAsync(endereco.Id, endereco.UsuarioId);
            if (existente is null)
            {
                throw new NotFoundException("Endereço não encontrado para atualização.");
            }

            existente.Cep = endereco.Cep;
            existente.Logradouro = endereco.Logradouro;
            existente.Complemento = endereco.Complemento;
            existente.Bairro = endereco.Bairro;
            existente.Cidade = endereco.Cidade;
            existente.Uf = endereco.Uf;
            existente.Numero = endereco.Numero;

            _unitOfWork.Enderecos.Update(existente);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id, int usuarioId)
        {
            var existente = await _unitOfWork.Enderecos.GetByIdAsync(id, usuarioId);
            if (existente is null)
            {
                throw new NotFoundException("Endereço não encontrado para remoção.");
            }

            _unitOfWork.Enderecos.Remove(existente);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
