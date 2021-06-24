using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            var delimiters = GetDelimitersFromString(numbers);
            numbers = RemoveDelimiterDataFromString(numbers);
            var newNumbersList = numbers.Split(delimiters.ToArray())
                                        .Select(ParseStringToInt).ToList();
            CheckForNegatives(newNumbersList);
            return newNumbersList.Sum();
        }

        private IEnumerable<char> GetDelimitersFromString(string numbers)
        {
            if (ContainsNewDelimiter(numbers))
            {
                char newDelimiter = numbers[2];
                return new List<char> { newDelimiter };
            }
            return new List<char> { ',', '\n' };
        }

        private bool ContainsNewDelimiter(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private string RemoveDelimiterDataFromString(string numbers)
        {
            if (ContainsNewDelimiter(numbers))
                return numbers.Substring(4);
            else
                return numbers;
        }

        private void CheckForNegatives(List<int> newNumbersList)
        {
            var negatives = newNumbersList.Where(n => n < 0).ToList();
            if (negatives.Any())
                throw new Exception("negatives not allowed " + string.Join(',', negatives));
        }

        private int ParseStringToInt(string number)
        {
            int.TryParse(number, out var result);
            return result;
        }
    }
}
