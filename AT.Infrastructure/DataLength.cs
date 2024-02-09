namespace AT.Infrastructure;
public static class DataLength : Object
{
    public static class Bytes : Object
    {
        #region Field(s)

        public const int B = 1;
        public const int kB = 0x400 * B;
        public const int MB = 0x400 * kB;
        public const int GB = 0x400 * MB;
        public const long TB = 1024L * GB;

        #endregion

        #region Method(s)

        public static String[] GetDataNames() => new[] { "B", "kB", "MB", "GB", "TB" };

        public static String[] GetDataNamesRu() => new[] { "Б", "кБ", "МБ", "ГБ", "ТБ" };

        #endregion
    }
}