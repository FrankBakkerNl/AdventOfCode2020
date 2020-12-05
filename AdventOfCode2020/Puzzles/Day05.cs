using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/5 </summary>
    public class Day05
    {
        [Result(944)]
        public static int GetAnswer1(string[] input) => input.Max(GetSeatNumber);

        [Result(554)]
        public static int GetAnswer2(string[] input)
        {
            var takenSeats = input.Select(GetSeatNumber).OrderBy(s => s).ToList();
            var firstId = takenSeats.First();
            return takenSeats.SkipWhile((id, i) => id - i == firstId).First()-1;
        }

        public static int GetSeatNumber(string code) => 
            code.Select((c, i) => c == 'B' || c == 'R' ? 1 << 9 - i : 0).Sum();
    }
}