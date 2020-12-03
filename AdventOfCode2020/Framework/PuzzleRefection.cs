using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Framework
{
    class PuzzleRefection
    {
        public static IEnumerable<MethodInfo> GetAnswerMethods()
        {
            var methods = GetPuzzleClasses().SelectMany(GetAnswerMethods).ToList();

            var focusMethods = methods.Where(d => d.GetCustomAttribute<FocusAttribute>() != null).ToList();
            return focusMethods.Any() ? focusMethods : methods;
        }

        private static List<Type> GetPuzzleClasses() =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => Regex.IsMatch(t.Name, "^Day[0-9][0-9]$"))
                .OrderBy(t => t.Name).ToList();

        private static IEnumerable<MethodInfo> GetAnswerMethods(Type puzzleClass) => 
            puzzleClass.GetMethods()
                .Where(m => m.Name.StartsWith("GetAnswer", StringComparison.OrdinalIgnoreCase));

    }
}