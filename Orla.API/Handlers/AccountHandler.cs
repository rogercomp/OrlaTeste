using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Orla.Api.Data;
using Orla.Api.Models;
using Orla.Core.DTOs;
using Orla.Core.Handlers;
using Orla.Core.Models;
using Orla.Core.Requests.Account;
using Orla.Core.Responses;
using static Orla.Core.DTOs.ServiceResponses;



namespace Orla.Api.Handlers;

public class AccountHandler(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config) : IAccountHandler
{

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        if (request == null) return new LoginResponse(false, null!, "Login vazio","");

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null) return new LoginResponse(false, null!, "Usuario e/ou senha inválido(s)!","");

        var singInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!singInResult.Succeeded) return new LoginResponse(false, null!, "Usuario e/ou senha inválido(s)!", "");

        var getUserRole = await userManager.GetRolesAsync(user);

        var userSession = new UserSession(user.Id.ToString(), user.UserName, getUserRole[0]);
        string token = GenerateToken(userSession);

        return new LoginResponse(true, token!, "Login efetuado", user.FullName!);
    }

    private string GenerateToken(UserSession user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userClaims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id == null ? "" : user.Id ),
                new Claim(ClaimTypes.Name, user.Name == null ? "" : user.Name),
                new Claim(ClaimTypes.Role, user.Role == null ? "" : user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<IdentityResult> CreateAsync(RegisterRequest request)
    {
        var newUser = new User
        {
            UserName = request.Email,
            Email = request.Email,
            PasswordHash = request.Password,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber           
        };

        var result = await userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return result;
        }

        var perfil = request.Perfil;

        var roleUser = await userManager.AddToRoleAsync(newUser, perfil);
               

        return result;
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    } 

    public async Task<Response<string>> ResetPasswordAsync(ResetPasswordRequest request)
    {
        StringBuilder sb = new StringBuilder();

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user != null)
        {
            var result = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (result.Succeeded)
            {
                return new Response<string>(null, 200, "Senha alterada com sucesso");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description);
                }
            }
        }
        return new Response<string>(null, 400, $"Não foi possível alterar a senha, erros: {sb.ToString()} ");
    }

    public async Task<Response<UserDTO?>> GetUserByIdAsync(GetUserByIdRequest request)
    {
        var user = await userManager.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id);

        if (user == null)
        {
            return new Response<UserDTO?>(null, 404, "Usuário não encontrado");
        }

        var userDTO = new UserDTO
        {            
            Id = user.Id,
            FullName = user.FullName
        };

        return new Response<UserDTO?>(userDTO, 200, "Usuário encontrado");
    }

    public async Task<Response<string?>> UpdateUserAsync(UserDTO request)
    {

        var user = await userManager.Users.FirstOrDefaultAsync(p => p.Id == request.Id);

        if (user == null)
        {
            return new Response<string?>(null, 404, "Usuário não encontrado");
        }

        try
        {
            user.FullName = request.FullName;            

            await userManager.UpdateAsync(user);

            return new Response<string?>(null, 200, "Usuário atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return new Response<string?>(null, 500, $"Erro ao atualizar informações Detalhes: {ex.Message}");
        }
        
    }
}
