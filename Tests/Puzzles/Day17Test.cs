using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day17test
    {
        private string[] SampleData = @".#.
..#
###".Split(NewLine);

        [Fact] public void PaseTest()
        {
            var set = Day17.Parse3D(SampleData);
            set.Count().Should().Be(5);
        }

        [Fact]
        public void NeigbourTest()
        {
            var steps = Day17.Position3D.NeigbourSteps;
            steps.Length.Should().Be(26);
        }

        [Fact]
        public void Answer1Test()
        {
             Day17.GetAnswer1(SampleData).Should().Be(112);
        }

        [Fact]
        public void Answer2Test()
        {
            Day17.GetAnswer2(SampleData).Should().Be(848);
        }

    }
}
