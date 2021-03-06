using System.Linq;

namespace SolStandardAPI.Utility;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? me)
    {
        return me is null || me.Length == 0;
    }

    public static bool IsNullOrWhitespace(this string? me)
    {
        return me.IsNullOrEmpty() || (me?.All(char.IsWhiteSpace) ?? false);
    }
}