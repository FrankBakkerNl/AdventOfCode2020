using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Linq.Enumerable;
using static System.Environment;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/16 </summary>
    public class Day16
    {
        [Result(27870)]
        public static int GetAnswer1(string input)
        {
            var ranges = GetRanges(input).ToArray();
            var fieldvalue = NearbyTicketValues(input);

            return fieldvalue.Where(v => !ranges.Any(r => IsInRange(v, r))).Sum();
        }

        static bool IsInRange(int value, (int, int) range) => value >= range.Item1 && value <= range.Item2;

        public static IEnumerable<(int, int)> GetRanges(string input)
        {
            var matches = Regex.Matches(input, "(?<lower>[0-9]+)-(?<upper>[0-9]+)");
            return matches.Select(m => (int.Parse(m.Groups["lower"].Value), int.Parse(m.Groups["upper"].Value)));
        }

        public static IEnumerable<int> NearbyTicketValues(string input)
        {
            var index = input.IndexOf("nearby tickets:");
            var x = input.AsSpan()[1..2];
            var matches = Regex.Matches(input[index..], "[0-9]+");
            return matches.Select(m => int.Parse(m.Value));
        }

        [Result(3173135507987L)]
        public static long GetAnswer2(string input)
        {
            var (fieldRules, myTicket, nearbyTickets) = ParseInput(input);

            var validTickets = nearbyTickets.Where(t => t.All(f => fieldRules.Any(r => r.Matches(f)))).ToList();

            var answersPerField = fieldRules.Select((r, i) => (r.name, values: validTickets.Select(t => t[i]))).ToArray();

            var validFieldIndexesPerRule = fieldRules.Select(rule => (rule.name, answersPerField
                .Select((a, i) => (a, i)).Where(answers => answers.a.values.All(answer => rule.Matches(answer)))
                .Select(answer => answer.i).ToArray()));

            var best = FindBestMatches(validFieldIndexesPerRule);
            var departureFiledIds = best.Where(m => m.name.StartsWith("departure")).Take(6).Select(m => m.index).ToArray();

            return departureFiledIds.Select(id => (long)myTicket[id]).Aggregate((a, b) => a * b);
        }

        static IEnumerable<(string name, int index)> FindBestMatches(IEnumerable<(string name, int[] matchingFields)> matches)
        {
            var unmatched = matches.OrderBy(m=>m.matchingFields.Length);

            var matchedAnswers = new HashSet<int>();
            foreach(var match in unmatched)
            {
                var remainingUnmatched = match.matchingFields.Except(matchedAnswers);
                if (remainingUnmatched.Count() == 1)
                {
                    yield return (match.name, remainingUnmatched.First());
                    matchedAnswers.Add(remainingUnmatched.First());
                }
            }
        }

        public static int[][] ParseNearbyTickets(string input) =>
            input.Split(NewLine)[1..].Select(line => line.Split(',').Select(int.Parse).ToArray()).ToArray();


        public record Range(int lower, int upper) 
        {
            public bool Matches(int value) => value >= lower && value <= upper;
        };

        public record FieldRule(string name, Range range1, Range range2) 
        {
            public bool Matches(int value) => range1.Matches(value) || range2.Matches(value);
        };

        private static (FieldRule[] fieldRules, int[] myTicket, int[][] nearbyTickets) ParseInput(string input)
        {
            // Array deconstruction, just for fun
            var segments = input.Split(NewLine + NewLine);

            return (
                fieldRules: ParseFieldRules(segments[0]),
                myTicket: segments[1].Split(NewLine)[1].Split(",").Select(int.Parse).ToArray(),
                nearbyTickets: ParseNearbyTickets(segments[2])
                );
        }

        public static FieldRule[] ParseFieldRules(string line)
        {
            // class: 0-1 or 4-19
            var matches = Regex.Matches(line, "(?<name>[a-z ]+): (?<range>(?<lower1>[0-9]+)-(?<upper1>[0-9]+)) or (?<range2>(?<lower2>[0-9]+)-(?<upper2>[0-9]+))");
            //return default;
            return matches.Select(match=> new FieldRule(
                match.Groups["name"].Value,
                new Range(int.Parse(match.Groups["lower1"].Value), int.Parse(match.Groups["upper1"].Value)),
                new Range(int.Parse(match.Groups["lower2"].Value), int.Parse(match.Groups["upper2"].Value))))
                .ToArray();
        }
    }
}