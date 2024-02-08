namespace AT.Infrastructure;
public static class ExceptionMessages : Object
{
    public const string StringParamCannotBeNullOrEmpty_ParamName = "{0} can't be null or empty.";
    public const string ParamCannotBeLessThan_ParamName_MinValue_ActualValue = "{0} can't be less then {1}. Value was {2}.";
    public const string ParamAStringLengthCannotBeGreaterThanValueOfParamB_ParamAStrLen_ParamBValue = "{0} string length can't be greater than value of {1}.";
    public const string Base64UrlIllegalCharacter = "The input is not a valid Base-64 url string as it contains a non-base 64 url character, more than two padding characters, or an illegal character among the padding characters.";
    public const string HexStringHasIllegalCharacter = "The input is not a valid hex string as it contains a non-hex character.";
    public const string HexStringInvalidLength = "Invalid length for a hex char array or string.";
}