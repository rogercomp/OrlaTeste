namespace Orla.Core;

public class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;

    public const string HttpClientName = "orla";

    public static string ConnectionString { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public static string ApiGoogleKey { get; set; } = string.Empty;    
  
    //Token 
    public static string Issuer { get; set; } = string.Empty;
    public static string Audience { get; set; } = string.Empty;
    public static string Key { get; set; } = string.Empty;
    public static string TokenExpireSeconds { get; set; } = string.Empty;
}
