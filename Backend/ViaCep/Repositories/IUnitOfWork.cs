namespace ViaCep.Repositories
{
    public interface IUnitOfWork
    {
        IUsuarioRepository Usuarios { get; }
        IEnderecoRepository Enderecos { get; }
        Task<int> SaveChangesAsync();
    }
}
