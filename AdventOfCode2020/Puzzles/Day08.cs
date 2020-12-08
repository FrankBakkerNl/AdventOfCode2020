using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/8 </summary>
    public class Day08
    {
        [Result(1941)]
        public static int GetAnswer1(string[] input)
        {
            var code = Cpu.ParseLines(input).ToArray();
            var cpu = new Cpu(code);
            return cpu.Run();
        }

        [Result(2096)]
        public static int GetAnswer2(string[] input)
        {
            var code = Cpu.ParseLines(input).ToArray();

            for (var index = 0; index < code.Length; index++)
            {
                var line = code[index];
                if (line.op == "acc") continue;

                var clone = code.ToArray();

                clone[index] = line with { op = line.op == "jmp" ? "nop" : "jmp" };

                var cpu = new Cpu(clone);
                var result = cpu.Run();

                if (!cpu.LoopDetected) return result;
            }

            return -1;
        }


        public class Cpu
        {
            public record Operation(string op, int value);

            private int _acc;
            private int _ip;
            readonly HashSet<int> _visited = new HashSet<int>();
            public bool LoopDetected { get; private set; }

            private readonly Operation[] _code;

            public Cpu(Operation[] code)
            {
                _code = code;
            }

            public int Run()
            {
                while (_ip < _code.Length)
                {
                    if (!_visited.Add(_ip))
                    {
                        LoopDetected = true;
                        return _acc;
                    }

                    var (op, value) = _code[_ip];
                    switch (op)
                    {
                        case "acc":
                            _acc += value;
                            _ip++;
                            break;

                        case "jmp":
                            _ip += value;
                            break;

                        default:
                            _ip++;
                            break;
                    }
                }
                return _acc;
            }

            public static IEnumerable<Operation> ParseLines(string[] input) => input.Select(Parse);

            public static Operation Parse(string line)
            {
                var parts = line.Split(' ');
                return new Operation(parts[0], int.Parse(parts[1]));
            }
        }
    }
}