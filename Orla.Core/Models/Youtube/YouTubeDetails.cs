using System.ComponentModel.DataAnnotations;

namespace Orla.Core.Models.Youtube;

public class YouTubeDetail
{
    public string Id { get; set; } = string.Empty;
    [Required(ErrorMessage = "Título inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 200 caracteres")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Link inválido")]
    [MaxLength(80, ErrorMessage = "O título deve conter até 200 caracteres")]
    public string? Link { get; set; }    
    public string? Thumbnail { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
}
