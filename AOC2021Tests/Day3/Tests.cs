using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace AOC2021Tests.Day3.Tests
{
    [TestFixture]
    public class Tests
    {

        private IEnumerable<uint> GetTestValues()
        {
            yield return 0b00100;
            yield return 0b11110;
            yield return 0b10110;
            yield return 0b10111;
            yield return 0b10101;
            yield return 0b01111;
            yield return 0b00111;
            yield return 0b11100;
            yield return 0b10000;
            yield return 0b11001;
            yield return 0b00010;
            yield return 0b01010;
        }

        [Test]
        public void test_after_one_pass()
        {
            var sut = new AOC2021.Day3();

            var testVals = GetTestValues();

            var commonval = sut.GetCommonNumber(testVals, 5);

            Assert.IsTrue((commonval & (1 << 4)) > 0);


        }


        [TestCase(23)]
        public void test_that_oxygen_value_is_correct(int expected)
        {
            var sut = new AOC2021.Day3();

            var testVals = GetTestValues();

            var ret = sut.GetOxygenValue(testVals, 5);

            Assert.IsTrue(expected == ret);
        }


        [TestCase(10)]
        public void test_that_carbon_dioxide_value_is_correct(int expected)
        {
            var sut = new AOC2021.Day3();

            var testVals = GetTestValues();

            var ret  = sut.GetCO2Value(testVals, 5);

            Assert.IsTrue(expected == ret);
        }




    }
}