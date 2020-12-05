using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/5 </summary>
    public class Day05
    {

        [Focus]
        public static int GetAnswer1(string []input)
        {
            return input.Max(GetSeatNumber);
        }

        public static int GetSeatNumber(string code)
        {
            return code.Select((c, i) => c == 'B' || c == 'R' ? 1 << 9-i : 0).Sum();
        }
    }
}
