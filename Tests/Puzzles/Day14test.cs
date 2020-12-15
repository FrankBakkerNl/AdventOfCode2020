using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day14test
    {
        private string[] SampleData = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0".Split(NewLine).ToArray();

        [Fact]
        public void Answer1Test()
        {
            Day14.GetAnswer1(SampleData).Should().Be(165);
        }

        private string[] SampleData2 = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1".Split(NewLine);
        [Fact]
        public void Answer2Test()
        {
            Day14.GetAnswer2(SampleData2).Should().Be(208);
        }


    }
}
