using ViaCep.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* 
Coloquei o nomes dos campos bem explicito como é e como vai
ser, como SenhaHash e não Senha, ou NomeUsuario ao invés de 
usuario para não confundir com o nome da classe. 
*/

namespace ViaCep.Models
{
[Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        [StringLength(50)]
        public string NomeUsuario { get; set; } = string.Empty;        
        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        // Navegação: um usuário pode ter vários endereços
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}

