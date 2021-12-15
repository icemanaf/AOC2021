using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2021
{
    public class Day14
    {

        private const string template = "BNSOSBBKPCSCPKPOPNNK";
        //gets the pairs from the string template




        public IEnumerable<String> GetPairs(string template)
        {
            int length = template.Length;

            //if (length % 2 > 0) throw new Exception("Invalid template length");

            for (int i = 0; i < length; i++)
            {
                if (i > 0)
                {
                    yield return $"{template[i - 1]}{template[i]}";
                }
            }
        }

        private Dictionary<String, String> LoadLookupFromFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Day14", "data.txt");
            var lines = File.ReadAllLines(path);

            var dict = new Dictionary<String, String>();

            foreach (var line in lines)
            {
                var cArray = line.ToCharArray();
                var key = new String(new char[] { cArray[0], cArray[1] });
                var val = new String(new char[] { cArray[6] });

                dict.Add(key, val);
            }



            return dict;
        }

        public string GeneratePolymer(string template, Dictionary<String, String> lookup)
        {
            var pairs = GetPairs(template);

            var triples = pairs.Select(pair =>
            {

                var cArray = new char[3];
                cArray[0] = pair[0];
                cArray[1] = lookup[pair][0];
                cArray[2] = pair[1];
                return new String(cArray);
            });

            return triples.Aggregate((acc, val) =>
            {

                var s = acc + val.Substring(1);

                return s;
            });
        }

        /*
            To solve this you need to use a recursive approach with dynamic programming. you will 
            need to use caching as the time complexity is too high.
            From the first example, search to a depth of 2
            Initial String NNCB
                        Break this into 3 strings
                                NN              NC          CB
                                |               |           |
            First Pass         NCN             NBC         CHB
                                |               |           |       
            Leaf level (2)  NBC   CCN       NBB   BBC    CBH  HCB
                            ++-   ++-       ++-   ++-    ++-  ++-
            Only count up the chars that are denoted with a + underneath them.
            Add the count of the last char of the template , which is 'B'
        */

        public Dictionary<char, long> CalculateStats(string template, int level, ref Dictionary<String, String> lookUp, ref Dictionary<string, Dictionary<char, long>> cache)
        {
            var cArray = template.ToCharArray();

            var stats = GenerateEmptyStatsDictionary();

            if (level == 0)
            {
                for (int i = 0; i < cArray.Length - 1; i++)
                {
                    stats[cArray[i]]++;
                }

                return stats;
            }


            for (int i = 0; i < cArray.Length - 1; i++)
            {
                Dictionary<char, long> stemp = null;

                var s = new String(new char[] { cArray[i], cArray[i + 1] });

                var key = $"{s}-{level}";

                var p = GeneratePolymer(s, lookUp);

                if (cache.ContainsKey(key))
                {
                    stemp = cache[key];
                }
                else
                {
                    stemp = CalculateStats(p, level - 1, ref lookUp, ref cache);
                    cache.Add(key, stemp);
                }



                foreach (char c in stemp.Keys)
                {
                    if (stats.ContainsKey(c))
                    {
                        stats[c] = stats[c] + stemp[c];
                    }
                }
            }

            return stats;
        }



        public Dictionary<char, long> GenerateEmptyStatsDictionary()
        {
            var dict = new Dictionary<char, long>();

            for (var i = 65; i < 91; i++)
            {
                dict.Add((char)i, 0);
            }

            return dict;
        }

        public void GetAnswer()
        {
            var dict = LoadLookupFromFile();

            var cache = new Dictionary<string, Dictionary<char,long>>();

           var stats= CalculateStats(template, 40,  ref dict, ref cache);

            //add back the last char of the template string.
            stats[template[template.Length - 1]]++;

            var max = stats.OrderByDescending(x => x.Value).Where(y=>y.Value>0).First();

            var min = stats.OrderBy(x => x.Value).Where(y=>y.Value>0).First();

            Console.WriteLine($"Answer for Day 14 part 2 is {max.Value - min.Value}");


        }
    }


}

