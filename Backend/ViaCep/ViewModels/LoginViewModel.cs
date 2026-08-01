using System.ComponentModel.DataAnnotations;

namespace ViaCep.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário.")]
        [Display(Name = "Usuário")]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;
    }
}
