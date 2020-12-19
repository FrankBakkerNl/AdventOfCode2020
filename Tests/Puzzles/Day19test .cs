using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day19test
    {
        private string SampleData = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: ""a""
5: ""b""

ababbb
bababa
abbbab
aaabbb
aaaabbb";

        [Fact] public void Answer1Test()
        {
            Day19.GetAnswer1(SampleData).Should().Be(2);
        }



    }
}
