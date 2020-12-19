using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/19 </summary>
    public class Day19
    {
        [Result(178)]
        [Focus]
        public static int GetAnswer1(string input)
        {
            var ruleLines = input.Split("\r\n\r\n")[0].Split("\r\n");
            var root = BuildRuleTree(ruleLines);

            var samples = input.Split("\r\n\r\n")[1].Split("\r\n");
            return samples.Count(s => root.Match(s.AsSpan())==s.Length);
        }

        static (int id, string pattern) SplitRule(string input)
        {
            var parts = input.Split(": ");
            return (int.Parse(parts[0]), parts[1]);
        }

        static Rule BuildRuleTree(string[] input)
        {
            var patterns = input.Select(SplitRule).ToDictionary(r => r.Item1, r=>r.pattern);
            var RuleBook = new Dictionary<int, Rule>();

            Rule BuildRule(string pattern)
            {
                if (int.TryParse(pattern, out var i)) return (GetRule(i));

                if (pattern.StartsWith("\"")) return new LiteralRule(pattern[1]);

                var orParts = pattern.Split(" | ");
                if (orParts.Length == 2) return new OrRule(BuildRule(orParts[0]), BuildRule(orParts[1]));

                var andParts = pattern.Split(" ").Select(c => GetRule(int.Parse(c))).ToArray();

                if (andParts.Length > 1)return new AndRule(andParts);

                return andParts[0];
            }

            Rule GetRule(int id)
            {
                if (RuleBook.TryGetValue(id, out var r)) return r;
                var pattern = patterns[id];
                var rule = BuildRule(pattern);
                RuleBook[id] = rule;
                return rule;
            }

            return BuildRule(patterns[0]);
        }


        public abstract record Rule()
        {
            public abstract int Match(ReadOnlySpan<char> input);
        }

        public record LiteralRule (char literal): Rule
        {
            public override int Match(ReadOnlySpan<char> input) => (input[0] == literal) ? 1 : 0;
        }

        public record AndRule( Rule[] rules) : Rule
        {
            public override int Match(ReadOnlySpan<char> input)
            {
                int matched = 0;
                foreach(var rule in rules)
                {
                    var m1 = rule.Match(input[matched..] );
                    if (m1 == 0) return 0;
                    matched += m1;
                }
                return matched;
            }            
        }


        public record OrRule(Rule first, Rule second) : Rule
        {
            public override int Match(ReadOnlySpan<char> input)
            {
                var m1 = first.Match(input);
                if (m1 > 0) return m1;

                var m2 = second.Match(input);
                return m2;
            }
        }
    }
}