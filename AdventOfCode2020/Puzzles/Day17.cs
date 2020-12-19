using AdventOfCode2020.Puzzles.Day16Hepers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;
using static System.Environment;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/17 </summary>
    public class Day17
    {

        public record Position(int x, int y, int z)
        {
            public static Position operator +(Position a, Position b) => new Position(a.x + b.x, a.y + b.y, a.z + b.z);
        };

        [Result(388)]
        public static int GetAnswer1(string[] input)
        {
            var currentState = Parse(input);
            for (int i = 0; i < 6; i++)
            {
                currentState = CreateNewState(currentState);

                var ordered = currentState.OrderBy(s => s.z).ThenBy(s => s.y).ThenBy(s => s.z).ToArray();
            }

            return currentState.Count;
        }


        public static HashSet<Position> CreateNewState(HashSet<Position> current)
        {
            Dictionary<Position, int> neigbourCount = new Dictionary<Position, int>();

            foreach (var cube in current)
            {
                foreach (var step in NeigbourSteps)
                {
                    var neigbour = step + cube;
                    neigbourCount.TryGetValue(neigbour, out var count);
                    neigbourCount[neigbour] = count + 1;
                }
            }

            var staysActive = current.Where(a => neigbourCount.TryGetValue(a, out var count) && (count == 3 || count == 2));
            var becomesActve = neigbourCount.Where(kv => !current.Contains(kv.Key) && kv.Value == 3).Select(kv => kv.Key);

            return staysActive.Union(becomesActve).ToHashSet();
        }

        public static HashSet<Position> Parse(string[] input)
        {
            return input.SelectMany((l, y) => l.Select((c, x) => (c, x)).Where(t => t.c == '#')
            .Select(t => new Position(t.x, y, 0)))
            .ToHashSet();
        }

        public static Position[] NeigbourSteps = GetNeigbourSteps().ToArray();

        public static IEnumerable<Position> GetNeigbourSteps()
        {
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    for (int z = -1; z <= 1; z++)
                        if (!(x == 0 && y == 0 && z == 0))
                            yield return new Position(x, y, z);
        }
    }
}