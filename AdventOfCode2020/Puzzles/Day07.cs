using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/7 </summary>
    public class Day07
    {
        [Result(185)]
        public static int GetAnswer1(string[] input)
        {
            var map = GetMap(input);

            IEnumerable<string> GetPossibleContainers(string bag) =>
                map[bag].Union(map[bag].SelectMany(GetPossibleContainers));

            return GetPossibleContainers("shiny gold").Count();
        }

        [Result(89084)]
        public static int GetAnswer2(string[] input)
        {
            var map = GetMapReverse(input);

            int CountContent(string bag) => map[bag].Sum(c => c.count + CountContent(c.bag) * c.count);

            return CountContent("shiny gold");
        }

        public static ILookup<string, string> GetMap(string[] input) =>
            GetMappings(input).ToLookup(m => m.inner.bag, m => m.container);

        public static ILookup<string, (int count, string bag)> GetMapReverse(string[] input) => 
            GetMappings(input).ToLookup(m => m.container, m => m.inner);

        private static IEnumerable<((int count, string bag) inner, string container)> GetMappings(string[] input) =>
            input.Where(l => !l.Contains("no other"))
                .Select(ParseLine)
                .SelectMany(l => l.content.Select(inner => (inner, l.container)));

        public static (string container, (int count, string bag)[] content) ParseLine(string line)
        {
            // vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
            var parts = line.Split(new[] {" bags contain ", ", "}, StringSplitOptions.None);
            var container = parts[0];

            var content =  parts[1..].Select(inner => (count: int.Parse(inner[..inner.IndexOf(" ")]),
                bag: inner[(inner.IndexOf(" ")+1)..inner.LastIndexOf(" bag") ])).ToArray();
            return (container, content);
        }
    }
}