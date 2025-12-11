namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for char values.
/// </summary>
public static class CharExtensions
{
    /// <summary>
    /// Checks if a character is a vowel.
    /// </summary>
    public static bool IsVowel(this char character)
    {
        return "aeiouAEIOU".IndexOf(character) >= 0;
    }

    /// <summary>
    /// Checks if a character is a consonant.
    /// </summary>
    public static bool IsConsonant(this char character)
    {
        return char.IsLetter(character) && !character.IsVowel();
    }
}
