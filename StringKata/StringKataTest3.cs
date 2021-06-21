using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringKata.Test3
{
    [TestClass]
    public class StringKataTest3
    {
        [TestMethod]
        public void Add_EmptyString_Returns0()
        {
            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(0, calculator.Add(""));
        }


        [TestMethod]
        public void Add_SingleValue_ReturnsItself()
        {
            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(1, calculator.Add("1"));
        }

        [TestMethod]
        public void Add_2ValuesDelimited_ReturnsSum()
        {
            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(3, calculator.Add("1,2"));
        }

        [TestMethod]
        public void Add_MultipleValuesDelimited_ReturnsSum()
        {
            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(6, calculator.Add("1,2,3"));
        }


        [TestMethod]
        public void Add_MultipleValuesNewLineDelimited_ReturnsSum()
        {
            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(6, calculator.Add("1\n2,3"));
        }

        [TestMethod]
        public void Add_MultipleValuesNewDelimiter_ReturnsSum()
        {
            StringCalculator calculator = new StringCalculator();
            Assert.AreEqual(6, calculator.Add("//;\n1;2;3"));
        }

        [TestMethod]
        public void Add_MultipleValuesWithNegativeNumbers_ReturnsExceptionWithTheNegativeValues()
        {
            var ex = Assert.ThrowsException<Exception>(() => new StringCalculator().Add("//;\n1;-2;-3"));
            Assert.AreEqual(ex.Message, "negatives not allowed -2 -3");
        }
    }

    public class StringCalculator
    {
        private const string newDelimiterStartSymbol = "//";
        private const string newDelimiterEndSymbol = "\n";

        public int Add(string numbers)
        {
            if (IsStringEmpty(numbers))
                return 0;

            var delimiters = GetDelimitersFromString(numbers);
            numbers = RemoveDelimiterDataFrom(numbers);

            //var integerNumbers = numbers.Split(delimiters, StringSplitOptions.None).Select(stringNumber => int.Parse(stringNumber));
            var numberArrayStrings = SplitString(numbers, delimiters);
            var numberArray = ConvertStringArrayToIntArray(numberArrayStrings);

            CheckForNegatives(numberArray);

            return numberArray.Sum();
        }

        private void ThrowNegativeFoundException(List<int> Negatives)
        {
            throw new Exception("negatives not allowed " + string.Join(" ", Negatives));
        }

        private void CheckForNegatives(IEnumerable<Int32> numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0).ToList();

            if (negativeNumbers.Any())
            {
                ThrowNegativeFoundException(negativeNumbers);
            }
        }

        private bool IsStringEmpty(string numbers)
        {
            return string.IsNullOrEmpty(numbers);
        }

        private char[] GetDelimitersFromString(string numbers)
        {
            if (!DelimiterDataExistsIn(numbers))
                return new char[] { ',', '\n' };

            const int delimiterStart = 2;
            const int delimiterLength = 1;
            var delimiterString = numbers.Substring(delimiterStart, delimiterLength)[0];

            return new char[] { delimiterString };
        }

        private bool DelimiterDataExistsIn(string numbers)
        {
            return StringStartsWith(numbers, newDelimiterStartSymbol);
        }

        private string RemoveDelimiterDataFrom(string numbers)
        {
            if (StringStartsWith(numbers,newDelimiterStartSymbol))
            {
                int start = numbers.IndexOf(newDelimiterEndSymbol) + 1;
                return numbers.Substring(start);
            }

            return numbers;
        }

        private bool StringStartsWith(string InputString, string StringToLookFor)
        {
            return InputString.StartsWith(StringToLookFor);
        }

        private string[] SplitString(string UnSplitString, params char[] Delimiter)
        {
            string[] result = UnSplitString.Split(Delimiter);
            return result;
        }

        private int[] ConvertStringArrayToIntArray(string[] SplitStrings)
        {
            List<int> numberList = new List<int>();
            foreach (string splitString in SplitStrings)
            {
                numberList.Add(ConvertStringToInt(splitString));
            }

            return numberList.ToArray();
        }

        private int ConvertStringToInt(string Numbers)
        {
            int.TryParse(Numbers, out int result);
            return result;
        }

    }
}
