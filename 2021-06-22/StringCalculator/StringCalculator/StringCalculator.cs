using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers))
                return 0;

            var delimiters = GetDelimitersFromString(numbers);
            numbers = RemoveDelimiterDataFrom(numbers);

            var splitStrings = numbers.Split(delimiters.ToArray());
            var numberArray = splitStrings.Select(int.Parse).ToList();

            CheckForNegatives(numberArray);

            return numberArray.Sum();

        }

        private IEnumerable<char> GetDelimitersFromString(string numbers)
        {
            if (!numbers.StartsWith("//"))
                return new char[] { ',', '\n' };

            const int delimiterStart = 2;
            const int delimiterLength = 1;
            var delimiterString = numbers.Substring(delimiterStart, delimiterLength)[0];

            return new char[] { delimiterString };
        }

        private string RemoveDelimiterDataFrom(string numbers)
        {
            if (!numbers.StartsWith("//")) return numbers;
            int start = numbers.IndexOf("\n") + 1;
            return numbers.Substring(start);

        }

        private void CheckForNegatives(IEnumerable<int> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0).ToList();

            if (negativeNumbers.Any())
            {
                ThrowNegativeFoundException(negativeNumbers);
            }
        }

        private void ThrowNegativeFoundException(IEnumerable<int> Negatives)
        {
            throw new Exception("negatives not allowed " + string.Join(" ", Negatives));
        }

    }
}
