using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day13test
    {
        private string[] SampleData = @"939
7,13,x,x,59,x,31,19".Split(NewLine).ToArray();

        [Fact]
        public void Answer1Test()
        {
            Day13.GetAnswer1(SampleData).Should().Be(295);
        }

    }
}
