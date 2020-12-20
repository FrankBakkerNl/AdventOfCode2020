using AdventOfCode2020.Puzzles.Day16Hepers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;
using static System.Environment;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/18 </summary>
    public class Day18
    {
        [Result(3_348_222_486_398L)]
        public static long GetAnswer1(string[] input) => input.Select(Evaluate).Sum();

        [Result(43_423_343_619_505L)]
        public static long GetAnswer2(string[] input) => input.Select(AddPrecedence).Select(Evaluate).Sum();

        public static long Evaluate(string expression)
        {
            var stack = new Stack<(long, char)>();
            char lastOp = '+';
            long value = 0;

            foreach (var ch in expression)
            {
                if (ch == '+' || ch == '*') lastOp = ch;

                else if (ch >= '0' && ch <= '9')
                {
                    if (lastOp == '+') value += ch - '0';
                    else if (lastOp == '*') value *= ch - '0';
                }

                else if (ch == '(')
                {
                    stack.Push((value, lastOp));
                    value = 0;
                    lastOp = '+';
                }

                else if (ch ==')')
                {
                    var (popVal, popOp) = stack.Pop();
                    if (popOp == '+') value += popVal;
                    else if (popOp == '*') value *= popVal;
                }
            }
            return value;
        }

        public static string AddPrecedence(string expression)
        {
            return "(" + expression.Replace("(", "((").Replace(")", "))").Replace("*", ")*(") + ")";
        }

    }
}