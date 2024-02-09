using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Conversion;

namespace AT.Extensions.Strings.Extraction;
public static class VoidExtensions : Object
{
    public static void ForEach(this String self, Action<char> action)
    {
        foreach (char character in self)
            action(character);
    }

    public static void IfNotNull(this String? target, Action<String> continuation)
    {
        if (target != null)
        {
            continuation(target);
        }
    }

    public static void ReadLines(this String text, Action<String> callback)
    {
        var reader = new StringReader(text);
        String? line;
        while ((line = reader.ReadLine()) != null)
        {
            callback(line);
        }
    }

    public static void ThrowIfEmpty(this String input, String paramName)
    {
        if (input.Length == 0)
            throw new ArgumentException($"{paramName} cannot be empty", paramName);
    }

    internal static void ThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] this String? input, String paramName)
    {
        if (input.IsNull()) throw new ArgumentNullException(paramName, $"{paramName} cannot be null");
    }

    public static void ThrowIfNullEmptyOrWhitespace([System.Diagnostics.CodeAnalysis.NotNull] this String? input, String paramName)
    {
        input.ThrowIfNull(paramName);
        input.ThrowIfEmpty(paramName);
        input.ThrowIfWhitespace(paramName);
    }

    internal static void ThrowIfWhitespace(this String input, String paramName)
    {
        if (input.IsEmpty())
        {
            return;
        }
        if (input.IsWhitespace())
        {
            throw new ArgumentException($"{paramName} cannot be whitespace only", paramName);
        }
    }

    public static void ToFile(this String self, String file)
    {
        File.WriteAllText(file, self);
    }

    public static void ToFile(this String self, FileInfo file)
    {
        self.ToFile(file.FullName);
    }

    public static void ToMorseCodeSound(this String str)
    {
        int freq = 500;
        int timeUnitMs = 100;
        System.Diagnostics.Stopwatch sw = new();

        foreach (char c in str.ToMorseCode(false))
        {
            if (c == '.')
            {
                Console.Beep(freq, timeUnitMs);
            }
            else if (c == '-')
            {
                Console.Beep(freq, timeUnitMs * 3);
            }
            //Pause between two symbols (. or -) (1* timeUnitMs)
            sw.Start();
            while (sw.ElapsedMilliseconds < timeUnitMs) { }
            sw.Stop();
            sw.Reset();

            //Pause between two words (7* timeUnitMs)
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