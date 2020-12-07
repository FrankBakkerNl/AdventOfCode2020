using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Environment;

namespace AdventOfCode2020.Puzzles
{
    /// <summary> https://adventofcode.com/2020/day/4 </summary>
    public class Day04
    {
        private static readonly string[] FieldNames = "byr iyr eyr hgt hcl ecl pid".Split(' ');

        [Result(200)]
        public static int GetAnswer1(string input) => ParsePassports(input).Count(IsValid);

        [Result(116)]
        public static int GetAnswer2(string input) => ParsePassports(input).Count(IsValidL2);

        public static IEnumerable<IReadOnlyCollection<(string key, string value)>> ParsePassports(string input) => 
            input.Split(NewLine + NewLine)
                .Select(GetFields);

        static IReadOnlyCollection<(string key, string value)> GetFields(string line) =>
            Regex.Matches(line, @"(?<key>-?[a-z]+):(?<val>-?[^ \r]+)")
                .Select(m => ( key:   m.Groups["key"].Value,
                    value: m.Groups["val"].Value))
                .Where(k => k.key != "cid").ToList();

        static bool IsValid(IEnumerable<(string key, string value)> fields) => 
            fields.Count() == 7;

        static bool IsValidL2(IReadOnlyCollection<(string key, string value)> fields) =>
            !FieldNames.Except(fields.Select(f => f.key)).Any() &&
            fields.All(IsValidField);

        /*
byr (Birth Year) - four digits; at least 1920 and at most 2002.
iyr (Issue Year) - four digits; at least 2010 and at most 2020.
eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
hgt (Height) - a number followed by either cm or in:
If cm, the number must be at least 150 and at most 193.
If in, the number must be at least 59 and at most 76.
hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
pid (Passport ID) - a nine-digit number, including leading zeroes.
cid (Country ID) - ignored, missing or not.
         */

        static bool IsValidField((string key, string value) kv) =>
            kv.key switch
            {
                "byr" => IsIntInRange(kv.value, 1920, 2002),
                "iyr" => IsIntInRange(kv.value, 2010, 2020),
                "eyr" => IsIntInRange(kv.value, 2020, 2030),
                "hgt" => CheckHeight(kv.value),
                "hcl" => Regex.IsMatch(kv.value, "^#[0-9a-f]{6}$"),
                "ecl" => "amb blu brn gry grn hzl oth".Split(' ').Contains(kv.value),
                "pid" => Regex.IsMatch(kv.value, "^[0-9]{9}$"),
                "cid" => true,
                _ => false
            };

        static bool IsIntInRange(string value, int min, int max) =>
            int.TryParse(value, out var i) && min <= i && i <= max;

        static bool CheckHeight(string value) =>
            value.EndsWith("cm") && IsIntInRange(value[..^2], 150, 193) || 
            value.EndsWith("in") && IsIntInRange(value[..^2], 59, 76);

    }
}
