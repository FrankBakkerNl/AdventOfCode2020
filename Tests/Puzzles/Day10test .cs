using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day10test
    {
        private int[] SampleData = @"16
10
15
5
1
11
7
19
6
12
4".Split(NewLine).Select(int.Parse).ToArray();

        [Fact]
        public void Answer1Test()
        {
            Day10.GetAnswer1(SampleData).Should().Be(35);
        }


        private int[] SampleData2 = @"28
            33
            18
            42
            31
            14
            46
            20
            48
            47
            24
            23
            49
            45
            19
            38
            39
            11
            1
            32
            25
            35
            8
            17
            7
            9
            4
            2
            34
            10
            3".Split(NewLine).Select(int.Parse).ToArray();

        [Fact]
        public void Answer2Test()
        {
            Day10.GetAnswer2(SampleData).Should().Be(8);
        }

        [Fact]
        public void Answer2Test2()
        {
            Day10.GetAnswer2(SampleData2).Should().Be(19208);
        }

        [Fact]
        public void Answer2All3s()
        {
            Day10.GetAnswer2(new []{3,6,9}).Should().Be(1);
        }
    }
}
