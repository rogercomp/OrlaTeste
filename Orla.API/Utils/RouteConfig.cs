namespace Orla.Api.Utils;

public class RouteConfig
{
    public static List<string> HiddenRoutes { get; private set; } = ["/auth/register", "/auth/confirmEmail", "/auth/resendConfirmationEmail", "/auth/manage/2fa", "/auth/manage/info"];
}
