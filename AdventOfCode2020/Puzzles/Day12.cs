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
            var initialState = (dir: 1, x: 0, y: 0);

            var (_, x, y) = lines.Aggregate(initialState, GetNewState);

            return Math.Abs(x) + Math.Abs(y);
        }

        static (char op, int steps) ParseLine(string line) => (line[0], int.Parse(line[1..]));

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


        record Position(int x, int y);
        record StateV2(Position wp, Position ship);

        [Result(58637)]
        public static int GetAnswer2(string[] input)
        {
            var lines = input.Select(ParseLine);
            var initialState = new StateV2(new Position(10, 1), new Position(0, 0));

            var (_, (x, y)) = lines.Aggregate(initialState, GetNewStateV2);

            return Math.Abs(x) + Math.Abs(y);
        }

        static StateV2 GetNewStateV2(StateV2 curent, (char op, int steps) instruction) => 
            instruction.op switch
            {
                'F' => curent with {ship = new Position(curent.ship.x + curent.wp.x * instruction.steps,
                                                        curent.ship.y + curent.wp.y * instruction.steps) },

                'L' => curent with { wp = RotateWp(curent.wp, instruction.steps) },
                'R' => curent with { wp = RotateWp(curent.wp, -instruction.steps) },

                'E' => curent with { wp = curent.wp with { x = curent.wp.x + instruction.steps } },
                'W' => curent with { wp = curent.wp with { x = curent.wp.x - instruction.steps } },
                'N' => curent with { wp = curent.wp with { y = curent.wp.y + instruction.steps } },
                'S' => curent with { wp = curent.wp with { y = curent.wp.y - instruction.steps } },
            };

        static Position RotateWp(Position current, int deg)
        {
            var steps = (deg / 90) + 4;
            return (steps % 4) switch
            {
                0 => current,
                1 => new Position(-current.y, current.x),
                2 => new Position(-current.x, -current.y),
                3 => new Position(current.y, -current.x),
            };
        }
    }
}