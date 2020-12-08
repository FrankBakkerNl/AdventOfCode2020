using System;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/6 </summary>
    public class Day06
    {
        /// <summary> Count distinct number of positive answers in group </summary>
        [Result(7128)]
        public static int GetAnswer1(string input) => 
            input.Split(NewLine + NewLine)
                .Select(CountDistinct).Sum();

        private static int CountDistinct(string groupAnswers) =>
            groupAnswers.Distinct().Except(NewLine).Count();


        /// <summary> Count number of common positive answers in group </summary>
        [Result(3640)]
        public static int GetAnswer2(string input) => 
            input.Split(NewLine + NewLine)
            .Select(CountCommon).Sum();

        public static int CountCommon(string groupAnswers) =>
            groupAnswers.Split(NewLine)
                .Select(a => a.AsEnumerable())
                .Aggregate(Enumerable.Intersect).Count();
    }
}