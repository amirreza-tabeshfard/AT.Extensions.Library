namespace AT.Infrastructure;
public sealed class SystemTimeZone
{
    #region Field(s): Private

    private readonly String _windowsSystemTimeZoneName;

    #endregion

    #region Field(s): Public

    public static readonly SystemTimeZone DateLineStandardTime = new SystemTimeZone("Dateline Standard Time");
    public static readonly SystemTimeZone UTCMinusEleven = new SystemTimeZone("UTC-11");
    public static readonly SystemTimeZone HawaiianStandardTime = new SystemTimeZone("Hawaiian Standard Time");
    public static readonly SystemTimeZone AlaskanStandardTime = new SystemTimeZone("Alaskan Standard Time");
    public static readonly SystemTimeZone PacificStandardTimeMexico = new SystemTimeZone("Pacific Standard Time (Mexico)");
    public static readonly SystemTimeZone PacificStandardTime = new SystemTimeZone("Pacific Standard Time");
    public static readonly SystemTimeZone MountainStandardTimeUS = new SystemTimeZone("US Mountain Standard Time");
    public static readonly SystemTimeZone MountainStandardTimeMexico = new SystemTimeZone("Mountain Standard Time (Mexico)");
    public static readonly SystemTimeZone MountainStandardTime = new SystemTimeZone("Mountain Standard Time");
    public static readonly SystemTimeZone CentralAmericaStandardTime = new SystemTimeZone("Central America Standard Time");
    public static readonly SystemTimeZone CentralStandardTime = new SystemTimeZone("Central Standard Time");
    public static readonly SystemTimeZone CentralStandardTimeMexico = new SystemTimeZone("Central Standard Time (Mexico)");
    public static readonly SystemTimeZone CentralStandardTimeCanada = new SystemTimeZone("Canada Central Standard Time");
    public static readonly SystemTimeZone SAPacificStandardTime = new SystemTimeZone("SA Pacific Standard Time");
    public static readonly SystemTimeZone EasternStandardTime = new SystemTimeZone("Eastern Standard Time");
    public static readonly SystemTimeZone EasternStandardTimeUS = new SystemTimeZone("US Eastern Standard Time");
    public static readonly SystemTimeZone VenezuelaStandardTime = new SystemTimeZone("Venezuela Standard Time");
    public static readonly SystemTimeZone ParaguayStandardTime = new SystemTimeZone("Paraguay Standard Time");
    public static readonly SystemTimeZone AtlanticStandardTime = new SystemTimeZone("Atlantic Standard Time");
    public static readonly SystemTimeZone CentralBrazilianStandardTime = new SystemTimeZone("Central Brazilian Standard Time");
    public static readonly SystemTimeZone SAWesternStandardTime = new SystemTimeZone("SA Western Standard Time");
    public static readonly SystemTimeZone PacificSAStandardTime = new SystemTimeZone("Pacific SA Standard Time");
    public static readonly SystemTimeZone NewFoundLandStandardTime = new SystemTimeZone("Newfoundland Standard Time");
    public static readonly SystemTimeZone EastSouthAmericaStandardTime = new SystemTimeZone("E. South America Standard Time");
    public static readonly SystemTimeZone ArgentinaStandardTime = new SystemTimeZone("Argentina Standard Time");
    public static readonly SystemTimeZone SAEasternStandardTime = new SystemTimeZone("SA Eastern Standard Time");
    public static readonly SystemTimeZone GreenlandStandardTime = new SystemTimeZone("Greenland Standard Time");
    public static readonly SystemTimeZone MontevideoStandardTime = new SystemTimeZone("Montevideo Standard Time");
    public static readonly SystemTimeZone BahiaStandardTime = new SystemTimeZone("Bahia Standard Time");
    public static readonly SystemTimeZone UTCMinus2 = new SystemTimeZone("UTC-02");
    public static readonly SystemTimeZone MidAtlanticStandardTime = new SystemTimeZone("Mid-Atlantic Standard Time");
    public static readonly SystemTimeZone AzoresStandardTime = new SystemTimeZone("Azores Standard Time");
    public static readonly SystemTimeZone CapeVerdeStandardTime = new SystemTimeZone("Cape Verde Standard Time");
    public static readonly SystemTimeZone MoroccoStandardTime = new SystemTimeZone("Morocco Standard Time");
    public static readonly SystemTimeZone UTC = new SystemTimeZone("UTC");
    public static readonly SystemTimeZone GMTStandardTime = new SystemTimeZone("GMT Standard Time");
    public static readonly SystemTimeZone GreenwichStandardTime = new SystemTimeZone("Greenwich Standard Time");
    public static readonly SystemTimeZone WEuropeStandardTime = new SystemTimeZone("W. Europe Standard Time");
    public static readonly SystemTimeZone CentralEuropeStandardTime = new SystemTimeZone("Central Europe Standard Time");
    public static readonly SystemTimeZone RomanceStandardTime = new SystemTimeZone("Romance Standard Time");
    public static readonly SystemTimeZone CentralEuropeanStandardTime = new SystemTimeZone("Central European Standard Time");
    public static readonly SystemTimeZone WCentralAfricaStandardTime = new SystemTimeZone("W. Central Africa Standard Time");
    public static readonly SystemTimeZone NamibiaStandardTime = new SystemTimeZone("Namibia Standard Time");
    public static readonly SystemTimeZone JordanStandardTime = new SystemTimeZone("Jordan Standard Time");
    public static readonly SystemTimeZone GTBStandardTime = new SystemTimeZone("GTB Standard Time");
    public static readonly SystemTimeZone MiddleEastStandardTime = new SystemTimeZone("Middle East Standard Time");
    public static readonly SystemTimeZone EgyptStandardTime = new SystemTimeZone("Egypt Standard Time");
    public static readonly SystemTimeZone SyriaStandardTime = new SystemTimeZone("Syria Standard Time");
    public static readonly SystemTimeZone EEuropeStandardTime = new SystemTimeZone("E. Europe Standard Time");
    public static readonly SystemTimeZone SouthAfricaStandardTime = new SystemTimeZone("South Africa Standard Time");
    public static readonly SystemTimeZone FLEStandardTime = new SystemTimeZone("FLE Standard Time");
    public static readonly SystemTimeZone TurkeyStandardTime = new SystemTimeZone("Turkey Standard Time");
    public static readonly SystemTimeZone IsraelStandardTime = new SystemTimeZone("Israel Standard Time");
    public static readonly SystemTimeZone KaliningradStandardTime = new SystemTimeZone("Kaliningrad Standard Time");
    public static readonly SystemTimeZone LibyaStandardTime = new SystemTimeZone("Libya Standard Time");
    public static readonly SystemTimeZone ArabicStandardTime = new SystemTimeZone("Arabic Standard Time");
    public static readonly SystemTimeZone ArabStandardTime = new SystemTimeZone("Arab Standard Time");
    public static readonly SystemTimeZone BelarusStandardTime = new SystemTimeZone("Belarus Standard Time");
    public static readonly SystemTimeZone RussianStandardTime = new SystemTimeZone("Russian Standard Time");
    public static readonly SystemTimeZone EAfricaStandardTime = new SystemTimeZone("E. Africa Standard Time");
    public static readonly SystemTimeZone IranStandardTime = new SystemTimeZone("Iran Standard Time");
    public static readonly SystemTimeZone ArabianStandardTime = new SystemTimeZone("Arabian Standard Time");
    public static readonly SystemTimeZone AzerbaijanStandardTime = new SystemTimeZone("Azerbaijan Standard Time");
    public static readonly SystemTimeZone RussiaTimeZone3 = new SystemTimeZone("Russia Time Zone 3");
    public static readonly SystemTimeZone MauritiusStandardTime = new SystemTimeZone("Mauritius Standard Time");
    public static readonly SystemTimeZone GeorgianStandardTime = new SystemTimeZone("Georgian Standard Time");
    public static readonly SystemTimeZone CaucasusStandardTime = new SystemTimeZone("Caucasus Standard Time");
    public static readonly SystemTimeZone AfghanistanStandardTime = new SystemTimeZone("Afghanistan Standard Time");
    public static readonly SystemTimeZone WestAsiaStandardTime = new SystemTimeZone("West Asia Standard Time");
    public static readonly SystemTimeZone EkaterinburgStandardTime = new SystemTimeZone("Ekaterinburg Standard Time");
    public static readonly SystemTimeZone PakistanStandardTime = new SystemTimeZone("Pakistan Standard Time");
    public static readonly SystemTimeZone IndiaStandardTime = new SystemTimeZone("India Standard Time");
    public static readonly SystemTimeZone SriLankaStandardTime = new SystemTimeZone("Sri Lanka Standard Time");
    public static readonly SystemTimeZone NepalStandardTime = new SystemTimeZone("Nepal Standard Time");
    public static readonly SystemTimeZone CentralAsiaStandardTime = new SystemTimeZone("Central Asia Standard Time");
    public static readonly SystemTimeZone BangladeshStandardTime = new SystemTimeZone("Bangladesh Standard Time");
    public static readonly SystemTimeZone NCentralAsiaStandardTime = new SystemTimeZone("N. Central Asia Standard Time");
    public static readonly SystemTimeZone MyanmarStandardTime = new SystemTimeZone("Myanmar Standard Time");
    public static readonly SystemTimeZone SEAsiaStandardTime = new SystemTimeZone("SE Asia Standard Time");
    public static readonly SystemTimeZone NorthAsiaStandardTime = new SystemTimeZone("North Asia Standard Time");
    public static readonly SystemTimeZone ChinaStandardTime = new SystemTimeZone("China Standard Time");
    public static readonly SystemTimeZone NorthAsiaEastStandardTime = new SystemTimeZone("North Asia East Standard Time");
    public static readonly SystemTimeZone SingaporeStandardTime = new SystemTimeZone("Singapore Standard Time");
    public static readonly SystemTimeZone WAustraliaStandardTime = new SystemTimeZone("W. Australia Standard Time");
    public static readonly SystemTimeZone TaipeiStandardTime = new SystemTimeZone("Taipei Standard Time");
    public static readonly SystemTimeZone UlaanbaatarStandardTime = new SystemTimeZone("");
    public static readonly SystemTimeZone TokyoStandardTime = new SystemTimeZone("Tokyo Standard Time");
    public static readonly SystemTimeZone KoreaStandardTime = new SystemTimeZone("Korea Standard Time");
    public static readonly SystemTimeZone YakutskStandardTime = new SystemTimeZone("Yakutsk Standard Time");
    public static readonly SystemTimeZone CenAustraliaStandardTime = new SystemTimeZone("Cen. Australia Standard Time");
    public static readonly SystemTimeZone AUSCentralStandardTime = new SystemTimeZone("AUS Central Standard Time");
    public static readonly SystemTimeZone EAustraliaStandardTime = new SystemTimeZone("E. Australia Standard Time");
    public static readonly SystemTimeZone AUSEasternStandardTime = new SystemTimeZone("");
    public static readonly SystemTimeZone WestPacificStandardTime = new SystemTimeZone("West Pacific Standard Time");
    public static readonly SystemTimeZone TasmaniaStandardTime = new SystemTimeZone("Tasmania Standard Time");
    public static readonly SystemTimeZone MagadanStandardTime = new SystemTimeZone("Magadan Standard Time");
    public static readonly SystemTimeZone VladivostokStandardTime = new SystemTimeZone("Vladivostok Standard Time");
    public static readonly SystemTimeZone RussiaTimeZone10 = new SystemTimeZone("Russia Time Zone 10");
    public static readonly SystemTimeZone CentralPacificStandardTime = new SystemTimeZone("Central Pacific Standard Time");
    public static readonly SystemTimeZone RussiaTimeZone11 = new SystemTimeZone("RussiaTimeZone11");
    public static readonly SystemTimeZone NewZealandStandardTime = new SystemTimeZone("New Zealand Standard Time");
    public static readonly SystemTimeZone UTCPlus12 = new SystemTimeZone("UTC+12");
    public static readonly SystemTimeZone FijiStandardTime = new SystemTimeZone("Fiji Standard Time");
    public static readonly SystemTimeZone KamchatkaStandardTime = new SystemTimeZone("Kamchatka Standard Time");
    public static readonly SystemTimeZone TongaStandardTime = new SystemTimeZone("Tonga Standard Time");
    public static readonly SystemTimeZone SamoaStandardTime = new SystemTimeZone("Samoa Standard Time");
    public static readonly SystemTimeZone LineIslandsStandardTime = new SystemTimeZone("Line Islands Standard Time");

    #endregion

    #region Constructor

    private SystemTimeZone(String windowsSystemTimeZoneName)
    {
        _windowsSystemTimeZoneName = windowsSystemTimeZoneName;
    }

    #endregion

    public override String ToString()
    {
        return _windowsSystemTimeZoneName;
    }
}