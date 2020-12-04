using System;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;

namespace Tests.Puzzles
{
    public class Day03test
    {
        private string[] SampleData1= @"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#".Split(Environment.NewLine);

        [Fact]
        public void TestParse()
        {
            var map = new Day03.Map(SampleData1);
            map[0,0].Should().BeFalse();
            map[1,0].Should().BeTrue();
            map[1,1].Should().BeFalse();
            map[1, 24].Should().BeFalse();
        }

        [Fact]
        public void Answer1Test()
        {
            Day03.GetAnswer1(SampleData1).Should().Be(7);
        }

        [Fact]
        public void Answer2Test()
        {
            Day03.GetAnswer2(SampleData1).Should().Be(336);
        }
    }
}
