using ViaCep.Models;

namespace ViaCep.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeUsuario { get; set; } 
        public string SenhaHash { get; set; } 
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}

