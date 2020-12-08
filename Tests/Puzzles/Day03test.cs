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
