using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day18Test
    {
        private string[] SampleData = @"".Split(NewLine);

        [Fact] public void EvaluateTest()
        {
            Day18.Evaluate("2 * 3 + (4 * 5)").Should().Be(26);
            Day18.Evaluate("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2").Should().Be(13632);

            Day18.Evaluate("(2 )*( 3 + ((4 )*( 5)))").Should().Be(46);
        }

    }
}
