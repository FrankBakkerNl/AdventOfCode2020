namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/3 </summary>
    public class Day03
    {
        /// <summary> Travel down a grid at an angle and count trees </summary>
        [Result(189)]
        public static int GetAnswer1(string[] input) => CountTrees(input, 1, 3);

        /// <summary> Travel down a grid at different angles and count trees </summary>
        [Result(1718180100)]
        public static int GetAnswer2(string[] input)
        {
            return
                CountTrees(input, 1, 1) *
                CountTrees(input, 1, 3) *
                CountTrees(input, 1, 5) *
                CountTrees(input, 1, 7) *
                CountTrees(input, 2, 1);
        }
        public static int CountTrees(string[] input, int ySlope, int xSlope)
        {
            var with = input[0].Length;
            var treeCount = 0;
            var x = 0;
            for (int y = 0; y < input.Length; y += ySlope, x += xSlope)
            {
                if (input[y][x % with] == '#') treeCount++;
            }

            return treeCount;
        }
    }
}
