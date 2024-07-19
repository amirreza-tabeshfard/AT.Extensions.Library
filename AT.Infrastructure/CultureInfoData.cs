namespace AT.Infrastructure;
public static class CultureInfoData : Object
{
    public class CultureData
    {
        public List<String>? Articles { get; set; }
        public List<String>? Conjunctions { get; set; }
        public List<String>? Prepositions { get; set; }
    }

    public static (System.Globalization.CultureInfo? info, CultureData? data) InfoData;

    public static Boolean InitializeCultureData(System.Globalization.CultureInfo culture)
    {
        if (Equals(InfoData.info, culture))
            return true;

        Boolean succeeded = false;

        try
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            String[] names = assembly.GetManifestResourceNames();

            InfoData.info = default;
            InfoData.data = default;

            String? resourceName = names.FirstOrDefault(name => name.Contains($".{culture.Name}.json"));
            if (resourceName == default)
                resourceName = names.FirstOrDefault(name => name.Contains($".{culture.TwoLetterISOLanguageName}-"));

            if (!String.IsNullOrEmpty(resourceName))
            {
                String result = String.Empty;
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