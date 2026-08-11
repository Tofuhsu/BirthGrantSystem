using System.Text.RegularExpressions;

namespace BirthGrant.Shared.Helpers;

public static class TaiwanNationalIdValidator
{
    private static readonly IReadOnlyDictionary<char, int> LetterMap = new Dictionary<char, int>
    {
        ['A'] = 10, ['B'] = 11, ['C'] = 12, ['D'] = 13, ['E'] = 14,
        ['F'] = 15, ['G'] = 16, ['H'] = 17, ['I'] = 34, ['J'] = 18,
        ['K'] = 19, ['L'] = 20, ['M'] = 21, ['N'] = 22, ['O'] = 35,
        ['P'] = 23, ['Q'] = 24, ['R'] = 25, ['S'] = 26, ['T'] = 27,
        ['U'] = 28, ['V'] = 29, ['W'] = 30, ['X'] = 31, ['Y'] = 32,
        ['Z'] = 33
    };

    public static bool IsValid(string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return false;

        id = id.Trim().ToUpperInvariant();

        if (!Regex.IsMatch(id, @"^[A-Z][0-9]{9}$"))
            return false;

        if (!LetterMap.TryGetValue(id[0], out var code))
            return false;

        var sum = (code / 10) * 1 + (code % 10) * 9;

        for (var i = 1; i <= 8; i++)
        {
            sum += (id[i] - '0') * (9 - i);
        }

        sum += id[9] - '0';

        return sum % 10 == 0;
    }
}