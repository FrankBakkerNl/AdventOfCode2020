using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020
{
    public class InputDataManager
    {
        public static object[] GetInputArgs(MethodInfo method)
        {
            var filename = $@"..\..\..\..\AdventOfCode2020\Data\{method.DeclaringType.Name}.txt";

            var param = method.GetParameters().FirstOrDefault();
            if (param == null)
            {
                return null;
            }

            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, "");
                throw new Exception($"Input file {filename} has been created");
            }

            var parameterType = param.ParameterType;

            if (parameterType == typeof(string)) return new object [] {File.ReadAllText(filename)};

            if (parameterType == typeof(string[])) return new object [] {File.ReadAllLines(filename)};

            if (parameterType== typeof(int[]))
                return new object[]{ File.ReadAllLines(filename).Select(int.Parse).ToArray()};

            if (parameterType== typeof(long[]))
                return new object[] { File.ReadAllLines(filename).Select(long.Parse).ToArray() };

            throw new InvalidOperationException($"Unable to map input data for {method.DeclaringType.Name}.{method.Name}");
        }
    }
}
