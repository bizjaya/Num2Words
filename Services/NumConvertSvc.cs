using System.Numerics;
using System.Text.RegularExpressions;

namespace Num2Words.Services;


//https://www.britannica.com/topic/large-numbers-1765137


public interface INumConvSvc
{
    Task<string> ConvertAsync(string input);
}

public class NumConvSvc : INumConvSvc
{
    public async Task<string> ConvertAsync(string input)
    {
        // simple regexto check if its fungible USD currency
        var currencyPattern = @"(\$|USD|dollars|USDT)";
        var numberPattern = @"[\d,]+(?:\.\d+)?";

        var currencyMatch = Regex.Match(input, currencyPattern, RegexOptions.IgnoreCase);
        var numberMatch = Regex.Match(input, numberPattern, RegexOptions.IgnoreCase);

        if (!numberMatch.Success)
        {
            throw new ArgumentException("Invalid input format.");
        }

        var numberPart = numberMatch.Value.Replace(",", "");

        // ensure its a vlaid int/ decimal
        if (numberPart.Count(c => c == '.') > 1)
        {
            throw new ArgumentException("Invalid input format.");
        }

        var parts = numberPart.Split('.');
        var integerPart = BigInteger.Parse(parts[0]);
        var fractionalPart = parts.Length > 1 ? parts[1] : "";

        if (currencyMatch.Success)
        {
            // if currency then limit to 2 decimal places
            var formattedFractionalPart = fractionalPart.PadRight(2, '0').Substring(0, 2); 
            var fractionalValue = BigInteger.Parse(formattedFractionalPart);
            return CurrencyToWords(integerPart, fractionalValue);
        }
        else
        {
            // No currency detected, return number with dynamic decimal places
            return NumberToWords(integerPart) + (fractionalPart.Length > 0 ? " POINT " + FractionalPartToWords(fractionalPart) : "");
        }
    }

    private string NumberToWords(BigInteger number)
    {
        if (number == 0) return "ZERO";
        if (number < 0) return "MINUS " + NumberToWords(BigInteger.Abs(number));

        var words = "";

        if ((number / BigInteger.Pow(10, 303)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 303)) + " CENTILLION ";
            number %= BigInteger.Pow(10, 303);
        }

        if ((number / BigInteger.Pow(10, 100)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 100)) + " GOOGOL ";
            number %= BigInteger.Pow(10, 100);
        }

        if ((number / BigInteger.Pow(10, 84)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 84)) + " QUATTUORDECILLION ";
            number %= BigInteger.Pow(10, 84);
        }

        if ((number / BigInteger.Pow(10, 45)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 45)) + " QUATTUORDECILLION ";
            number %= BigInteger.Pow(10, 45);
        }

        if ((number / BigInteger.Pow(10, 42)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 42)) + " TREDECILLION ";
            number %= BigInteger.Pow(10, 42);
        }

        if ((number / BigInteger.Pow(10, 39)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 39)) + " DUODECILLION ";
            number %= BigInteger.Pow(10, 39);
        }

        if ((number / BigInteger.Pow(10, 36)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 36)) + " UNDECILLION ";
            number %= BigInteger.Pow(10, 36);
        }

        if ((number / BigInteger.Pow(10, 33)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 33)) + " DECILLION ";
            number %= BigInteger.Pow(10, 33);
        }

        if ((number / BigInteger.Pow(10, 30)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 30)) + " NONILLION ";
            number %= BigInteger.Pow(10, 30);
        }

        if ((number / BigInteger.Pow(10, 27)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 27)) + " OCTILLION ";
            number %= BigInteger.Pow(10, 27);
        }

        if ((number / BigInteger.Pow(10, 24)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 24)) + " SEPTILLION ";
            number %= BigInteger.Pow(10, 24);
        }

        if ((number / BigInteger.Pow(10, 21)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 21)) + " SEXTILLION ";
            number %= BigInteger.Pow(10, 21);
        }

        if ((number / BigInteger.Pow(10, 18)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 18)) + " QUINTILLION ";
            number %= BigInteger.Pow(10, 18);
        }

        if ((number / BigInteger.Pow(10, 15)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 15)) + " QUADRILLION ";
            number %= BigInteger.Pow(10, 15);
        }

        if ((number / BigInteger.Pow(10, 12)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 12)) + " TRILLION ";
            number %= BigInteger.Pow(10, 12);
        }

        if ((number / BigInteger.Pow(10, 9)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 9)) + " BILLION ";
            number %= BigInteger.Pow(10, 9);
        }

        if ((number / BigInteger.Pow(10, 6)) > 0)
        {
            words += NumberToWords(number / BigInteger.Pow(10, 6)) + " MILLION ";
            number %= BigInteger.Pow(10, 6);
        }

        if ((number / 1_000) > 0)
        {
            words += NumberToWords(number / 1_000) + " THOUSAND ";
            number %= 1_000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " HUNDRED ";
            number %= 100;
        }

        var unitsMap = new[]
        {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE",
            "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN",
            "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };

        var tensMap = new[]
        {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };

        if (number > 0)
        {
            if (words != "")
                words += "AND ";

            if (number < 20)
                words += unitsMap[(int)number];
            else
            {
                words += tensMap[(int)(number / 10)];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[(int)(number % 10)];
            }
        }

        return words.Trim();
    }

    private string FractionalPartToWords(string fractionalPart)
    {
        var words = "";
        foreach (var digit in fractionalPart)
        {
            words += NumberToWords(BigInteger.Parse(digit.ToString())) + " ";
        }
        return words.Trim();
    }

    private string CurrencyToWords(BigInteger integerPart, BigInteger fractionalPart)
    {
        var words = NumberToWords(integerPart) + " DOLLARS";

        if (fractionalPart > 0)
        {
            // Cent part
            var fractionalWords = NumberToWords(fractionalPart);
            words += $" AND {fractionalWords} CENTS";
        }

        return words;
    }
}