namespace AT.Infrastructure;
public static class CultureInfoData : Object
{
    public class CultureData
    {
        public List<string>? Articles { get; set; }
        public List<string>? Conjunctions { get; set; }
        public List<string>? Prepositions { get; set; }
    }

    public static (System.Globalization.CultureInfo? info, CultureData? data) InfoData;

    public static bool InitializeCultureData(System.Globalization.CultureInfo culture)
    {
        if (Equals(InfoData.info, culture))
            return true;

        bool succeeded = false;

        try
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string[] names = assembly.GetManifestResourceNames();

            InfoData.info = default;
            InfoData.data = default;

            string? resourceName = names.FirstOrDefault(name => name.Contains($".{culture.Name}.json"));
            if (resourceName == null)
                resourceName = names.FirstOrDefault(name => name.Contains($".{culture.TwoLetterISOLanguageName}-"));

            if (!string.IsNullOrEmpty(resourceName))
            {
                string result = string.Empty;
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

                CultureData? data = Newtonsoft.Json.JsonConvert.DeserializeObject<CultureData>(result);
                if (data != null)
                {
                    InfoData.info = culture;
                    InfoData.data = data;
                    succeeded = true;
                }
            }
        }
        catch (Exception)
        {
            InfoData.info = default;
            InfoData.data = default;
            succeeded = false;
        }

        return succeeded;
    }
}