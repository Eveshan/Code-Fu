using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringKata
{
    [TestClass]
    public class StringKataTest2
    {
        [TestMethod]
        public void Add_EmptyString_Returns0()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(0, calculator.Add(""));
        }


        [TestMethod]
        public void Add_SingleValue_ReturnsItself()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(1, calculator.Add("1"));
        }

        [TestMethod]
        public void Add_2ValuesDelimited_ReturnsSum()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(3, calculator.Add("1,2"));
        }

        [TestMethod]
        public void Add_MultipleValuesDelimited_ReturnsSum()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(6, calculator.Add("1,2,3"));
        }


        [TestMethod]
        public void Add_MultipleValuesNewLineDelimited_ReturnsSum()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(6, calculator.Add("1\n2,3"));
        }

        [TestMethod]
        public void Add_MultipleValuesNewDelimiter_ReturnsSum()
        {
            Calculator calculator = new Calculator();
            Assert.AreEqual(6, calculator.Add("//;\n1;2;3"));
        }

        [TestMethod]
        public void Add_MultipleValuesWithNegativeNumbers_ReturnsExceptionWithTheNegativeValues()
        {
            var ex = Assert.ThrowsException<Exception>(() => new Calculator().Add("//;\n1;-2;-3"));
            Assert.AreEqual(ex.Message, "negatives not allowed -2 -3");
        }
    }

    public class Calculator
    {
        private char[] _delimiters = new[] { ',', '\n' };

        public int Add(string numbers)
        {
            int sum = 0;
            if (HasNewDelimiters(numbers))
            {
                UpdateDelimiters(numbers);
                numbers = GetDelimitedString(numbers);
            }
            
            string[] splitStrings = SplitString(numbers, _delimiters);
            int[] numberArray = ConvertStringArrayToIntArray(splitStrings);

            string negatives = FindNegativesInArray(numberArray);
            if(!IsStringEmpty(negatives))
                ThrowNegativeFoundException(negatives);

            sum = sumIntArray(numberArray);
            return sum;
        }

        private string FindNegativesInArray(int[] numberArray)
        {
            string result = "";
            if (numberArray.Any(n => n < 0))
            {
                foreach (int i in numberArray)
                {
                    if(i < 0)
                    result += " " + i.ToString();
                }
            }

            return result;
        }

        private void ThrowNegativeFoundException(string Negatives)
        {
            throw new Exception("negatives not allowed" + Negatives);
        }
        private string GetDelimitedString(string Numbers)
        {
            return Numbers.Substring(4);
        }

        private void UpdateDelimiters(string Numbers)
        {
            _delimiters = new[] { Numbers.Substring(2, 1)[0]};
        }

        private bool HasNewDelimiters(string Numbers)
        {
            if (StringStartsWith(Numbers, "//"))
                return true;
            else
                return false;
        }

        private int sumIntArray(int[] NumberArray)
        {
            return NumberArray.Sum();
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

        private string[] SplitString(string UnSplitString, params char[] Delimiter)
        {
            string[] result = UnSplitString.Split(Delimiter);
            return result;
        }


        private bool StringStartsWith(string InputString, string StringToLookFor)
        {
            return InputString.StartsWith(StringToLookFor);
        }

        private bool IsStringEmpty(string Numbers)
        {
            return string.IsNullOrEmpty(Numbers);
        }

        
    }
}
