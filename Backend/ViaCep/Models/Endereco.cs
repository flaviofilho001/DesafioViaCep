using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViaCep.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9)]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(200)]
        public string Logradouro { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100)]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100)]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [StringLength(2)]
        public string Uf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(20)]
        public string Numero { get; set; } = string.Empty;

        // FK do usuário dono do endereço
        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; } = null!;
    }
}


/*

Retorno da API ViaCep para o CEP 01001-000:

URL: viacep.com.br/ws/01001000/json/

    {
      "cep": "01001-000",
      "logradouro": "Praça da Sé",
      "complemento": "lado ímpar",
      "unidade": "",
      "bairro": "Sé",
      "localidade": "São Paulo",
      "uf": "SP",
      "estado": "São Paulo",
      "regiao": "Sudeste",
      "ibge": "3550308",
      "gia": "1004",
      "ddd": "11",
      "siafi": "7107"
    }
         
*/