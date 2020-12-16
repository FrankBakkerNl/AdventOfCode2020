using System;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/15 </summary>
    public class Day15
    {
//        [Focus]
        [Result(1325L)]
        public static long GetAnswer1(string input)
        {
            const int target = 2020;
            return FindAnswerNoLong(input, target);
        }


        [Result(59006L)]
        public static long GetAnswer2(string input)
        {
            const long target = 30_000_000;
            return FindAnswerNoLong(input, target);
        }

        public static long FindAnswerNoLong(string input, long target)
        {
            var data = input.Split(',').Select(long.Parse).ToArray();

            var mem = data[..^1].Select((v, i) => (v, i)).ToDictionary(t => t.v, t => (long)t.i + 1);
            var lastSaid = data[^1];

            for (var cur = data.Length; cur < target; cur++)
            {
                if (!mem.ContainsKey(lastSaid))
                {
                    mem[lastSaid] = cur;
                    lastSaid = 0;
                }
                else
                {
                    var age = mem[lastSaid];
                    mem[lastSaid] = cur;
                    lastSaid = Math.Abs(cur - age);
                }
            }

            return lastSaid;
        }
    }
}