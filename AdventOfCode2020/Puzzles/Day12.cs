using System;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/12 </summary>
    public class Day12
    {
        const string directions = "NESW";
        [Result(1152)]
        public static int GetAnswer1(string[] input)
        {
            var lines = input.Select(ParseLine);
            var initialState = (dir:1, x:0, y:0);

            var (_, x, y) = lines.Aggregate(initialState, GetNewState);

            return Math.Abs(x) + Math.Abs(y);
        }

        private static (int dir, int x, int y) GetNewState((int dir, int x, int y) state, (char op, int steps) instruction)
        {
            state = (Turn(state.dir, instruction.op, instruction.steps), state.x, state.y);
            
            var moveDir = instruction.op == 'F' ? directions[state.dir] : instruction.op;

            return moveDir switch
            {
                'E' => (state.dir, state.x + instruction.steps, state.y),
                'W' => (state.dir, state.x - instruction.steps, state.y),
                'N' => (state.dir, state.x, state.y + instruction.steps),
                'S' => (state.dir, state.x, state.y - instruction.steps),
                _ => state
            };
        }

        static int Turn(int dir, char op, int deg) => op switch
        {
            'L' => (dir - (deg / 90) + 4) % 4,
            'R' => (dir + (deg / 90) + 4) % 4,
            _ => dir
        };

        static (char op, int steps) ParseLine(string line) => ( line[0], int.Parse(line[1..]));

   }
}