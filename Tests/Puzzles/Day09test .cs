using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day09test
    {
        private long[] SampleData = @"35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576".Split(NewLine).Select(long.Parse).ToArray();

        [Fact]
        public void Answer1Test()
        {
            Day09.FindFirstInvalid(SampleData, 5).Should().Be(127);
        }


        [Fact]
        public void Answer2Test()
        {
            Day09.FindRange(SampleData, 127).Should().Be(62);
        }
    }
}
