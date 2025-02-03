using System.IO.Compression;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using Orla.Core.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace Orla.Core.Models.Generics;

public static class Generics
{
    public static ClaimsPrincipal SetClaimPrincipal(UserSession model)
    {
        return new ClaimsPrincipal(new ClaimsIdentity(
            new List<Claim>()
            {
                    new(ClaimTypes.NameIdentifier, model.Id!),
                    new(ClaimTypes.Name, model.Name!),
                    new(ClaimTypes.Role, model.Role!)
            }, "JwtAuth"));
        ;
    }

    public static UserSession GetClaimsFromToken(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);
        var claims = token.Claims;

        string Id = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value!;
        string Name = claims.First(c => c.Type == ClaimTypes.Name).Value!;
        string Role = claims.First(c => c.Type == ClaimTypes.Role).Value!;

        return new UserSession(Id, Name, Role);
    }

    public static JsonSerializerOptions JsonOptions()
    {
        return new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
        };
    }

    public static Task WhenAll(this IEnumerable<ValueTask> tasks)
    {
        return Task.WhenAll(tasks.Select(v => v.AsTask()));
    }


    public static byte[] Compress(byte[] bytes)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
            {
                gzipStream.Write(bytes, 0, bytes.Length);
            }
            return memoryStream.ToArray();
        }
    }

    public static byte[] Decompress(byte[] bytes)
    {
        using (var memoryStream = new MemoryStream(bytes))
        {

            using (var outputStream = new MemoryStream())
            {
                using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    decompressStream.CopyTo(outputStream);
                }
                return outputStream.ToArray();
            }
        }
    }

    public static string SerializeObj<T>(T modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());
    public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
    public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;
    public static StringContent GenerateStringContent(string serialiazedObj) => new(serialiazedObj, System.Text.Encoding.UTF8, "application/json");
}
