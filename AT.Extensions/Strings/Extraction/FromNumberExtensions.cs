namespace AT.Extensions.Strings.Extraction;
public static class FromNumberExtensions
{
    #region Field(s)

    private static readonly String NegativePrefix = "negative ";

    private static readonly String[] Ones =
    {
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    };

    private static readonly String[] Teens =
    {
        "ten",
        "eleven",
        "twelve",
        "thirteen",
        "fourteen",
        "fifteen",
        "sixteen",
        "seventeen",
        "eighteen",
        "nineteen"
    };

    private static readonly String[] Tens =
    {
        "",
        "ten",
        "twenty",
        "thirty",
        "forty",
        "fifty",
        "sixty",
        "seventy",
        "eighty",
        "ninety"
    };

    private static readonly String[] Thousands =
    {
        "",
        "thousand",
        "million",
        "billion",
        "trillion",
        "quadrillion",
        "quintillion",
        "sextillion",
        "septillion",
        "octillion",
    };

    #endregion

    #region Method(s): Private

    private static void FormatNumber(System.Text.StringBuilder builder, String digits)
    {
        String s;
        Boolean allZeros = true;

        for (Int32 i = digits.Length - 1; i >= 0; i--)
        {
            Int32 ndigit = digits[i] - '0';
            Int32 column = digits.Length - (i + 1);

            // Determine if ones, tens, or hundreds column
            switch (column % 3)
            {
                case 0:        // Ones position
                    Boolean showThousands = true;
                    if (i == 0)
                    {
                        // First digit in number (last in loop)
                        s = String.Format("{0} ", Ones[ndigit]);
                    }
                    else if (digits[i - 1] == '1')
                    {
                        // This digit is part of "teen" value
                        s = String.Format("{0} ", Teens[ndigit]);
                        // Skip tens position
                        i--;
                    }
                    else if (ndigit != 0)
                    {
                        // Any non-zero digit
                        s = String.Format("{0} ", Ones[ndigit]);
                    }
                    else
                    {
                        // This digit is zero. If digit in tens and hundreds
                        // column are also zero, don't show "thousands"
                        s = String.Empty;
                        // Test for non-zero digit in this grouping
                        if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                            showThousands = true;
                        else
                            showThousands = false;
                    }

                    // Show "thousands" if non-zero in grouping
                    if (showThousands)
                    {
                        if (column > 0)
                        {
                            s = String.Format("{0}{1}{2}",
                                s,
                                Thousands[column / 3],
                                allZeros ? " " : ", ");
                        }
                        // Indicate non-zero digit encountered
                        allZeros = false;
                    }
                    builder.Insert(0, s);
                    break;

                case 1:        // Tens column
                    if (ndigit > 0)
                    {
                        s = String.Format("{0}{1}",
                            Tens[ndigit],
                            (digits[i + 1] != '0') ? "-" : " ");
                        builder.Insert(0, s);
                    }
                    break;

                case 2:        // Hundreds column
                    if (ndigit > 0)
                    {
                        s = String.Format("{0} hundred ", Ones[ndigit]);
                        builder.Insert(0, s);
                    }
                    break;
            }
        }

        // Trim trailing space
        System.Diagnostics.Debug.Assert(builder.Length > 0 && builder[^1] == ' ');
        builder.Length--;
    }

    private static Int64 GetGreatestCommonDivisor(Int64 a, Int64 b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    private static String DecimalToFraction(decimal value)
    {
        if (value == 0m)
            return String.Empty;

        // Consider precision value to convert fractional part to integral equivalent
        Int64 pVal = 1000000000;

        // Calculate GCD of integral equivalent of fractional part and precision value
        Int64 gcd = GetGreatestCommonDivisor((Int64)Math.Round(value * pVal), pVal);

        // Calculate numerator and denominator
        Int64 numerator = (Int64)Math.Round(value * pVal) / gcd;
        Int64 denominator = pVal / gcd;

        return $"{numerator}/{denominator}";
    }

    #endregion

    public static String FromNumber(float value, Enums.DecimalFormat decimalFormat) => FromNumber((decimal)value, decimalFormat);

    public static String FromNumber(Double value, Enums.DecimalFormat decimalFormat) => FromNumber((decimal)value, decimalFormat);

    public static String FromNumber(decimal value, Enums.DecimalFormat decimalFormat)
    {
        Boolean isNegative = default;
        String? integerPart = default;
        Int32 i = default;
        decimal @decimal = default;
        // ----------------------------------------------------------------------------------------------------
        if (value < 0m)
        {
            value = Math.Abs(value);
            isNegative = true;
        }

        integerPart = value.ToString();
        i = integerPart.IndexOf('.');

        if (i >= 0)
            integerPart = integerPart[0..i];

        @decimal = value - Math.Truncate(value);

        System.Text.StringBuilder builder = new();
        FormatNumber(builder, integerPart);

        if (isNegative)
            builder.Insert(0, NegativePrefix);

        switch (decimalFormat)
        {
            case Enums.DecimalFormat.Currency:
                {
                    builder.AppendFormat(" and {0:00}/100", @decimal * 100);
                }
                break;

            case Enums.DecimalFormat.Fraction:
                {
                    if (@decimal != 0)
                    {
                        builder.Append(" and ");
                        builder.Append(DecimalToFraction(@decimal));
                    }
                }
                break;
        }
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    public static String FromNumber(Int32 value) => FromNumber((Int64)value);

    public static String FromNumber(Int64 value)
    {
        Boolean isNegative = default;
        System.Text.StringBuilder builder = default;
        // ----------------------------------------------------------------------------------------------------
        if (value < 0)
        {
            value = Math.Abs(value);
            isNegative = true;
        }
        // ----------------------------------------------------------------------------------------------------
        builder = new();
        FormatNumber(builder, value.ToString());
        // ----------------------------------------------------------------------------------------------------
        if (isNegative)
            builder.Insert(0, NegativePrefix);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    public static String FromNumber(Int32 value, out Boolean isNegative) => FromNumber((Int64)value, out isNegative);

    public static String FromNumber(Int64 value, out Boolean isNegative)
    {
        if (value < 0)
        {
            value = Math.Abs(value);
            isNegative = true;
        }
        else isNegative = false;
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new();
        FormatNumber(builder, value.ToString());
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }
}