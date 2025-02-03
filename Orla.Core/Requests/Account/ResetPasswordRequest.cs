using System.ComponentModel.DataAnnotations;

namespace Orla.Core.Requests.Account;

public class ResetPasswordRequest: Request
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha inválida")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmar Senha")]
    [Compare("Password", ErrorMessage = "Senha e contrasenha não coincidem")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Token inválido")]
    public string Token { get; set; } = string.Empty;
}
