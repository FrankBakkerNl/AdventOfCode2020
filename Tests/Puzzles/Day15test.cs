using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day15test
    {
        private string SampleData = @"0,3,6";

        [Fact]
        public void Answer1Test()
        {
            Day15.FindAnswerNoLong(SampleData, 10).Should().Be(0);
        }

        private string[] SampleData2 = @"".Split(NewLine);

        [Fact]
        public void Answer2Test()
        {
            Day15.FindAnswerNoLong(SampleData, 10).Should().Be(0);
        }
    }
}
