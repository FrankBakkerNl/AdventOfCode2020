using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;

namespace Tests.Puzzles
{
    public class Day01test
    {
        [Fact]
        public void Test1()
        {
            var input = new []
            {
                1721,
                979,
                366,
                299,
                675,
                1456
            };
            Day01.GetAnswer1(input).Should().Be(514579);
        }

        [Fact]
        public void Test2()
        {
            var input = new[]
            {
                1721,
                979,
                366,
                299,
                675,
                1456
            };
            Day01.GetAnswer2(input).Should().Be(241861950);
        }
    }
}
