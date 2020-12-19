using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/17 </summary>
    public class Day17
    {
        public abstract record Position 
        {
            public abstract IEnumerable<Position> Neigbours { get; }
        }

        public record Position3D(int x, int y, int z) : Position
        {
            public static Position3D operator +(Position3D a, Position3D b) => new Position3D(a.x + b.x, a.y + b.y, a.z + b.z);

            public override IEnumerable<Position> Neigbours => NeigbourSteps.Select(n => n + this);

            public static Position3D[] NeigbourSteps = GetNeigbourSteps().ToArray();

            public static IEnumerable<Position3D> GetNeigbourSteps()
            {
                for (int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                        for (int z = -1; z <= 1; z++)
                            if (!(x == 0 && y == 0 && z == 0))
                                yield return new Position3D(x, y, z);
            }

        };

        public record Position4D(int x, int y, int z, int w) : Position
        {
            public static Position4D operator +(Position4D a, Position4D b) => new Position4D(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

            public override IEnumerable<Position> Neigbours => NeigbourSteps4D.Select(n => n + this);

            public static Position4D[] NeigbourSteps4D = GetNeigbourSteps4D().ToArray();

            public static IEnumerable<Position4D> GetNeigbourSteps4D()
            {
                for (int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                        for (int z = -1; z <= 1; z++)
                            for (int w = -1; w <= 1; w++)
                                if (!(x == 0 && y == 0 && z == 0 && w == 0))
                                    yield return new Position4D(x, y, z, w);
            }

        };


        [Result(388)]
        public static int GetAnswer1(string[] input)
        {
            var currentState = Parse3D(input).ToHashSet();
            for (int i = 0; i < 6; i++)
            {
                currentState = CreateNewState(currentState);
            }

            return currentState.Count();
        }

        [Result(2280)]
        public static int GetAnswer2(string[] input)
        {
            var currentState = Parse4D(input).ToHashSet();
            for (int i = 0; i < 6; i++)
            {
                currentState = CreateNewState(currentState);
            }

            return currentState.Count();
        }


        public static HashSet<Position> CreateNewState(HashSet<Position> current)
        {
            Dictionary<Position, int> neigbourCount = new Dictionary<Position, int>();

            foreach (var cube in current)
            {
                foreach (var neigbour in cube.Neigbours)
                {
 
                    neigbourCount.TryGetValue(neigbour, out var count);
                    neigbourCount[neigbour] = count + 1;
                }
            }

            var staysActive = current.Where(a => neigbourCount.TryGetValue(a, out var count) && (count == 3 || count == 2));
            var becomesActve = neigbourCount.Where(kv => !current.Contains(kv.Key) && kv.Value == 3).Select(kv => kv.Key);

            return staysActive.Union(becomesActve).ToHashSet();
        }

        public static IEnumerable<Position> Parse3D(string[] input)
        {
            return input.SelectMany((l, y) => l.Select((c, x) => (c, x)).Where(t => t.c == '#')
            .Select(t => new Position3D(t.x, y, 0)));
        }

        public static IEnumerable<Position> Parse4D(string[] input)
        {
            return Parse3D(input).Cast<Position3D>().Select(p => new Position4D(p.x, p.y, p.z, 0));
        }



    }
}