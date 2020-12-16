using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/16 </summary>
    public class Day16
    {
        [Result(27870)]
        public static int GetAnswer1(string input)
        {
            var ranges = GetRanges(input).ToArray();
            var fieldvalue = TicketFields(input);

            var invalid = fieldvalue.Where(v => !ranges.Any(r => IsInRange(v, r))).ToList();
            return invalid.Sum();

        }

        static bool IsInRange(int value, (int, int) range) => value >= range.Item1 && value <= range.Item2;

        public static IEnumerable<(int,int)> GetRanges(string input)
        {
            var matches = Regex.Matches(input, "(?<lower>[0-9]+)-(?<upper>[0-9]+)");
            return matches.Select(m => (int.Parse(m.Groups["lower"].Value), int.Parse(m.Groups["upper"].Value)));
        }

        public static IEnumerable<int> TicketFields(string input)
        {
            var index = input.IndexOf("nearby tickets:");
            var matches = Regex.Matches(input[index..], "[0-9]+");
            return matches.Select(m => int.Parse(m.Value));

        }
    }
}