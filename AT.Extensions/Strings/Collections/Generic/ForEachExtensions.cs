namespace AT.Extensions.Strings.Collections.Generic;
public static class ForEachExtensions : Object
{
    public static IEnumerable<TFuncResult> ForEach<TFuncResult>(this String self, Func<Char, TFuncResult> function)
    {
        ArgumentException.ThrowIfNullOrEmpty(self);
        // ----------------------------------------------------------------------------------------------------
        IList<TFuncResult> items = new List<TFuncResult>();
        foreach (Char character in self)
        {
            TFuncResult? result = function(character);

            if (result is null)
                continue;

            items.Add(result);
        }
        // ----------------------------------------------------------------------------------------------------
        return items;
    }
}