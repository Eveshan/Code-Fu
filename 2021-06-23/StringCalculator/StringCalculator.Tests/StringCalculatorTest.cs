using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Constraints;

namespace StringCalculator.Tests
{
    public class StringCalculatorTest
    {
        [Test]
        public void Add_WhenEmptyString_ShouldReturn0()
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add("");
            //---------------Test Result -----------------------
            actual.Should().Be(0);
        }

        [TestCase("1", 1)]
        [TestCase("22", 22)]
        [TestCase("333", 333)]
        public void Add_WhenGivenStringIntValue_ShouldReturnIntValue(string testData, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(testData);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("11,22,33", 66)]
        [TestCase("11,222,333", 566)]
        public void Add_WhenGivenCommaDelimitedString_ShouldReturnSum(string testData, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(testData);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("1\n2,3",6)]
        [TestCase("1\n2\n3", 6)]
        [TestCase("11\n22\n333", 366)]
        [TestCase("11,22\n333", 366)]
        public void Add_WhenGivenNewLineDelimitedString_ShouldReturnSum(string testData, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(testData);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("//;\n1;2;3", 6)]
        [TestCase("//;\n1;22;333", 356)]
        [TestCase("//;\n11;111;333;444", 899)]
        public void Add_WhenGivenNewDelimiter_ShouldReturnSum(string testData, int expected)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(testData);
            //---------------Test Result -----------------------
            actual.Should().Be(expected);
        }

        [TestCase("-1", "negatives not allowed -1")]
        [TestCase("-2,3,-4,-55", "negatives not allowed -2,-4,-55")]
        [TestCase("//;\n3;-22;-666", "negatives not allowed -22,-666")]
        public void Add_WhenGivenStringWithNegatives_ShouldThrowException(string testData, string exceptionMessage)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Execute Test ----------------------
            var ex = Assert.Throws<Exception>(() => stringCalculator.Add(testData));
            //---------------Test Result -----------------------
            ex.Should().NotBeNull();
            ex.Message.Should().Be(exceptionMessage);

        }
    }
}
