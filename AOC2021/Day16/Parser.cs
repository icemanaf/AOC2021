using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021.Day16challenge
{
    public class Packet
    {
        public int Version { get; set; }

        public int Type { get; set; }

        public long LiteralValue { get; set; }

        public int BitLength { get; set; }

        public List<Packet> SubPackets { get; set; }

        public int GetVersionSum()
        {
            int sum = Version;
            if (SubPackets != null)
            {
                foreach (var item in SubPackets)
                {
                    sum += item.GetVersionSum();
                }
            }

            return sum;

        }


        public long GetValueSum()
        {

           long ret=0;

                switch (Type)
                {
                    case 4:
                        ret = LiteralValue;
                        break;
                    case 0:
                        foreach (var s in SubPackets)
                        {
                            ret = ret + s.GetValueSum();
                        }
                        break;
                    case 1:
                        ret = 1;
                        foreach (var s in SubPackets)
                        {
                            ret = ret * s.GetValueSum();

                            if (ret<0) throw new Exception("can't be negative");
                        }
                        break;
                    case 2:
                        ret = SubPackets.Min(x => x.GetValueSum());
                        break;
                    case 3:
                        ret = SubPackets.Max(x => x.GetValueSum());
                        break;
                    case 5:
                        ret =( SubPackets[0].GetValueSum() > SubPackets[1].GetValueSum() ? 1 : 0);
                        break;
                    case 6:
                        ret = (SubPackets[0].GetValueSum() < SubPackets[1].GetValueSum() ? 1 : 0);
                        break;
                    case 7:
                        ret = (SubPackets[0].GetValueSum() == SubPackets[1].GetValueSum() ? 1 : 0);
                        break;

                }

            return ret;
        }
    }


    public static class HelperExtensions
    {
        public static uint DequeNBits(this Queue<bool> queue, int n)
        {
            uint ret = 0;

            for (int i = 0; i < n; i++)
            {
                var temp = queue.Dequeue();

                ret = ret | (uint)(temp ? 1 : 0);

                if (i < n - 1)
                {
                    ret = ret << 1;
                }
            }

            return ret;
        }

        public static void EnqueBits(this Queue<bool> queue, char hexValue)
        {
            byte b = (byte)hexValue.HexToInt();

            for (int i = 0; i < 4; i++)
            {
                var temp = b >> 3 & 0b0001;
                queue.Enqueue(temp > 0);

                b = (byte)(b << 1);
            }

        }

        public static byte[] ToHexByteArray(this string s)
        {
            var cArray = s.ToCharArray();
            var buff = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                buff[i] = (byte)cArray[i].HexToInt();
            }

            return buff;
        }


        public static int HexToInt(this char ch)
        {
            if (ch >= '0' && ch <= '9')
                return ch - '0';
            if (ch >= 'A' && ch <= 'F')
                return ch - 'A' + 10;
            if (ch >= 'a' && ch <= 'f')
                return ch - 'a' + 10;
            return -1;
        }


    }


    public class Parser
    {


        public Packet Parse(Queue<bool> bitstream)
        {

            int packetVersion = 0;
            int packetType = 0;
            ulong lValue = 0;
            int subPacketLengthinBits = 0;
            int subPacketCount = 0;
            int packetLength = 0;

            if (bitstream.Count > 0)
            {


                // get the first  bit grouping
                var first = (int)bitstream.DequeNBits(4);

                if (true)
                {
                    packetVersion = first >> 1;

                    packetType = ((first & 0b0001) << 2) | (int)bitstream.DequeNBits(2);

                    if (packetType == 4)
                    {
                        var bitCounter = 6;//
                                           //literal get 5 bits at a time
                        while (true)
                        {
                            bitCounter += 5;

                            var temp = bitstream.DequeNBits(5);

                            lValue = (lValue << 4) | (temp & 0b1111);

                            if ((temp >> 4 & 0b1) == 0)
                            {
                                packetLength = bitCounter;
                                break;
                            }

                        }

                        return new Packet()
                        {
                            Type = packetType,
                            Version = packetVersion,
                            LiteralValue = (long)lValue,
                            BitLength = packetLength
                        };

                    }
                    else
                    {

                        var packet = new Packet()
                        {
                            Version = packetVersion,
                            Type = packetType,
                            SubPackets = new List<Packet>()
                        };

                        //operator packets
                        var lbit = bitstream.DequeNBits(1);
                        packetLength = 7;

                        if (lbit == 0)
                        {
                            subPacketLengthinBits = (int)bitstream.DequeNBits(15);
                            packetLength = packetLength + 15;

                            int l = bitstream.Count;

                            while ((l - bitstream.Count) < subPacketLengthinBits)
                            {
                                var s = Parse(bitstream);

                                packet.SubPackets.Add(s);
                            }

                        }
                        else
                        {
                            subPacketCount = (int)bitstream.DequeNBits(11);
                            var count = 0;
                            while (count < subPacketCount)
                            {
                                var s = Parse(bitstream);

                                packet.SubPackets.Add(s);

                                count++;
                            }
                        }

                        packet.BitLength = packetLength;

                        return packet;
                    }
                }


            }

            return null; //default if nothing is found
        }
    }
}