using Microsoft.AspNetCore.Identity;

namespace Orla.Api.Models;

public class User : IdentityUser<long>
{
    public List<IdentityRole<long>>? Roles { get; set; }    
    public string? FullName { get; set; }    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
