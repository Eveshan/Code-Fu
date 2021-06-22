using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using FluentAssertions;

namespace StringCalculator.Tests
{
    public class UnitTest1
    {
        [Test]
        public void Add_WhenGivenEmptyString_ShouldReturn0()
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add("");
            //---------------Test Result -----------------------
            actual.Should().Be(0);
        }

        [Test]
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        [TestCase("3", 3)]
        public void Add_WhenGivenSingleValue_ShouldReturnItself(string numbers, int expectedResult)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(numbers);
            //---------------Test Result -----------------------
            actual.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("3,4", 7)]
        public void Add_WhenGivenTwoValuesDelimited_ShouldReturnSum(string numbers, int expectedResult)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(numbers);
            //---------------Test Result -----------------------
            actual.Should().Be(expectedResult);
        }
        
        [Test]
        [TestCase("1,2,3", 6)]
        [TestCase("2,3,4", 9)]
        [TestCase("3,4,5", 12)]
        public void Add_WhenGivenThreeValuesDelimited_ShouldReturnSum(string numbers, int expectedResult)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(numbers);
            //---------------Test Result -----------------------
            actual.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("1,2,3,4", 10)]
        [TestCase("2,3,4,5,6", 20)]
        [TestCase("3,4,5,6,7,8", 33)]
        public void Add_WhenGivenMultipleValuesDelimited_ShouldReturnSum(string numbers, int expectedResult)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(numbers);
            //---------------Test Result -----------------------
            actual.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("1\n2,3", 6)]
        public void Add_WhenUsingNewLineDelimiter_ShouldReturnSum(string numbers, int expectedResult)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(numbers);
            //---------------Test Result -----------------------
            actual.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("//;\n1;2;3", 6)]
        public void Add_WhenUsingNewDelimiter_ReturnsSum(string numbers, int expectedResult)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            int actual = stringCalculator.Add(numbers);
            //---------------Test Result -----------------------
            actual.Should().Be(expectedResult);
        }

        [Test]
        [TestCase("//;\n1;-2;-3", "negatives not allowed -2 -3")]
        public void Add_WhenUsingNewDelimiterWithNegatives_ThrowException(string numbers, string expectedException)
        {
            //---------------Set up test pack-------------------
            StringCalculator stringCalculator = new StringCalculator();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<Exception>(() => stringCalculator.Add(numbers));
            //---------------Test Result -----------------------
            ex.Should().NotBeNull();
            ex.Message.Should().Be(expectedException);
        }
    }
}
