using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/11 </summary>
    public class Day11
    {
        [Result(2273)]
        public static int GetAnswer1(string[] input)
        {
            var index = AssignSeatNumbers(input);
            var map = CreateDirectNeighborMap(index);
            var state = MapCurrentState(input, index);

            bool changed;
            do
            {
                changed = CreateNewState(state, map, 4);
            } while (changed);

            return state.Count(l => l);
        }

        [Result(2064)]
        public static int GetAnswer2(string[] input)
        {
            var index = AssignSeatNumbers(input);
            var map = CreateVisibleNeighborMap(input, index);
            var state = MapCurrentState(input, index);

            bool changed;
            do
            {
                changed = CreateNewState(state, map, 5);
            } while (changed);

            return state.Count(l => l);
        }

        public static (int r, int c)[] Directions =
        {
            (-1, -1), (-1, 0), (-1, 1),
            ( 0, -1), /*skip*/ ( 0, 1),
            ( 1, -1), ( 1, 0), ( 1, 1),
        };

        private static Dictionary<(int r, int c), int> AssignSeatNumbers(string[] input) =>
            GetAllSeatPositions(input).Select((c, i) => (c, i))
                .ToDictionary(v => v.c, v => v.i);

        private static IEnumerable<(int r, int c)> GetAllSeatPositions(string[] input) =>
            Range(0, input.Length)
                .SelectMany(r => Range(0, input[0].Length)
                    .Select(c => (r, c)))
                .Where(p => "#L".Contains(input[p.r][p.c]));


        private static int[][] CreateDirectNeighborMap(Dictionary<(int r, int c), int> seatIndex) =>
            seatIndex.Keys
                .Select(position => Directions.Select(delta => (r:position.r + delta.r, c:position.c + delta.c ))
                    .Where(seatIndex.ContainsKey)
                    .Select(n => seatIndex[n])
                    .ToArray())
                .ToArray();

        /// <summary> For each seat create an array of its neighbors </summary>
        private static int[][] CreateVisibleNeighborMap(string[] input, Dictionary<(int r, int c), int> seatIndex) =>
            seatIndex.Keys
                .Select(position => Directions.Select(delta => NextNeighbor(position, delta, input))
                    .Where(n => n.HasValue)
                    .Select(n => seatIndex[n.Value])
                    .ToArray())
                .ToArray();

        public static (int, int)? NextNeighbor((int r, int c) position, (int r, int c) delta, string[] input)
        {
            do
            {
                position = (position.r + delta.r, position.c + delta.c);

                if (position.r < 0 || position.r >= input.Length || 
                    position.c < 0 || position.c >= input[0].Length) return null;

            } while (input[position.r][position.c] == '.');

            return position;
        }

        private static bool[] MapCurrentState(string[] input, Dictionary<(int r, int c), int> seatIndex) =>
            seatIndex.Keys.Select(c => input[c.r][c.c] == '#').ToArray();


        public static bool CreateNewState(bool[] current, int[][] neighborMap, int leaveSeatThreshold)
        {
            var counts = new int[current.Length];
            for (var i = 0; i < current.Length; i++)
            {
                if (!current[i]) continue;

                foreach (var neighbor in neighborMap[i])
                {
                    counts[neighbor]++;
                }
            }

            var changed = false;
            for (var i = 0; i < current.Length; i++)
            {
                var taken = current[i];
                var numNeighborTaken = counts[i];
                if (!taken && numNeighborTaken == 0 ||
                    taken && numNeighborTaken >= leaveSeatThreshold )
                {
                    current[i] = !taken;
                    changed = true;
                }
            }

            return changed;
        }
    }
}