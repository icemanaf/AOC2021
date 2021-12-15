using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AOC2021Tests.Day14.Tests
{
    [TestFixture]
    public class Tests
    {
        Dictionary<string, string> _lookUp = new Dictionary<string, string>();


        [SetUp]
        public void SetUp()
        {

            _lookUp.Clear();
            _lookUp.Add("CH", "B");
            _lookUp.Add("HH", "N");
            _lookUp.Add("CB", "H");
            _lookUp.Add("NH", "C");
            _lookUp.Add("HB", "C");
            _lookUp.Add("HC", "B");
            _lookUp.Add("HN", "C");
            _lookUp.Add("NN", "C");
            _lookUp.Add("BH", "H");
            _lookUp.Add("NC", "B");
            _lookUp.Add("NB", "B");
            _lookUp.Add("BN", "B");
            _lookUp.Add("BB", "N");
            _lookUp.Add("BC", "B");
            _lookUp.Add("CC", "N");
            _lookUp.Add("CN", "C");
        }


        [TestCase("NNCB", 3)]
        public void test_pair_generation(string template, int expectedCount)
        {
            var sut = new AOC2021.Day14();

            var ret = sut.GetPairs(template);

            Assert.IsTrue(ret.Count() == expectedCount);
        }


        [TestCase("NNCB", "NCNBCHB")]
        [TestCase("NCNBCHB", "NBCCNBBBCBHCB")]
        [TestCase("NBCCNBBBCBHCB", "NBBBCNCCNBBNBNBBCHBHHBCHB")]
        public void test_polymer_generation(string template, string expectedPolymer)
        {
            var sut = new AOC2021.Day14();

            var polymer = sut.GeneratePolymer(template, _lookUp);

            Assert.IsTrue(polymer.Equals(expectedPolymer));
        }


        [TestCase("NC", "CB", "NCNBCHB")]
        public void test_whether_splitting_the_template_works(string template1, string template2, string expectedPolymer)
        {
            var sut = new AOC2021.Day14();

            var poly1 = sut.GeneratePolymer(template1, _lookUp);

            var poly2 = sut.GeneratePolymer(poly1, _lookUp);

            var poly3 = sut.GeneratePolymer(poly2, _lookUp);

            var poly4 = sut.GeneratePolymer(poly3, _lookUp);

            int a = (int)'A';
            int z = (int)'Z';
            var ret = poly1 + poly2;

            Assert.IsTrue(ret.Equals(expectedPolymer));
        }

        [Test]
        public void test_stats_dictionary()
        {
            var sut = new AOC2021.Day14();

            var stats = sut.GenerateEmptyStatsDictionary();

            Assert.IsTrue(stats['A'] == 0);
            Assert.IsTrue(stats['Z'] == 0);
        }


        


         [TestCase("NNCB", 10)]
        public void test_recursive_stats3(string template, int level)
        {
            var sut = new AOC2021.Day14();

            var cache=new Dictionary<String,Dictionary<char,long>>();

            var stats =sut.CalculateStats(template, level, ref _lookUp,ref cache);

            //add the last char 
            stats[template[template.Length-1]]++;

            var max = stats.OrderByDescending(x => x.Value).First();

            Assert.IsTrue(max.Value==1749);
        }


    }
}