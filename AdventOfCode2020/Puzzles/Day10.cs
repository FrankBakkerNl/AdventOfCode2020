using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/10 </summary>
    public class Day10
    {
        [Result(1656L)]
        public static long GetAnswer1(int[] input)
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
            var target = input.Max() + 3;
            var inputSet = input.ToHashSet();
            inputSet.Add(target);
            inputSet.Add(0);

            var pathCounts = new long[target+3];
            pathCounts[0] = 1;

            for (int i = 0; i < target; i++)
            {
                if (!inputSet.Contains(i)) continue;

                // see how many paths lead to this node and add that to all possible next nodes
                var count = pathCounts[i];
                pathCounts[i + 1] += count;
                pathCounts[i + 2] += count;
                pathCounts[i + 3] += count;
            }

            return pathCounts[target];
        }
    }
}