using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021.Day16challenge
{
    public class Day16
    {

        public IEnumerable<char> ReadHexFromFile()
        {
            Char ch;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Day16", "data.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    ch = (char)sr.Read();

                    yield return ch;
                }
            }
        }

        public void GetAnswer()
        {

            var queue = new Queue<bool>();

            var clist = ReadHexFromFile();

            foreach (var c in clist)
                queue.EnqueBits(c);

            var parser = new Parser();

            var ret = parser.Parse(queue);

            Console.WriteLine($"Answer to Day 16 part 1 is {ret.GetVersionSum()}");

            Console.WriteLine($"Answer to Day 16 part 2 is {ret.GetValueSum()}");
        }
    }
}
