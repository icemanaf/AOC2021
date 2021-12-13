using System;
using System.IO;

namespace AOC2021
{
    public struct Position
    {
        public int Depth { get; set; }
        public int HorzPos { get; set; }
        public int Aim { get; set; }
    }


    public class Submarine
    {
        private Position _pos;

        public Submarine(Position startingPosition)
        {
            _pos = startingPosition;
        }


        public Position GetPosition()
        {
            return _pos;
        }

        public void IssueCommand(string Command)
        {
            var parts = Command.Split(" ");

            int.TryParse(parts[1], out int val);

            switch (parts[0])
            {
                case "forward":
                    _pos.HorzPos += val;
                    _pos.Depth = _pos.Depth+ _pos.Aim * val;
                    break;

                case "down":
                    _pos.Aim += val;
                    break;

                case "up":
                    _pos.Aim -= val;
                    break;
            }



        }
    }

    public class Day2
    {
        public string[] GetCommands()
        {
            return File.ReadAllLines("Day2.txt");
        }


        public void GetAnswer()
        {
            var sub = new Submarine(new Position() { Depth = 0, HorzPos = 0,Aim=0 });

            var commands = GetCommands();

            foreach (var c in commands)
            {
                sub.IssueCommand(c);
            }

            var pos = sub.GetPosition();


            Console.WriteLine($"Answer for day2 is {pos.Depth * pos.HorzPos}");

        }
    }
}
