using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/3 </summary>
    public class Day03
    {
        [Result(189)]
        public static int GetAnswer1(string[] input) => CountTrees(new Map(input), 1, 3);

        public static int CountTrees(Map map, int ySlope, int xSlope)
        {
            int treeCount = 0;
            int x = 0;
            for (int y = 0; y < map.Rows; y+=ySlope)
            {
                if (map[y, x]) treeCount++;

                x += xSlope;
            }

            return treeCount;
        }

        [Result(1718180100)]
        public static int GetAnswer2(string[] input)
        {
            var map = new Map(input);
            return
                CountTrees(map, 1, 1) *
                CountTrees(map, 1, 3) *
                CountTrees(map, 1, 5) *
                CountTrees(map, 1, 7) *
                CountTrees(map, 2, 1);
        }

        public class Map
        {
            private bool[][] _field;

            public Map(string[] input)
            {
                _field = input.Select(l => l.Select(c => c == '#').ToArray()).ToArray();
            }

            public int Rows => _field.Length;

            public bool this[int y, int x] => _field[y % _field.Length][x % _field[0].Length];
        }
    }
}
