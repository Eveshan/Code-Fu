using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringKata.Test1
{
    [TestClass]
    public class StringKataTest1
    {
        private StringCalculator calculator;

        [TestInitialize]
        public void Init()
        {
            calculator = new StringCalculator();
        }

        [TestMethod]
        public void Add_EmptyInput_ReturnsZero()
        {
            int actual = calculator.Add("");
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void Add_SingleValue_ReturnsSameValue()
        {
            int actual = calculator.Add("1");
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Add_2Values_ReturnsSumOfValues()
        {
            int actual = calculator.Add("1,2");
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void Add_MultipleValues_ReturnsSumOfValues()
        {
            int actual = calculator.Add("1,2,3");
            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void Add_MultipleValuesWithNewLineDelimiter_ReturnsSumOfValues()
        {
            int actual = calculator.Add("1\n2,3");
            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void Add_MultipleValuesWithNewDelimiter_ReturnsSumOfValues()
        {
            int actual = calculator.Add("//;\n1;2;3");
            Assert.AreEqual(6, actual);
        }

        [TestMethod]
        public void Add_MultipleValuesWithNegativeNumbers_ReturnsExceptionWithTheNegativeValues()
        {
            var ex = Assert.ThrowsException<Exception>(() => calculator.Add("//;\n1;-2;-3"));
            Assert.AreEqual(ex.Message, "negatives not allowed -2 -3");
        }
    }

    public class StringCalculator
    {
        public int Add(string numbers)
        {
            int sum = 0;
            char[] delimiters = {',', '\n'};
            if (numbers.StartsWith("//"))
            {
                delimiters = new [] {numbers.Substring(2, 1).First()};
                numbers = numbers.Substring(4);
            }

            string[] splitNumbers = numbers.Split(delimiters);
            string negatives = "";
            foreach (var str in splitNumbers)
            {
                int.TryParse(str, out int number);
                if (number < 0)
                    negatives += " " + number.ToString();
                sum += number;
            }

            if (!string.IsNullOrEmpty(negatives))
            {
                throw new Exception("negatives not allowed" + negatives);
            }
            return sum;
        }


    }


}
