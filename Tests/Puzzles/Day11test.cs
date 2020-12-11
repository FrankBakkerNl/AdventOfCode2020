using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day11test
    {
        private string[] SampleData = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL".Split(NewLine).ToArray();

        [Fact]
        public void Answer1Test()
        {
            Day11.GetAnswer1(SampleData).Should().Be(37);
        }


        [Fact]
        public void Answer2Test()
        {
            Day11.GetAnswer2(SampleData).Should().Be(26);
        }


    }
}
