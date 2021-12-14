using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2021
{

    public struct Coord
    {
        public int X { get; }

        public int Y { get; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        //use this a potential key in dictionary
        public override string ToString()
        {
            return $"{X}-{Y}";
        }

    }

    public class Day13
    {

        private IEnumerable<Coord> GetInputData()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Day13", "coordinates.txt");
            var lines = File.ReadAllLines(path);
            return GetCoordsFromStringList(lines);
        }


        private IEnumerable<Coord> RunInstructionsOnInputData(IEnumerable<Coord> input)
        {
            var inputData = GetInputData();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Day13", "instructions.txt");

            var instructions = File.ReadAllLines(path);

            var ret = input.ToList();

            foreach (var instruction in instructions)
            {
                var ins = instruction.Split('=');

                if (ins[0] == "fold along x")
                {
                    int x = Convert.ToInt32(ins[1]);

                    ret = FoldAlongX(x, ret).ToList();
                }
                else if (ins[0] == "fold along y")
                {
                    int y = Convert.ToInt32(ins[1]);

                    ret = FoldAlongY(y, ret).ToList();
                }

            }

            return ret;

        }

        public void GetAnswer()
        {
            var coords = GetInputData();

            var firstfoldResults = FoldAlongX(655, coords);

            Console.WriteLine($"Answer for Day 13 part 1 is {firstfoldResults.Count()}");
    
            coords = RunInstructionsOnInputData(coords);

             Console.WriteLine($"Answer for Day 13 part 2");
    
            PrintAsciiArt(coords);

        }

        private void PrintAsciiArt(IEnumerable<Coord> input)
        {
            int max_x = input.Max(c => c.X);
            int max_y = input.Max(c => c.Y);

            Console.WriteLine();
            Console.WriteLine();

            //sort dataset by y from lowest to highest
            for (int y = 0; y <= max_y; y++)
            {
                for (int x = 0; x <= max_x; x++)
                {
                    var any=input.Any(c=>c.X==x && c.Y==y);

                    if (any)
                        Console.Write("*");
                    else{
                        Console.Write(" ");
                    }

                    if (x>=max_x)
                        Console.Write("\r\n");
                }
            }

        }


        //Each string element consists of the format x,y
        public IEnumerable<Coord> GetCoordsFromStringList(IEnumerable<String> stringInput)
        {
            return stringInput.Select(s =>
            {
                var coords = s.Split(',');
                int x = Convert.ToInt32(coords[0]);
                int y = Convert.ToInt32(coords[1]);
                return new Coord(x, y);
            });
        }

        //for each  coordinate below the fold axis y, rotate it by finding it's y displacement and negating it.
        public IEnumerable<Coord> FoldAlongY(int y, IEnumerable<Coord> input)
        {
            var rotated_coords = input.Select(coord =>
            {

                if (coord.Y < y)
                {
                    return coord;
                }
                else if (coord.Y > y)
                {
                    return new Coord(coord.X, coord.Y - 2 * (coord.Y - y));
                }
                else
                {
                    throw new Exception("Coordinate can't be on the fold line.");
                }


            });

            //eliminate dupes by adding and iterating a dictionary.
            var dict = new Dictionary<string, Coord>();

            foreach (var c in rotated_coords)
            {
                if (!dict.Keys.Contains(c.ToString()))
                {
                    dict.Add(c.ToString(), c);
                }
            }

            return dict.Values;


        }

        //for each  coordinate right of the fold axis x, rotate it by finding it's x displacement and negating it.
        public IEnumerable<Coord> FoldAlongX(int x, IEnumerable<Coord> input)
        {
            var rotated_coords = input.Select(coord =>
            {

                if (coord.X < x)
                {
                    return coord;
                }
                else if (coord.X > x)
                {
                    return new Coord(coord.X - 2 * (coord.X - x), coord.Y);
                }
                else
                {
                    throw new Exception("Coordinate can't be on the fold line.");
                }


            });

            //eliminate dupes by adding and iterating a dictionary.
            var dict = new Dictionary<string, Coord>();

            foreach (var c in rotated_coords)
            {
                if (!dict.Keys.Contains(c.ToString()))
                {
                    dict.Add(c.ToString(), c);
                }
            }

            return dict.Values;


        }


    }
}
