using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AOC2021Tests.Day13.Tests
{
    [TestFixture]
    public class Tests
    {


        //get test data from the Day13 example
        private IEnumerable<String> GetTestData()
        {
            yield return "6,10";
            yield return "0,14";
            yield return "9,10";
            yield return "0,3";
            yield return "10,4";
            yield return "4,11";
            yield return "6,0";
            yield return "6,12";
            yield return "4,1";
            yield return "0,13";
            yield return "10,12";
            yield return "3,4";
            yield return "3,0";
            yield return "8,4";
            yield return "1,10";
            yield return "2,14";
            yield return "8,10";
            yield return "9,0";
        }


        [Test]
        public void test_that_test_data_intput_to_coord_creatation_works()
        {
            var sut = new AOC2021.Day13();
            var input = GetTestData();
            var coords = sut.GetCoordsFromStringList(input);
            Assert.IsTrue(coords.Count() == 18);
        }

        [TestCase(17,7)]
        public void test_coords_left_after_folding_along_y(int expectedCoords, int y)
        {
            var sut = new AOC2021.Day13();
            var input = GetTestData();
            var coords=sut.GetCoordsFromStringList(input);
            var ret=sut.FoldAlongY(y,coords);

            Assert.IsTrue(ret.Count()==expectedCoords);

        }

        [TestCase(16,5)]
        public void test_coords_left_after_folding_along_x(int expectedCoords, int x)
        {
            var sut = new AOC2021.Day13();
            var input = GetTestData();
            var coords=sut.GetCoordsFromStringList(input);
            var ret=sut.FoldAlongX(x,coords);

            Assert.IsTrue(ret.Count()==expectedCoords);

        }

    }


}
