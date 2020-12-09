using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/9 </summary>
    public class Day09
    {
        [Result(18272118L)]
        public static long GetAnswer1(long[] input) => FindFirstInvalid(input, 25);

        public static long FindFirstInvalid(long[] input, int preSize)
        {
            var preceding = new Queue<long>(input.Take(preSize));

            foreach (var current in input[preSize..])
            {
                if (!preceding.Any(x => preceding.Contains(current - x)))
                    return current;

                preceding.Enqueue(current);
                preceding.Dequeue();
            }
            return -1;
        }

        [Result(2186361L)]
        public static long GetAnswer2(long[] input) => FindRange(input, 18272118);

        public static long FindRange(long[] input, long target)
        {
            int lower = 0;
            int upper = 1;

            long sum = input[lower] + input[upper];

            while (sum != target)
            {
                if (sum < target)
                {
                    upper++;
                    sum += input[upper];
                }
                else 
                {
                    sum -= input[lower];
                    lower++;
                }
            }

            var range = input[lower..upper];
            return range.Max() + range.Min();
        }
    }
}