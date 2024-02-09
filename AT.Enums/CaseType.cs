namespace AT.Enums;
public enum CaseType
{
    /// <summary>
    /// Converts each character in the String to upper case.
    /// </summary>
    Upper,

    /// <summary>
    /// Converts each character in the String to lower case.
    /// </summary>
    Lower,

    /// <summary>
    /// Convert only the first character to upper case. Leave all other characters
    /// unchanged.
    /// </summary>
    CapitalizeFirstCharacter,

    /// <summary>
    /// Converts the first character of each sentence to upper case.
    /// </summary>
    Sentence,

    /// <summary>
    /// Converts the String to title capitalization.
    /// </summary>
    Title,
}