using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021
{
    public class Day3
    {
        public IEnumerable<uint> ReadBitStrings()
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Day3", "Day3.txt");

            var lines = File.ReadAllLines(path);

            var b = Convert.ToUInt32("10000000", 2);

            foreach (var l in lines)
            {
                yield return Convert.ToUInt32(l, 2);
            }
        }


        public  uint GetCommonNumber(IEnumerable<uint> nums,int bitstringLength)
        {
            uint gamma = 0;

            int[] bitCounters = new int[12];

            var midCount = Math.Ceiling((decimal)( nums.Count() / 2.0d));

            uint b = 0;

            foreach (var x in nums)
            {
                b = 0b000000000001;

                for (int i = 0; i < bitstringLength; i++)
                {
                    if ((b & x) > 0)
                    {
                        bitCounters[i]++;
                    }

                    b = b << 1;
                }


            }


            b = 0b000000000001;

            for (var i = 0; i < bitstringLength; i++)
            {

                if (bitCounters[i] >= midCount)
                {
                    gamma = gamma | b;
                }

                b = b << 1;
            }


            return gamma;
        }

       

        public  IEnumerable<uint> SelectNumbersByBitPos(IEnumerable<uint> nums,int bitPos,bool bit)
        {
            uint bitmask = 0b000000000001;

            for(int i = 0; i < bitPos; i++)
            {
                bitmask = bitmask << 1;
            }

            foreach(var u in nums)
            {
                if (((u & bitmask)>0)== bit)
                    yield return u;
            }

        }

        public uint GetOxygenValue(IEnumerable<uint> nums,int bitlength)
        {

            int currentBitPos = bitlength-1;

            uint bitmask = (uint)1 << currentBitPos;

            uint commonNumber = 0;

            var filteredList = nums.ToList();

            while (currentBitPos >= 0)
            {
                commonNumber = GetCommonNumber(filteredList, bitlength);

                filteredList = SelectNumbersByBitPos(filteredList, currentBitPos, (bitmask & commonNumber) > 0).ToList();

                if (filteredList.Count == 1) break;

                currentBitPos--;

                bitmask = bitmask >> 1;
            }

            return filteredList[0];

        }

        public uint GetCO2Value(IEnumerable<uint> nums, int bitlength)
        {

            int currentBitPos = bitlength - 1;

            uint bitmask = (uint)1 << currentBitPos;

            uint commonNumber = 0;

            var filteredList = nums.ToList();

            while (currentBitPos >= 0)
            {
                commonNumber = GetCommonNumber(filteredList, bitlength);

                filteredList = SelectNumbersByBitPos(filteredList, currentBitPos, !((bitmask & commonNumber) > 0)).ToList();

                if (filteredList.Count == 1) break;

                currentBitPos--;

                bitmask = bitmask >> 1;
            }

            return filteredList[0];

        }



        public void GetAnswer()
        {
            //loop through each number and count up the bit positions that are set to 1.
            //for bit operations you need to use unsigned ints

            var nums = ReadBitStrings();

            var gamma = GetCommonNumber(nums, 12);

            var epsilon = (~gamma & 0b111111111111);

            Console.WriteLine($"Day 3 answer is { gamma * epsilon}");

            var o2Val = GetOxygenValue(nums, 12);
            var co2Val = GetCO2Value(nums, 12);

            Console.WriteLine($"Day 3 answer part 2 is {GetCO2Value(nums,12) *GetOxygenValue(nums,12)  } ");

           
        }


    }
}
