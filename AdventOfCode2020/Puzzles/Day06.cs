using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/6 </summary>
    public class Day06
    {
        [Result(7128)]
        public static int GetAnswer1(string input) => 
            SplitGroups(input).Select(g=>g.Replace("\r\n", "").Distinct().Count()).Sum();

        public static IEnumerable<string> SplitGroups(string input) => input.Split(new[] {"\r\n\r\n"}, StringSplitOptions.None);

        [Result(3640)]
        public static int GetAnswer2(string input) => SplitGroups(input)
            .Select(CountAllCommonInGroup)
            .Sum();

        public static int CountAllCommonInGroup(string groupAnswers) =>
            groupAnswers.Split(new[] {"\r\n"}, StringSplitOptions.None)
                .Select(a => a.AsEnumerable())
                .Aggregate((x, y) => x.Intersect(y)).Count();
    }
}