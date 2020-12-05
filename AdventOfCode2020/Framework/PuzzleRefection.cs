using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Framework
{
    public class PuzzleRefection
    {
        public static IEnumerable<MethodInfo> GetAnswerMethods(bool ignoreFocus = false)
        {
            var methods = GetPuzzleClasses().SelectMany(GetAnswerMethods).ToList();
            
            if (ignoreFocus) return methods;

            var focusMethods = methods.Where(d => d.GetCustomAttribute<FocusAttribute>() != null).ToList();
            return focusMethods.Any() ? focusMethods : methods;
        }

        public static List<Type> GetPuzzleClasses() =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => Regex.IsMatch(t.Name, "^Day[0-9][0-9]$"))
                .OrderBy(t => t.Name).ToList();

        private static IEnumerable<MethodInfo> GetAnswerMethods(Type puzzleClass) => 
            puzzleClass.GetMethods()
                .Where(m => m.Name.StartsWith("GetAnswer", StringComparison.OrdinalIgnoreCase));

    }
}