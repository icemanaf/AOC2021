using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC2021.Day16challenge;

namespace AOC2021Tests.Day16.Tests
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void QueuingAndDequeingBits()
        {
            var queue = new Queue<bool>();

            queue.EnqueBits('D');

            var c = queue.DequeNBits(4);

            Assert.True(true);
        }

        [TestCase(1350)]
        public void test_that_packets_can_be_parsed(int expectedPacketCount)
        {
            var sut = new AOC2021.Day16challenge.Day16();

            var ret = sut.ReadHexFromFile();

            Assert.IsTrue(ret.Count() == expectedPacketCount);
        }


        [TestCase("D2F")]
        public void test_packet_parser(string input)
        {
            var parser = new AOC2021.Day16challenge.Parser();

            var queue = new Queue<bool>();

            var s = input.ToCharArray();

            foreach (var c in s)
            {
                queue.EnqueBits(c);
            }

            parser.Parse(queue);


        }

        [TestCase("8A004A801A8002F478")]
        public void test_parser_canparse_literal(string input)
        {
            var parser = new AOC2021.Day16challenge.Parser();

            var queue = new Queue<bool>();

            var s = input.ToCharArray();

            foreach (var c in s)
            {
                queue.EnqueBits(c);
            }

            var ret = parser.Parse(queue);

            Assert.IsTrue(ret != null); ;


        }



        [TestCase("8A004A801A8002F478", 16)]
        public void test_parser_check_version_sum(string input, int expected)
        {
            var parser = new AOC2021.Day16challenge.Parser();

            var queue = new Queue<bool>();

            var s = input.ToCharArray();

            foreach (var c in s)
            {
                queue.EnqueBits(c);
            }

            var ret = parser.Parse(queue);

            var vs = ret.GetVersionSum();

            Assert.IsTrue(vs == expected); ;


        }

        [TestCase("620080001611562C8802118E34", 12)]
        public void test_parser_check_version_sum2(string input, int expected)
        {
            var parser = new AOC2021.Day16challenge.Parser();

            var queue = new Queue<bool>();

            var s = input.ToCharArray();

            foreach (var c in s)
            {
                queue.EnqueBits(c);
            }

            var ret = parser.Parse(queue);

            var vs = ret.GetVersionSum();

            Assert.IsTrue(vs == expected); ;

        }


        [TestCase("A0016C880162017C3686B18A3D4780", 31)]
        public void test_parser_check_version_sum3(string input, int expected)
        {
            var parser = new AOC2021.Day16challenge.Parser();

            var queue = new Queue<bool>();

            var s = input.ToCharArray();

            foreach (var c in s)
            {
                queue.EnqueBits(c);
            }

            var ret = parser.Parse(queue);

            var vs = ret.GetVersionSum();

            Assert.IsTrue(vs == expected); ;

        }

        [TestCase("9C0141080250320F1802104A08", 1)]
        public void test_value_sum1(string input, int expected)
        {
            var parser = new AOC2021.Day16challenge.Parser();

            var queue = new Queue<bool>();

            var s = input.ToCharArray();

            foreach (var c in s)
            {
                queue.EnqueBits(c);
            }

            var ret = parser.Parse(queue);

            var vs = ret.GetValueSum();

            Assert.IsTrue(vs == expected); ;
        }

    }
}