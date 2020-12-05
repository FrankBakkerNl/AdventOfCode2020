using System;
using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;

namespace Tests.Puzzles
{
    public class Day05test
    {

        [Theory]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]

        public void TestSeatNumber(string code, int expected)
        {
            Day05.GetSeatNumber(code).Should().Be(expected);
        }
    }
}
