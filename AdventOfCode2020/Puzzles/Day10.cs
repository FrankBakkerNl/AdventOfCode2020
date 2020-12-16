using System;
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
            var adapterSet = input.ToHashSet();

            Span<long> pathCounts = stackalloc long[] {1L, 1L, 1L}; 
            // Ring buffer for 3 path counts we need to track
            // Because we start at 0 we know there is at least 1 path to values 1, 2, and 3

            var lastValue = 0;
            var numPaths = 1L;

            // Loop all possible adapter values from 1 until we missed 3 values in a row which must be the end
            for (var i = 1; i <= lastValue + 3 ; i++)
            {
                if (!adapterSet.Contains(i))
                {
                    // No adapter for this value, so no paths lead here 
                    pathCounts[i % 3] = 0;
                    continue;
                }
                lastValue = i;

                numPaths = pathCounts[i % 3];

                // Increment number of paths to the next two values with the current number of paths to this adapter
                pathCounts[(i + 1) % 3] += numPaths;
                pathCounts[(i + 2) % 3] += numPaths;
                // i + 3 starts with the same number of paths as the current, 
                // however we do not need to update the value because the same slot in the ring buffer will be re-used 
                // pathCounts[(i + 3) % 3] is the same as pathCounts[i % 3]
            }

            return numPaths;
        }
    }
}