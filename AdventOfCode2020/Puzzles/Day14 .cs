using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/14 </summary>
    public class Day14
    {
        public record Operation();
        public record MemOp(long addr, long value) : Operation;
        public record MaskOp(long andMask, long orMask, long xMask) : Operation;
        public record MaskOp2(long orMask, long[] xMasks) : Operation;

        [Result(7817357407588)]
        public static long GetAnswer1(string[] input)
        {
            var operations = input.Select(Parse).ToArray();
            Dictionary<long, long> memory = new Dictionary<long, long>();
            MaskOp currentMask = default;

            foreach (var op in operations)
            {
                if (op is MaskOp maskOp) currentMask = maskOp;
                else
                {
                    var memOp = (MemOp)op;
                    memory[memOp.addr] = (memOp.value & currentMask.andMask) | currentMask.orMask;
                }
            }
            return memory.Values.Sum();
        }

        [Result(4335927555692)]
        public static long GetAnswer2(string[] input)
        {
            var operations = input.Select(Parse2).ToArray();
            Dictionary<long, long> memory = new Dictionary<long, long>();
            MaskOp2 currentMask = default;

            foreach (var op in operations)
            {
                if (op is MaskOp2 maskOp) currentMask = maskOp;
                else
                {
                    var memOp = (MemOp)op;
                    long baseAddr = memOp.addr | currentMask.orMask;
                    var àddresses = currentMask.xMasks.Select(m => baseAddr ^ m);

                    foreach (var address in àddresses)
                    {
                        memory[address] = memOp.value;
                    }
                }

            }
            return memory.Values.Sum();
        }

        public static Operation Parse(string line)
        {
            var maskMatch = Regex.Match(line, "mask = (?<mask>[X01]{36})");
            if (maskMatch.Success)
            {
                var mask = maskMatch.Groups["mask"].Value;
                var andMask = Convert.ToInt64(mask.Replace("X", "1"), 2);
                var orMask = Convert.ToInt64(mask.Replace("X", "0"), 2);
                var xMask = Convert.ToInt64(mask.Replace("1", "0").Replace("X", "1"), 2);
                return new MaskOp(andMask, orMask, xMask);
            }
            else
            {
                var memMatch = Regex.Match(line, "mem\\[(?<addr>[0-9]+)\\] = (?<val>[0-9]+)");
                return new MemOp(long.Parse(memMatch.Groups["addr"].Value), long.Parse(memMatch.Groups["val"].Value));
            }
        }


        public static Operation Parse2(string line)
        {
            var maskMatch = Regex.Match(line, "mask = (?<mask>[X01]{36})");
            if (maskMatch.Success)
            {
                var mask = maskMatch.Groups["mask"].Value;
                var orMask = Convert.ToInt64(mask.Replace("X", "0"), 2);
                var xMask = Convert.ToInt64(mask.Replace("1", "0").Replace("X", "1"), 2);
                var xCount = mask.Count(c => c == 'X');
                var xMasks = GetxMasks(xMask);
                return new MaskOp2(orMask, xMasks);
            }
            else
            {
                var memMatch = Regex.Match(line, "mem\\[(?<addr>[0-9]+)\\] = (?<val>[0-9]+)");
                return new MemOp(long.Parse(memMatch.Groups["addr"].Value), long.Parse(memMatch.Groups["val"].Value));
            }
        }

        static long[] GetxMasks(long mask)
        {
            long[] adresses = { 1 };
            for (long bit = 1L; bit < 1L << 36; bit <<= 1)
            {
                if ((mask & bit) != 0L) // mask has this bit set
                {
                    adresses = adresses.Concat(adresses.Select(a => a ^ bit)).ToArray();
                }
            }
            return adresses;
        }
    }
}