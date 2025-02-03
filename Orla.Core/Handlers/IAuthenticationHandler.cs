using Microsoft.AspNetCore.Identity;
using Orla.Core.DTOs;
using Orla.Core.Requests.Account;
using Orla.Core.Responses;
using static Orla.Core.DTOs.ServiceResponses;

namespace Orla.Core.Handlers;

public interface IAuthenticationHandler
{
    Task<IdentityResult> CreateAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<Response<string>> ResetPasswordAsync(ResetPasswordRequest request);
    Task<Response<UserDTO?>> GetUserByIdAsync(GetUserByIdRequest request);
    Task<Response<string?>> UpdateUserAsync(UserDTO request);
}