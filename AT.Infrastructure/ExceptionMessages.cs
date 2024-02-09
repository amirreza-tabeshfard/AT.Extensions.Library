namespace AT.Infrastructure;
public static class ExceptionMessages : Object
{
    public const String StringParamCannotBeNullOrEmpty_ParamName = "{0} can't be null or empty.";
    public const String ParamCannotBeLessThan_ParamName_MinValue_ActualValue = "{0} can't be less then {1}. Value was {2}.";
    public const String ParamAStringLengthCannotBeGreaterThanValueOfParamB_ParamAStrLen_ParamBValue = "{0} String length can't be greater than value of {1}.";
    public const String Base64UrlIllegalCharacter = "The input is not a valid Base-64 url String as it contains a non-base 64 url character, more than two padding characters, or an illegal character among the padding characters.";
    public const String HexStringHasIllegalCharacter = "The input is not a valid hex String as it contains a non-hex character.";
    public const String HexStringInvalidLength = "Invalid length for a hex char array or String.";
}