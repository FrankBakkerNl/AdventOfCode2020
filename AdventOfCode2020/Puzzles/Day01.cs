using System;

namespace AdventOfCode2020.Puzzles
{
    public class Day01
    {
        [Result(633216)]
        public static int GetAnswer1(int[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = i+1; j < input.Length; j++)
                {
                    if (input[i] + input[j] == 2020) return input[i] * input[j];
                }
            }
            throw new InvalidOperationException("No solution found");
        }

        [Result(68348924)]
        public static int GetAnswer2(int[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = i + 1; j < input.Length; j++)
                {
                    for (var k = j + 1; k < input.Length; k++)
                    {
                        if (input[i] + input[j] + input[k] == 2020) return input[i] * input[j] * input[k];
                    }
                }
            }
            throw new InvalidOperationException("No solution found");
        }
    }
}
