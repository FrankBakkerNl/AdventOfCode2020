using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/7 </summary>
    public class Day07
    {
        /// <summary> Find number of possible containers for bag</summary>
        [Result(185)]
        public static int GetAnswer1(string[] input)
        {
            var map = GetReverseMap(input);

            IEnumerable<string> GetPossibleContainers(string bag) =>
                map[bag].Union(map[bag].SelectMany(GetPossibleContainers));

            return GetPossibleContainers("shiny gold").Count();
        }

        /// <summary> Find number of bags in containing bag</summary>
        [Result(89084)]
        public static int GetAnswer2(string[] input)
        {
            var map = GetMap(input);

            int CountContent(string bag) => map[bag].Sum(c => c.count + CountContent(c.bag) * c.count);

            return CountContent("shiny gold");
        }

        public static ILookup<string, string> GetReverseMap(string[] input) =>
            GetMappings(input).ToLookup(m => m.content.bag, m => m.container);

        public static ILookup<string, (int count, string bag)> GetMap(string[] input) => 
            GetMappings(input).ToLookup(m => m.container, m => m.content);

        private static IEnumerable<(string container, (int count, string bag) content)> GetMappings(string[] input) =>
            input.Where(l => !l.Contains("no other"))
                .Select(ParseLine)
                .SelectMany(l => l.content.Select(inner => (l.container, inner)));


        public static (string container, (int count, string bag)[] content) ParseLine(string line)
        {
            // vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
            // ------------              - ---------------  - -----------------

            var indexOfBags = line.IndexOf(" bags");

            return ( container: line[0 .. indexOfBags], 
                     content:   line[(indexOfBags + 14)..].Split(", ")
                                .Select(ParseContentPart)
                                .ToArray()
                     );
        }

        private static (int count, string bag) ParseContentPart(string c)
        {
            var indexOfSpace = c.IndexOf(" ");
            return (count: int.Parse(c[.. indexOfSpace]), 
                    bag:   c[(indexOfSpace + 1) .. c.IndexOf(" bag")]);
        }
    }
}