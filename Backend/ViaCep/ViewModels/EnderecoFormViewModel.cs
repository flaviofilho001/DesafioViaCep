using System.ComponentModel.DataAnnotations;

namespace ViaCep.ViewModels
{
    public class EnderecoFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP inválido.")]
        [Display(Name = "CEP")]
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
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve ter 2 caracteres.")]
        [Display(Name = "UF")]
        public string Uf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(20)]
        public string Numero { get; set; } = string.Empty;
    }
}
