using System.ComponentModel.DataAnnotations;

namespace Orla.Core.Requests.Account;

public class ForgotPasswordRequest: Request
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;
}
