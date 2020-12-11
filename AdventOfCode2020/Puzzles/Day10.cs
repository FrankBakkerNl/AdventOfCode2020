using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/10 </summary>
    public class Day10
    {
        [Result(1656)]
        public static int GetAnswer1(int[] input)
        {
            var inputSet = input.ToHashSet();

            var ones = 0;
            var threes = 0;

            foreach (var i in input.Append(0))
            {
                if (inputSet.Contains(i + 1)) ones++;
                else if (!inputSet.Contains(i + 2)) threes++;
            }

            return ones * threes;
        }

        [Result(56693912375296)]
        public static long GetAnswer2(int[] input)
        {
            var inputSet = input.Append(0).ToHashSet();

            var pathCounts = new long[input.Length  * 3 + 4 ];
            pathCounts[0] = 1;

            var skipped = 0;

            for (var i = 0; ; i++)
            {
                if (!inputSet.Contains(i))
                {
                    if (++skipped == 3) return pathCounts[i - skipped]; // If we skipped 3 times in a row we are passed the end
                    continue;
                }

                var paths = pathCounts[i];
                pathCounts[i + 1] += paths;
                pathCounts[i + 2] += paths;
                pathCounts[i + 3] += paths;

                skipped = 0;
            }
        }
    }
}