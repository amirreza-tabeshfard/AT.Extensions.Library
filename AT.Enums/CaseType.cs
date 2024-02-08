namespace AT.Enums;
public enum CaseType
{
    /// <summary>
    /// Converts each character in the string to upper case.
    /// </summary>
    Upper,

    /// <summary>
    /// Converts each character in the string to lower case.
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
    /// Converts the string to title capitalization.
    /// </summary>
    Title,
}