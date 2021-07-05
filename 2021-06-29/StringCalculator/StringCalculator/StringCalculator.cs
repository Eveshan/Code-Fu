using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (numbers == "")
                return 0;

            var delimiterList = GetDelimiterList(numbers);
            var delimitedNumbers = GetDelimitedNumbers(numbers, delimiterList);
            var numberList = ConvertStringListToNumberList(delimitedNumbers);
            if (numberList.Any(x => x < 0))
                throw new Exception("negatives not allowed " + string.Join(", ", numberList.Where(x => x < 0)));

            return numberList.Sum();
        }

        private static List<string> GetDelimiterList(string numbers)
        {
            var delimiterList = new List<string> { ",", "\n" };

            if (!numbers.StartsWith("//")) return delimiterList;

            var delimiters = GetCustomDelimiters(numbers);

            delimiterList.Add(delimiters);
            return delimiterList;
        }

        private static string GetCustomDelimiters(string numbers)
        {
            var delimiters = numbers.Substring(2, 1);
            return delimiters;
        }

        private static List<int> ConvertStringListToNumberList(string[] delimitedNumbers)
        {
            var numberStringList = delimitedNumbers.Where(x => int.TryParse(x, out var e));
            var numberList = numberStringList.Select(int.Parse).ToList();
            return numberList;
        }

        private static string[] GetDelimitedNumbers(string numbers, List<string> delimiterList)
        {
            var delimitedNumbers = numbers.Split(delimiterList.ToArray(), StringSplitOptions.None);
            return delimitedNumbers;
        }
    }
}
