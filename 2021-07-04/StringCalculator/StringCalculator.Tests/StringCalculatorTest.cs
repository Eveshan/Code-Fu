using System;
using NUnit.Framework;
using FluentAssertions;

namespace StringCalculator.Tests
{
    public class StringCalculatorTest
    {
        [Test]
        public void Add_WhenGivenEmptyString_ShouldReturn0()
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add("");
            //---------------Test Result -----------------------
            actual.Should().Be(0);
        }

        [TestCase("1", 1)]
        [TestCase("22", 22)]
        [TestCase("222", 222)]
        public void Add_WhenGivenNumber_ShouldReturnNumber(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            int actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("1,1", 2)]
        [TestCase("1,1,3", 5)]
        [TestCase("22,33,44", 99)]
        [TestCase("222,333,111", 666)]
        public void Add_WhenGivenCommaDelimitedNumber_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }
        
        [TestCase("1\n1", 2)]
        [TestCase("1,1\n3", 5)]
        [TestCase("22\n33\n44", 99)]
        [TestCase("222\n333,111", 666)]
        public void Add_WhenGivenNewLineDelimitedNumber_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("//x\n1x1", 2)]
        [TestCase("//;\n1,1;3", 5)]
        [TestCase("//.\n22,33\n11.11", 77)]
        public void Add_WhenGivenCustomDelimiter_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("-1", "negatives not allowed -1")]
        [TestCase("//x\n1x-1", "negatives not allowed -1")]
        [TestCase("//;\n1,-1;-3", "negatives not allowed -1, -3")]
        [TestCase("//.\n-22,-33\n-11.-11", "negatives not allowed -22, -33, -11, -11")]
        public void Add_WhenGivenNegatives_ShouldThrowException(string input, string expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = Assert.Throws<Exception>(() => StringCalculator.Add(input));
            //---------------Test Result -----------------------
            actual.Message.Should().Be(expected);
        }

        [TestCase("//x\n1x1000", 1)]
        [TestCase("//;\n1005,1;3", 4)]
        [TestCase("//.\n2002,33\n11.11", 55)]
        public void Add_WhenGivenNumbers_ShouldReturnSumIgnoringGreaterThan1000(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("//[xx]\n1xx100", 101)]
        [TestCase("//[;;;]\n10,1;;;3", 14)]
        [TestCase("//[...]\n1,33\n11...11", 56)]
        public void Add_WhenGivenNumbersWithCustomerDelimiterOfAnyLength_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("//[xx][aa]\n1xx100aa2", 103)]
        [TestCase("//[;;]['']\n10,1;;3''4", 18)]
        [TestCase("//[..][o]\n1,33\n11..11oo11", 67)]
        [TestCase("//[..][ooo]\n1,33\n11..11ooo11", 67)]
        public void Add_WhenGivenNumbersWithCustomerMultipleDelimitersOfAnyLength_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var actual = StringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        private static StringCalculator StringCalculatorBuilder()
        {
            return new StringCalculator();
        }
    }
}
