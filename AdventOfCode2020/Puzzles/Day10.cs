using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/9 </summary>
    public class Day10
    {
        [Result(1656L)]
        public static long GetAnswer1(int[] input)
        {
            var pairs = WithNext(input.OrderBy(i => i)).ToList();
            var ones = pairs.Count(p => p.current == p.next - 1) + 1;
            var trees = pairs.Count(p => p.current == p.next - 3) + 1;
            return ones * trees;
        }


        private static IEnumerable<(T current, T next)> WithNext<T>(IEnumerable<T> input)
        {
            var previous = default(T);
            var first = true;

            foreach (var current in input)
            {
                if (!first)
                {
                    yield return (previous, current);
                }

                previous = current;
                first = false;
            }

            yield return (previous, default(T));
        }


        [Result(56693912375296)]
        public static long GetAnswer2(int[] input)
        {
            var hashset = input.ToHashSet();
            hashset.Add(0);
            var target = hashset.Max()+ 3;

            var cache  = new Dictionary<int, long>();

            long CountPaths(int target)
            {
                if (cache.TryGetValue(target, out var value)) return value;

                if (target == 0 ) return 1;
                long paths = 0;
                if (hashset.Contains(target - 1)) paths += CountPaths(target - 1);
                if (hashset.Contains(target - 2)) paths += CountPaths(target - 2);
                if (hashset.Contains(target - 3)) paths += CountPaths(target - 3);
                cache[target] = paths;
                return paths;
            }
            
            return CountPaths(target);
        }
    }
}