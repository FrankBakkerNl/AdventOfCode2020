using System;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/13 </summary>
    public class Day13
    {
        [Result(4782)]
        public static int GetAnswer1(string[] input)
        {
            var curentTime = int.Parse(input[0]);
            var busses = input[1].Split(',').Where(c => c != "x").Select(int.Parse).ToArray();

            var busArrivals = busses.Select(bus => (bus, time: bus - curentTime % bus));

            var (bus, time) = busArrivals.OrderBy(b => b.time).FirstOrDefault();
            return bus * time;
        }
    }

}