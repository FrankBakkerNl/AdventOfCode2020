using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day12test
    {
        private string[] SampleData = @"F10
N3
F7
R90
F11".Split(NewLine).ToArray();

        [Fact]
        public void Answer1Test()
        {
            Day12.GetAnswer1(SampleData).Should().Be(25);
        }


        [Fact]
        public void Answer2Test()
        {
            Day12.GetAnswer2(SampleData).Should().Be(286);
        }


    }
}
