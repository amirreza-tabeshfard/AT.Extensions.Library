namespace AT.Infrastructure.RegularExpressionss;
public static class Patterns : Object
{
    // |     ==> Or
    // \d    ==> Digit

    // {n}   ==> n times
    // {n,m} ==> n to m times

    // a+    ==> One or more a
    // a*    ==> Zero or more a

    // $     ==> End of String
    // ^     ==> Start of String

    public const String BirthCertificate = @"\d{10}";

    public const String PhoneNumber = @"\d{8}";

    public const String CellPhoneNumber = @"\d{11}";

    public const String Culture = @"(\w{2})|(\w{2}-\w{2})";

    public const String ZipCod = @"\d{10}";

    public const String Double = @"[0-9.]{0,9}";

    public const String Email = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

    public const String FileName = @"[a-zA-Z0-9_]{1,100}";

    public const String Integer = @"(\+|-)?\d+";

    public const String IP = @"((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))";

    public const String Money = @"\d+(.\d{0,2})?";

    public const String NationalCode = @"\d{10}";

    public const String Password = @"[a-zA-Z0-9_]{8,40}";

    public const String Percentage = @"100(.0{0,2})?|\d{1,2}(.\d{1,2})?";

    public const String String = @"[a-zA-Z]{1, 100}";

    public const String PersianSting = @"^[ آ-ی]+$";

    public const String Url = @"^(https?)://(((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))|((([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])))(:[1-9][0-9]+)?(/)?([/?].+)?$";

    public const String Username = @"[a-zA-Z0-9_]{6,20}";

    public const String ZeroOrUnsignedInteger = @"\d+";

    public const String PhoneNumberCompany = @"[-]\d+";
}