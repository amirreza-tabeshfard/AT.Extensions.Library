namespace AT.Infrastructure;
public static class DataLength
{
    public static class Bytes
    {
        #region Field(s)

        public const Int32 B = 1;
        public const Int32 kB = 0x400 * B;
        public const Int32 MB = 0x400 * kB;
        public const Int32 GB = 0x400 * MB;
        public const Int64 TB = 1024L * GB;

        #endregion

        #region Method(s)

        public static String[] GetDataNames() => new[] { "B", "kB", "MB", "GB", "TB" };

        public static String[] GetDataNamesRu() => new[] { "Б", "кБ", "МБ", "ГБ", "ТБ" };

        #endregion
    }
}