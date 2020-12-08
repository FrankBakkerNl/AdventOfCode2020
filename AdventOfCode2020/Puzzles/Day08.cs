using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/8 </summary>
    public class Day08
    {
//        [Focus]
        public static int GetAnswer1(string[] input)
        {
            var code = ParseLines(input).ToArray();
            var cpu = new Cpu(code);
            return cpu.Run();
        }

        [Focus] // not 425
        public static int GetAnswer2(string[] input)
        {
            var code = ParseLines(input).ToArray();
            for (var index = 0; index < code.Length; index++)
            {
                var line = code[index];
                var clone = code.ToArray();
                if (line.Item1 == "nop")
                {
                    clone[index] = ("jmp", line.Item2);
                }
                else if (line.Item1 == "jmp")
                {
                    clone[index] = ("nop", line.Item2);
                }
                else continue;

                var cpu = new Cpu(clone);
                cpu.Run();
                if (cpu.done) return cpu.acc;
            }

            return -1;
        }


        public static IEnumerable<(string, int)> ParseLines(string[] input) => input.Select(Parse);

        public static (string, int) Parse(string line)
        {
            var parts = line.Split(' ');
            return (parts[0], int.Parse(parts[1]));
        }
    }

    public class Cpu
    {
        public int acc =0;
        private int ip =0;
        HashSet<int> visited = new HashSet<int>();

        private (string, int)[] code;

        public Cpu((string, int)[] code)
        {
            this.code = code;
        }

        public int Run()
        {
            while (ip < code.Length)
            {
                var current = code[ip];
                if (!visited.Add(ip)) return acc;
                if (current.Item1 == "acc")
                {
                    acc += current.Item2;
                    ip++;
                }

                else if (current.Item1 == "jmp")
                {
                    ip += current.Item2;
                }
                else ip++;

            }

            done = true;
            return 0;
        }

        public bool done;
    }
}