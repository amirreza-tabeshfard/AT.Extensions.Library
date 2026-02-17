namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections.Generic;
public static class ForEachExtensions
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