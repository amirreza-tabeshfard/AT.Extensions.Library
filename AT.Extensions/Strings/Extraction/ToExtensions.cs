using AT.Extensions.Strings.Conversion;

namespace AT.Extensions.Strings.Extraction;
public static class ToExtensions
{
    public static void ToFile(this String self, String file)
    {
        ArgumentException.ThrowIfNullOrEmpty(self);
        ArgumentException.ThrowIfNullOrEmpty(file);
        // ----------------------------------------------------------------------------------------------------
        File.WriteAllText(file, self);
    }

    public static void ToFile(this String self, FileInfo file)
    {
        ArgumentException.ThrowIfNullOrEmpty(self);
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        self.ToFile(file.FullName);
    }

    public static void ToMorseCodeSound(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32 freq = 500;
        Int32 timeUnitMs = 100;
        System.Diagnostics.Stopwatch sw = new();
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value.ToMorseCode(false))
        {
            if (c == '.')
                Console.Beep(freq, timeUnitMs);
            else if (c == '-')
                Console.Beep(freq, timeUnitMs * 3);
            // ----------------------------------------------------------------------------------------------------
            sw.Start();
            while (sw.ElapsedMilliseconds < timeUnitMs) { }
            sw.Stop();
            sw.Reset();
            // ----------------------------------------------------------------------------------------------------
            if (c == ' ')
            {
                sw.Start();
                while (sw.ElapsedMilliseconds < timeUnitMs * 7) { }
                sw.Stop();
                sw.Reset();
            }
        }
    }
}