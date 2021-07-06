using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class StringCalculator
    {
        public static int Add(string numbers)
        {
         
            var delimiterList = GetDelimiterListFromString(numbers);
            var numberStringList = GetNumberStringList(numbers, delimiterList);
            var numberList = GetNumberList(numberStringList);

            CheckForNegatives(numberList);
            return SumNumberList(numberList);
        }

        private static List<string> GetDelimiterListFromString(string numbers)
        {
            List<string> delimiterList = new List<string>() { ",", "\n" };
            if (IsNotCustomDelimited(numbers)) return delimiterList;

            var customDelimiterLength = GetCustomDelimiterLength(numbers);
            var delimiterString = GetDelimiterString(numbers, customDelimiterLength);
            var customDelimiters = GetCustomDelimiters(delimiterString);
            delimiterList.AddRange(customDelimiters);
            return delimiterList;
        }

        private static List<string> GetNumberStringList(string numbers, List<string> delimiterList)
        {
            return numbers.Split(delimiterList.ToArray(), StringSplitOptions.None).ToList();
        }

        private static List<int> GetNumberList(List<string> numberStringList)
        {
            return numberStringList.Select(x => int.TryParse(x, out var e) ? e : 0).ToList();
        }
        
        private static void CheckForNegatives(List<int> numberList)
        {
            var negatives = numberList.Where(x => x < 0).ToList();
            if (negatives.Any())
                throw new Exception(GenerateNegativesNotAllowedMessage(negatives));
        }

        private static int SumNumberList(List<int> numberList)
        {
            var numberListWithoutLargeNumbers = GetNumbersUnder1000(numberList);
            return numberListWithoutLargeNumbers.Sum();
        }

        private static List<int> GetNumbersUnder1000(List<int> numberList)
        {
            return numberList.Where(x => x < 1000).ToList();
        }
        
        private static string GenerateNegativesNotAllowedMessage(List<int> negatives)
        {
            return "negatives not allowed " + string.Join(", ", negatives);
        }
        
        private static List<string> GetCustomDelimiters(string delimitedString)
        {
            return delimitedString.Split("[").Select(x => x.TrimEnd(']')).ToList();
        }

        private static string GetDelimiterString(string numbers, int customDelimiterLength)
        {
            return numbers.Substring(2, customDelimiterLength);
        }

        private static int GetCustomDelimiterLength(string numbers)
        {
            return numbers.IndexOf('\n') - 2;
        }
        
        private static bool IsNotCustomDelimited(string numbers)
        {
            return !numbers.StartsWith("//");
        }
    }
}
