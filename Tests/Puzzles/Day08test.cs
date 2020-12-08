using System.Linq;
using AdventOfCode2020.Puzzles;
using FluentAssertions;
using Xunit;
using static System.Environment;

namespace Tests.Puzzles
{
    public class Day08test
    {
        private string[] SampleData = @"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6".Split(NewLine);

        [Fact]
        public void Answer1Test()
        { 
            Day08.GetAnswer1(SampleData).Should().Be(5);
        }

        [Fact]
        public void ParseTest()
        {
            var res = Day08.Cpu.ParseLines(SampleData);
            res.Select(o=>(o.op, o.value)).Should().StartWith(new[]
            {
                ( "nop", 0 ),
                ( "acc", 1 ),
                ( "jmp", 4 ),
                ( "acc", 3 ),
                ( "jmp", -3 ),
                ( "acc", -99 ),
            });
        }

        [Fact]
        public void Answer2Test()
        {
            Day08.GetAnswer2(SampleData).Should().Be(8);
        }


    }
}
