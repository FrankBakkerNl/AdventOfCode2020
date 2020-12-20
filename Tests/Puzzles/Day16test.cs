using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day16test
    {
        private string SampleData = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";

        [Fact] public void PaseTest()
        {
            var ranges = Day16.GetRanges(SampleData);
            var fields =  Day16.NearbyTicketValues(SampleData);
        }

        [Fact]
        public void Answer1Test()
        {
            Day16.GetAnswer1(SampleData).Should().Be(71);
        }

        private string[] SampleData2 = @"".Split(NewLine);

        [Fact]
        public void ParseRuleTest()
        {
            //Day16.ParseFieldRule("class: 0-1 or 4-19").Should().Be(default);
        }

        [Fact]
        public void Answer2Test()
        {
        }


    }
}
