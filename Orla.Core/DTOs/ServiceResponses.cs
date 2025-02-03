namespace Orla.Core.DTOs;

public class ServiceResponses
{    
    public record class LoginResponse(bool Flag, string Token, string Message, string Name);
}
