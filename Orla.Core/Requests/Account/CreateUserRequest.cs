using System.ComponentModel.DataAnnotations;

namespace Orla.Core.Requests.Account;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Nome inválido")]
    public string? FullName { get; set; } = string.Empty;
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha Inválida")]
    public string Password { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? Document { get; set; } = string.Empty;
    public DateTime? BirthdayDate { get; set; }
    public string? ImgURL { get; set; }
    public string? CRMV { get; set; }
    public string Perfil { get; set; } = string.Empty;
    public DateTime? ExperiencyDate { get; set; }
    public bool ReleaseConsultation { get; set; }    
}
