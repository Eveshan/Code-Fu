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
            int actual = stringCalculator.Add("");
            //---------------Test Result -----------------------
            actual.Should().Be(0);
        }
        
        [TestCase("1", 1)]
        [TestCase("11", 11)]
        public void Add_WhenGivenNumber_ShouldReturnNumber(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }
        
        [TestCase("1,2,3", 6)]
        [TestCase("11,22,33", 66)]
        public void Add_WhenGivenDelimitedNumbers_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        
        [TestCase("1\n2\n3", 6)]
        [TestCase("11,22\n33", 66)]
        public void Add_WhenGivenNewLineDelimitedNumbers_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }
        
        [TestCase("//x\n1x2x3", 6)]
        [TestCase("//;\n11,22;33", 66)]
        public void Add_WhenGivenCustomDelimitedNumbers_ShouldReturnSum(string input, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(input);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }
        
        [TestCase("//x\n1x2x-3", "negatives not allowed -3")]
        [TestCase("//;\n11,-22;-33", "negatives not allowed -22, -33")]
        public void Add_WhenGivenNegativeNumbers_ShouldThrowException(string input, string expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = StringCalculatorBuilder();
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<Exception>(() => stringCalculator.Add(input));
            //---------------Test Result -----------------------
            ex.Message.Should().Be(expected);
        }


    
        private static StringCalculator StringCalculatorBuilder()
        {
            return new StringCalculator();
        }
    }
}
