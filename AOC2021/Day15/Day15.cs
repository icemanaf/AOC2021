using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Diagnostics;


namespace AOC2021.Day15challenge
{

    public record Node
    {
        public int X { get; }
        public int Y { get; }

        public int Risk { get; }

        public int TotalRiskFromSource { get; set; }

        public bool Visited { get; set; }

        public Node Parent { get; set; }
        public Node(int x, int y, int risk)
        {
            X = x;
            Y = y;
            Risk = risk;
            TotalRiskFromSource = int.MaxValue;
            Parent = null;
        }

        public override string ToString()
        {
            return $"{X}-{Y}";
        }
    }
    public class DjikstraSearch
    {

        private readonly int[,] _arraySpace;
        private readonly int _height;
        private readonly int _width;


        private readonly List<Node> _searchSet = new List<Node>();

        public DjikstraSearch(int[,] arraySpace)
        {
            _arraySpace = arraySpace;
            _height = arraySpace.GetLength(0);
            _width = arraySpace.GetLength(1);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var n = new Node(x, y, _arraySpace[x, y]);
                    _searchSet.Add(n);
                }
            }
        }

        public DjikstraSearch(List<Node> searchSet)
        {
            _searchSet = searchSet;

            _width = _searchSet.Max(n => n.X + 1);

            _height = _searchSet.Max(n => n.Y + 1);
        }




        public IEnumerable<Node> GetNeighbors(Node n)
        {

            if (n.X < 0 || n.Y < 0) throw new Exception("Invalid bounds.");

            if (n.X > _width || n.Y > _height) throw new Exception("Invalid bounds.");

            return _searchSet.Where(node =>
            {

                return ((node.Y < _height) && (node.X == n.X && node.Y == n.Y + 1)) ||

                 ((node.Y > 0) && (node.X == n.X && node.Y == n.Y - 1)) ||

                 ((n.X < _width) && (node.X == n.X + 1 && node.Y == n.Y)) ||

                 ((n.X > 0) && (node.X == n.X - 1 && node.Y == n.Y));

            });

        }


        private Node GetMinimumRiskNode(ref Dictionary<String, Node> dict)
        {

            // a priority queue would be faster here
            var n = dict.Where(x => !x.Value.Visited).OrderBy(x => x.Value.TotalRiskFromSource).FirstOrDefault().Value;

            return n;


            
        }


        public System.Collections.ObjectModel.ReadOnlyCollection<Node> Search(int x, int y,Node target=null)
        {
            
            var vstDict = new Dictionary<String, Node>();
            

            var src = _searchSet.First(n => n.X == x && n.Y == y);

            src.TotalRiskFromSource = 0;
            src.Visited = false;


            vstDict.Add(src.ToString(), src);

            while (vstDict.Any(x => !x.Value.Visited))
            {
                var current = GetMinimumRiskNode(ref vstDict);
                current.Visited=true;

                //vstDict.Add(current.ToString(), current);

                var neighbours = GetNeighbors(current);

                if (vstDict.Count%1000==0)
                    Console.WriteLine($"reached - {vstDict.Count}");

                if (target!=null)
                {
                    if (target.X==current.X && target.Y==current.Y)
                        break;

                }

                foreach (var node in neighbours)
                {

                    //make sure it's not an already visited node
                    if (!vstDict.ContainsKey(node.ToString()))
                    {
                        if (node.TotalRiskFromSource > current.TotalRiskFromSource + node.Risk)
                        {
                            node.TotalRiskFromSource = current.TotalRiskFromSource + node.Risk;
                            node.Parent = current;
                        }

                       vstDict.Add(node.ToString(),node);


                    }

                }
            }

            var readOnly = new System.Collections.ObjectModel.ReadOnlyCollection<Node>(vstDict.Values.ToList());

            return readOnly;

        }

    }



    public class Day15
    {
        public IEnumerable<Node> GetSearchSpaceFromFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Day15", "data.txt");
            var lines = File.ReadAllLines(path);
            var height = lines.Count();

            for (int y = 0; y < height; y++)
            {
                var cArray = lines[y];

                for (int x = 0; x < cArray.Length; x++)
                {
                    yield return new Node(x, y, cArray[x] - '0');

                }
            }
        }

        public IEnumerable<Node> GenerateLargerDataSet(IEnumerable<Node> initial)
        {
            for (int y=0;y<5;y++)
            {
                for(int j=0;j<5;j++ )
                {
                   foreach(var n in initial)
                   {
                         yield return new Node(n.X +100*j,n.Y+100*y,n.Risk);
                   }
                   
                }
            }
        }

        public void GetAnswer()
        {
            var data = GetSearchSpaceFromFile().ToList();
             var large=GenerateLargerDataSet(data).ToList();
            var djk = new DjikstraSearch(large);


            var sw=new Stopwatch();
            sw.Start();
            var ret = djk.Search(0, 0);
            sw.Stop();

            var dest = ret.First(n => n.X == 99 && n.Y == 99);
            Console.WriteLine($"Answer to Day 15 part 1 is {dest.TotalRiskFromSource} elapses time {sw.ElapsedMilliseconds}ms");

            

           
        }
    }
}


