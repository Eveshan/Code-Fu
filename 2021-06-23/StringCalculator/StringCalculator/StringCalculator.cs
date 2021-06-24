using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if(string.IsNullOrEmpty(numbers))
                return 0;
            else
            {
                var delimiters = GetDelimitersFromString(numbers);
                numbers = RemoveDelimiterDataFromString(numbers);
                var newNumbersList = numbers.Split(delimiters.ToArray()).Select(int.Parse).ToList();
                CheckForNegatives(newNumbersList);
                return newNumbersList.Sum();
            }
        }

        private IEnumerable<char> GetDelimitersFromString(string numbers)
        {
            IEnumerable<char> delimiters = new List<char> {',', '\n'};
            if (ContainsNewDelimiter(numbers))
            {
                char newDelimiter = numbers[2];
                delimiters = new List<char> {newDelimiter};
            }

            return delimiters;
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
    }
}
