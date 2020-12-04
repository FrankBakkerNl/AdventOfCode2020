using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/2 </summary>
    public class Day02
    {
        [Result(493)]
        public static int GetAnswer1(string[] input) => input.Select(Parse).Count(IsValid);

        [Result(593)]
        public static int GetAnswer2(string[] input) => input.Select(Parse).Count(IsValidPolicy2);

        private static bool IsValid((int min, int max, char c, string pwd) arg)
        {
            var numberFound = arg.pwd.Count(c => c == arg.c);
            return numberFound >= arg.min && numberFound <= arg.max;
        }

        private static bool IsValidPolicy2((int min, int max, char c, string pwd) arg)
        {
            var char1 = arg.pwd[arg.min - 1];
            var char2 = arg.pwd[arg.max - 1];

            return char1 == arg.c ^ char2 == arg.c; 
        }

        public static (int min, int max, char c , string pwd) Parse(string line)
        {
            // match "1-3 a: abcde"
            var match = Regex.Match(line, "(?<min>-?[0-9]+)-(?<max>-?[0-9]+) (?<char>-?[a-z]+): (?<pwd>-?[a-z]+)");

            return (
                int.Parse(match.Groups["min"].Value),
                int.Parse(match.Groups["max"].Value),
                match.Groups["char"].Value[0],
                match.Groups["pwd"].Value);
        }
    }
}
