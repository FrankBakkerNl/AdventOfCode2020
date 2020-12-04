using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/1 </summary>
    public class Day01
    {
        [Result(633216)]
        public static int GetAnswer1(int[] input)
        {
            var hashset = input.ToHashSet();
            var x = hashset.First(x => hashset.Contains(2020 - x));

            return x * (2020 - x );
        }

        [Result(68348924)]
        public static int GetAnswer2(int[] input)
        {
            var hashset = input.ToHashSet();
            var (xr,yr) = input.SelectMany((x,i) => input.Skip(i).Where(y => x + y < 2020).Select(y => (x,y)))
                .First(p=>hashset.Contains(2020 - p.x - p.y));

            return xr * yr * (2020 - xr - yr);
        }
    }
}
