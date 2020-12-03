using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;

namespace Tests.Puzzles
{
    public class Day02test
    {
        [Fact]
        public void PaseTest()
        {
            var line = "1-3 a: abcde";
            Day02.Parse(line).Should().Be((1,3,'a', "abcde"));
        }

        [Fact]
        public void Answer1Test()
        {
            var input = new[]
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };
            Day02.GetAnswer1(input).Should().Be(2);
        }

        [Fact]
        public void Answer2Test()
        {
            var input = new[]
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };
            Day02.GetAnswer2(input).Should().Be(1);
        }


    }
}
