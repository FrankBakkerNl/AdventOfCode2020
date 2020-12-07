using System;
using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;

namespace Tests.Puzzles
{
    public class Day06test
    {
        private string SampleData = @"abc

a
b
c

ab
ac

a
a
a
a

b";
        [Fact]
        public void Answer1Test()
        {
            Day06.GetAnswer1(SampleData).Should().Be(11);
        }

        [Fact]
        public void Answer2Test()
        {
            Day06.GetAnswer2(SampleData).Should().Be(6);
        }
    }
}
