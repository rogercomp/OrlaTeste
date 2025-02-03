using System.ComponentModel.DataAnnotations;

namespace Orla.Core.Requests.Account;

public class RegisterRequest: Request
{
    [Required(ErrorMessage = "Nome inválido")]
    public string FullName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Documento inválido")]
    //[RegularExpression(@"^[0-9]+\s[a-zA-Z\s\-']{2,}.\s?[a-zA-Z\s\(\),]{2,}$", ErrorMessage = "Documento inválido")]
    public string Document { get; set; } = string.Empty;
    [Required(ErrorMessage = "Telefone inválido")]
    //[RegularExpression(@"^[0-9]+\s[a-zA-Z\s\-']{2,}.\s?[a-zA-Z\s\(\),]{2,}$", ErrorMessage = "Telefone inválido")]
    public string PhoneNumber { get; set; } = string.Empty;    
    public DateTime? BirthdayDate { get; set; }
    public DateTime? ExperiencyDate { get; set; }
    public string Perfil { get; set; } = string.Empty;    
    public byte[]? ImgURL { get; set; }    
    public string? CRMV { get; set; }
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha Inválida")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [DataType(DataType.Password)]
    [Display(Name = "Confirme a senha")]
    [Compare("Password", ErrorMessage = "Senhas não conferem")]
    public string? ConfirmPassword { get; set; }   

    public bool ReleaseConsultation { get; set; }
}
