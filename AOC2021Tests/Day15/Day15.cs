using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2021.Day15challenge;

namespace AOC2021Tests.Day15.Tests
{
    [TestFixture]
    public class Tests
    {

        private readonly int[,] arraySpace = new int[,]{{1,1,6,3,7,5,1,7,4,2},
                                                        {1,3,8,1,3,7,3,6,7,2},
                                                        {2,1,3,6,5,1,1,3,2,8},
                                                        {3,6,9,4,9,3,1,5,6,9},
                                                        {7,4,6,3,4,1,7,1,1,1},
                                                        {1,3,1,9,1,2,8,1,3,7},
                                                        {1,3,5,9,9,1,2,4,2,1},
                                                        {3,1,2,5,4,2,1,6,3,9},
                                                        {1,2,9,3,1,3,8,5,2,1},
                                                        {2,3,1,1,9,4,4,5,8,1}};

        [TestCase(9,8,8)]
        public void test_array_positions(int x ,int y ,int expected)
        {
            var val=arraySpace[x,y];       

            Assert.IsTrue(val==expected);
        }


        [Test]
        public void test_neigbhor_generation()
        {
            var djk=new DjikstraSearch(arraySpace);

            var n=djk.GetNeighbors(new Node(8,8,arraySpace[8,8]));

            Assert.IsTrue(n.Count()==4);
        }


        [TestCase(40)]
        public void test_djikstra_search(int expectedRisk)
        {
                var djk=new DjikstraSearch(arraySpace);

                var searchSet=djk.Search(0,0);
                
                var destNode=searchSet.First(n=>n.X==9 && n.Y==9);

                Assert.IsTrue(destNode.TotalRiskFromSource==expectedRisk);

        }


        [Test]
        public void test_expanded_dataset()
        {

        }

    }
}