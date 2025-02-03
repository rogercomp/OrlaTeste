using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Orla.Core.Common.Extensions;

public static class StringExtensions
{
    public static string SomenteCaracteres(this string value)
    {
        if (string.IsNullOrEmpty(value)) return value;

        string pattern = @"[-\.\(\)\s]";

        string result = Regex.Replace(value, pattern, string.Empty);

        return result;
    }

    public static IRuleBuilderOptions<T, string> MustBeDateTimeOfFormat<T>(this IRuleBuilder<T, string> ruleBuilder, string format)
    {
        return ruleBuilder.Must(x => DateTime.TryParseExact(x, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
    }
}
